using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class personaje : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Transform Posicion; 
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 1f;
    [Header("Disparo")]
    [SerializeField] private float tiempoDisparo=1f;
    [SerializeField] private float proximoDisparo=1f,veldisparo=1f;
    
     
     [Header("Objetos")]
     public GameObject Bala;
     private GameObject bala;
     public Transform PuntoDisparoArriba;
     public Transform PuntoDisparoAbajo;
     public Transform PuntoDisparoIzquierda;
     public Transform PuntoDisparoDerecha;
     public Transform PuntoDisparoArribaIzquierda;
     public Transform PuntoDisparoArribaDerecha;
     public Transform PuntoDisparoAbajoIzquierda;
     public Transform PuntoDisparoAbajoDerecha;
     private Transform PuntoDisparo;
     private Vector2 direccionDisparo;
    
    [Header("Invulnerabilidad")]
    [SerializeField] private float tiempoInvulnerable = 2f;
    public bool esInvulnerable = false;


    [Header("Vida")]
    [SerializeField] private int vidasMaximas = 3;
    private int vidasActuales;
    public TMP_Text textoVidas;

    //REFERENCIAS
    private GameObject escudo; // Referencia al objeto del escudo
    public GameObject escudoObject;
    private Escudo escudoScript; // Referencia al script del escudo

    //Limites de pared
    private float limiteAbajo=-4.485f;
    private float limiteArriba=4.485f;
    //8.485
    private float limiteIzquierdo=-8.556f;
    private float limiteDerecho=8.556f;
    // Start is called before the first frame update
    void Start()
    {
        //INICIALZIAR COMPONENTES
        spriteRenderer = GetComponent<SpriteRenderer>();
        vidasActuales = vidasMaximas;
        textoVidas.text = vidasActuales.ToString();
        PuntoDisparo = PuntoDisparoArriba;
        rb = GetComponent<Rigidbody2D>();

        //ESCUDO
        escudoScript = escudoObject.GetComponent<Escudo>();
        escudoScript.jugador = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        // INICIO MOVIMIENTO JUGADOR
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        // Si el jugador está en una posición límite en el eje X, bloquear el movimiento horizontal
        if ((horizontalInput < 0 && transform.position.x <= limiteIzquierdo) || (horizontalInput > 0 && transform.position.x >= limiteDerecho))
        {
        horizontalInput = 0;
        }
        // Si el jugador está en una posición límite en el eje Y, bloquear el movimiento vertical
        if ((verticalInput < 0 && transform.position.y <= limiteAbajo) || (verticalInput > 0 && transform.position.y >= limiteArriba))
        {
        verticalInput = 0;
        }
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * velocidadMovimiento * Time.deltaTime;
        transform.Translate(movement);
        // FIN MOVIMIENTO JUGADOR
        // DISPARO
        if (Input.GetMouseButtonDown(0) && Time.time>proximoDisparo){
            Disparo();
            proximoDisparo=Time.time+tiempoDisparo;
        }
        //FIN DISPARO

        //ESCUDO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            ActivarEscudo();
        }
        //FIN ESCUDO
    }

    private void ActivarEscudo()
    {
        if (!escudoScript.activado)
        {
            escudoScript.ActivarEscudo();
            Debug.Log("Escudo activado con espacio a traves del personaje");
        }
    }

    public void RecibirGolpe()
    {
        if (!esInvulnerable) // Verificar si el personaje es vulnerable
        {
            vidasActuales--;

            if (vidasActuales <= 0)
            {
                Morir();
                textoVidas.text = vidasActuales.ToString();
                // Activar animación de muerte
            }
            else
            {
                // Activar el temporizador de invulnerabilidad
                StartCoroutine(TemporalInvulnerabilidad());
                textoVidas.text = vidasActuales.ToString();
                // Añadir animacion de daño
            }
        }
    }

    private IEnumerator TemporalInvulnerabilidad()
    {
        esInvulnerable = true;
        spriteRenderer.color = Color.red;
        // Esperar el tiempo de invulnerabilidad
        yield return new WaitForSeconds(tiempoInvulnerable);
        esInvulnerable = false;
        spriteRenderer.color = Color.white;
    }


    private void Morir()
    {
        Destroy(gameObject);
    }
    void Disparo()
    {
        // Obtener la dirección del disparo basada en la posición del mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direccionDisparo = (worldMousePosition - (Vector3)transform.position).normalized;

        // Redondear los valores del vector de dirección a múltiplos de 45 grados
        float angle = Mathf.Atan2(direccionDisparo.y, direccionDisparo.x) * Mathf.Rad2Deg;
        int roundedAngle = Mathf.RoundToInt(angle / 45f) * 45;
        float roundedAngleRad = roundedAngle * Mathf.Deg2Rad;
        direccionDisparo = new Vector2(Mathf.Cos(roundedAngleRad), Mathf.Sin(roundedAngleRad));

        // Selecionar el punto de disparo correspondiente a la dirección redondeada
        if (direccionDisparo == Vector2.up)
        {
            PuntoDisparo = PuntoDisparoArriba;
        }
        else if (direccionDisparo == Vector2.down)
        {
            PuntoDisparo = PuntoDisparoAbajo;
        }
        else if (direccionDisparo == Vector2.left)
        {
            PuntoDisparo = PuntoDisparoIzquierda;
        }
        else if (direccionDisparo == Vector2.right)
        {
            PuntoDisparo = PuntoDisparoDerecha;
        }
        else if (direccionDisparo == new Vector2(1, 1).normalized)
        {
            PuntoDisparo = PuntoDisparoArribaDerecha;
        }
        else if (direccionDisparo == new Vector2(-1, 1).normalized)
        {
            PuntoDisparo = PuntoDisparoArribaIzquierda;
        }
        else if (direccionDisparo == new Vector2(1, -1).normalized)
        {
            PuntoDisparo = PuntoDisparoAbajoDerecha;
        }
        else if (direccionDisparo == new Vector2(-1, -1).normalized)
        {
            PuntoDisparo = PuntoDisparoAbajoIzquierda;
        }

        // Aquí agregamos un ajuste a la posición del punto de disparo según la dirección del disparo
        PuntoDisparo.localPosition = direccionDisparo;

        // Crear la bala en la posición del punto de disparo
        bala = Instantiate(Bala, PuntoDisparo.position, Quaternion.identity);

        // Obtener el componente Rigidbody2D de la bala
        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

        // Aplicar una fuerza para disparar la bala en la dirección adecuada
        rbBala.AddForce(direccionDisparo * veldisparo, ForceMode2D.Impulse);
    }

}
