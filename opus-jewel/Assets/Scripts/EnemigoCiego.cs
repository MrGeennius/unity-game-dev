using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemigoCiego : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private float distanciaMinimaRecorrido;
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
    private bool isOnMap = false;
    public SpriteRenderer sr;


    public bool atacando = false;
    private bool enMovimiento = false;
    private bool grande = false;
    public Transform jugadorTransform;
    public float tiempoEspera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        isOnMap=CheckIfOnMap();
        
        posicionXEnemigo = transform.position.x;
        posicionYEnemigo = transform.position.y;
        ElegirNuevoObjetivo();
        
    }
    private void Update()
    {
        if (impulsando)
        {
            rb.AddForce(direccionImpulso * impulso, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Escudo") )
        {
            Debug.Log("Colision detectada");
            impulsando = false;
        }
    }
    private bool CheckIfOnMap()
    {
        float posx = transform.position.x;
        float posy = transform.position.y;
        return posx >= limiteIzquierdo && posx <= limiteDerecho && posy >= limiteAbajo && posy <= limiteArriba;
    }
    public void ElegirNuevoObjetivo()
    {
        bool objetivoValido = false;
        float x = posicionXEnemigo;
        float y = posicionYEnemigo;
        int i = 0;
        while (!objetivoValido && i < 10 && isOnMap)
        {
            x = posicionXEnemigo + Random.Range(posicionMinimaX, posicionMaximaX);
            y = posicionYEnemigo + Random.Range(posicionMinimaY, posicionMaximaY);

            // Redondear las coordenadas a números enteros
            x = Mathf.Round(x);
            y = Mathf.Round(y);

            // Calcular la distancia entre las coordenadas y la posición del enemigo
            float distancia = Vector2.Distance(new Vector2(x, y), new Vector2(posicionXEnemigo, posicionYEnemigo));

            objetivo = new Vector3(x, y, transform.position.z);
            Debug.Log(x + " " + y + " " +  (x >= limiteIzquierdo && x <= limiteDerecho && y >= limiteAbajo && y <= limiteArriba && distancia >= distanciaMinimaRecorrido));
            
            if (x >= limiteIzquierdo && x <= limiteDerecho && y >= limiteAbajo && y <= limiteArriba && distancia >= distanciaMinimaRecorrido && !atacando && !enMovimiento)
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
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(transform.position, objetivo);
        
        while (Vector3.Distance(transform.position, objetivo) > distanciaMinima && !atacando)
        {
            float distanceCovered = (Time.time - startTime) * velocidadMovimiento;
            float fractionOfJourney = Mathf.SmoothStep(0, 1, distanceCovered / journeyLength);

            transform.position = Vector3.Lerp(transform.position, objetivo, fractionOfJourney);
            yield return null;
        }

        enMovimiento = false;
        yield return new WaitForSeconds(tiempoEspera);
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