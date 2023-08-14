using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoCiego : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private float posicionMinimaX = 1f;
    [SerializeField] private float posicionMaximaX = 2f;
    [SerializeField] private float posicionMinimaY = 1f;
    [SerializeField] private float posicionMaximaY = 2f;

    public float impulso = 10f;
    public float duracionImpulso = 1f;
    public float frenado = 5f;
    private bool impulsando = false;
    private Vector2 direccionImpulso;
    private Rigidbody2D rb;
    private EnemigoWanderAI enemigoWanderAI;

    private float limiteAbajo = -4.6f;
    private float limiteArriba = 4.6f;
    private float limiteIzquierdo = -8.6f;
    private float limiteDerecho = 8.6f;
    private Vector3 objetivo;
    private float posicionXEnemigo;
    private float posicionYEnemigo;

    public SpriteRenderer sr;


    public bool atacando = false;
    private bool enMovimiento = false;
    private bool grande = false;
    public Transform jugadorTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ElegirNuevoObjetivo();
        posicionXEnemigo = transform.position.x;
        posicionYEnemigo = transform.position.y;
    }
    private void Update()
    {
        if (impulsando)
        {
            rb.AddForce(direccionImpulso * impulso, ForceMode2D.Impulse);
        }
    }

    public void ElegirNuevoObjetivo()
    {
        bool objetivoValido = false;
        float x = posicionXEnemigo;
        float y = posicionYEnemigo;
        int i = 0;
        while (!objetivoValido && i < 10)
        {
            x = posicionXEnemigo + Random.Range(posicionMinimaX, posicionMaximaX);
            y = posicionYEnemigo + Random.Range(posicionMinimaY, posicionMaximaY);
            objetivo = new Vector3(x, y, transform.position.z);
            Debug.Log(x + " " + y + " " +  (x >= limiteIzquierdo && x <= limiteDerecho && y >= limiteAbajo && y <= limiteArriba) );
            if (x >= limiteIzquierdo && x <= limiteDerecho && y >= limiteAbajo && y <= limiteArriba && !atacando && !enMovimiento)
            {
                Debug.Log("Objetivo valido");
                objetivoValido = true;
                StartCoroutine(MoverHaciaObjetivo());
            }
            i++;
        }
    }

    private IEnumerator MoverHaciaObjetivo()
    {
        enMovimiento = true;
        while (Vector3.Distance(transform.position, objetivo) > distanciaMinima && !atacando)
        {
            transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidadMovimiento * Time.deltaTime);
            yield return null;
        }
        enMovimiento = false;
        yield return new WaitForSeconds(0.1f);
        posicionXEnemigo = transform.position.x;
        posicionYEnemigo = transform.position.y;
        if (!atacando && !enMovimiento)
        {
            ElegirNuevoObjetivo();
        }
    }

    

    
    public IEnumerator ActivarAtaqueDespuesDeEspera()
    {
        if(!grande)
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(1f); // Espera 1 segundo
                // Escalar el tamaño del enemigo
                transform.localScale *= 1.2f; // Aumenta el tamaño en un 20%
            }
            grande=true;
        }
        
        yield return new WaitForSeconds(3f); // Espera 3 segundos
        Vector2 direccion = (jugadorTransform.position - transform.position).normalized;
        ActivarAtaque(direccion);
        //checkpoints.alcanzoPunto = true;
    }
    public void ActivarAtaque(Vector2 direccion)
    {
        if (!impulsando)
        {
            direccionImpulso = direccion.normalized;
            StartCoroutine(Impulsar());
        }
    }
    private IEnumerator Impulsar()
    {
        impulsando = true;
        yield return new WaitForSeconds(duracionImpulso);
        impulsando = false;

        Vector2 velocidadActual = rb.velocity;
        float velocidadMagnitud = velocidadActual.magnitude;
        Vector2 direccionFrenado = -velocidadActual.normalized;
        float fuerzaFrenado = Mathf.Clamp(frenado * velocidadMagnitud, 0f, rb.mass * frenado);
        rb.AddForce(direccionFrenado * fuerzaFrenado, ForceMode2D.Force);
        Debug.Log("Eligiendo nuevo objetivo");
        atacando=false;
        sr.color = Color.white;
        if(grande)
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(1f);

                transform.localScale *= 0.8f; 
            }
            grande=false;
        }
        ElegirNuevoObjetivo();
    }
}