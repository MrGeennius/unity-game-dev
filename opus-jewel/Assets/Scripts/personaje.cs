using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class personaje : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Transform Posicion; 
    [Header("Movimiento")]
    private float velocidadMovimientoInicial=5f;
    public float velocidadMovimiento = 1f;
    [Header("Disparo")]
    public float velocidadDisparo=1f;
    [SerializeField] private float proximoDisparo=1f;
    public float velocidadBala=1f;
    private float rangoDisparoInicial=5f;
    public float rangoDisparo = 1f;
    private float tamañoInicialProyectil=0.445f;
    public float tamañoProyectil = 1f;
    private float distanciaRecorrida = 0f; //Distancia recorrida de la bala
    private Vector2 posicionInicialBala;
    
     
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

    [Header("Tamaño")]
    private float tamañoInicialJugador=1f;
    public float tamañoJugador;

    //REFERENCIAS
    private GameObject escudo; // Referencia al objeto del escudo
    public GameObject escudoObject;
    private Escudo escudoScript; // Referencia al script del escudo

    //Limites de pared
    // public bool limitar=false;
    // private float limiteAbajo=-4.490f;
    // private float limiteArriba=4.490f;
    // //8.485
    // private float limiteIzquierdo=-8.685f;
    // private float limiteDerecho=8.685f;

    private bool estaMoviendo = false;
    private bool bloquearArriba = false;
    private bool bloquearAbajo = false;
    private bool bloquearDerecha = false;
    private bool bloquearIzquierda = false;
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
        rb.transform.localScale = Vector3.one * (tamañoInicialJugador * tamañoJugador);
        // INICIO MOVIMIENTO JUGADOR
        float horizontalInput = Input.GetAxis("Horizontal") ;
        float verticalInput = Input.GetAxis("Vertical");

        //BLOQUEO DE MOVIMIENTOS
        
        if(bloquearIzquierda && horizontalInput < 0){
            horizontalInput=0;
        }
        if(bloquearDerecha && horizontalInput > 0){
            horizontalInput=0;
        }
        if(bloquearArriba && verticalInput > 0){
            verticalInput=0;
        }
        if(bloquearAbajo && verticalInput < 0){
            verticalInput=0;
        }

        Vector2 movement = new Vector2(horizontalInput, verticalInput) * (velocidadMovimientoInicial * velocidadMovimiento) * Time.deltaTime;
        transform.Translate(movement);
        // FIN MOVIMIENTO JUGADOR
        // DISPARO
        if (Input.GetMouseButtonDown(0) && Time.time>proximoDisparo){
            Disparo();
            proximoDisparo=Time.time+velocidadDisparo;
        }
        //FIN DISPARO

        //ESCUDO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            ActivarEscudo();
        }
        //FIN ESCUDO

        // Verificar si el jugador se está moviendo
        if(estaMoviendo = movement.magnitude > 0f){
            gameObject.GetComponent<Animator>().SetBool("moviendose", true);
        }
        else{
            gameObject.GetComponent<Animator>().SetBool("moviendose", false);
        }
        // Giro la animacion del personaje
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("ParedIzquierda"))
            {
                bloquearIzquierda = true;
            }
            else if (collision.gameObject.CompareTag("ParedDerecha"))
            {
                bloquearDerecha = true;
            }
            else if (collision.gameObject.CompareTag("ParedArriba"))
            {
                bloquearArriba = true;
            }
            else if (collision.gameObject.CompareTag("ParedAbajo"))
            {
                bloquearAbajo = true;
            }
        }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ParedIzquierda"))
            {
                bloquearIzquierda = false;
            }
            else if (collision.gameObject.CompareTag("ParedDerecha"))
            {
                bloquearDerecha = false;
            }
            else if (collision.gameObject.CompareTag("ParedArriba"))
            {
                bloquearArriba = false;
            }
            else if (collision.gameObject.CompareTag("ParedAbajo"))
            {
                bloquearAbajo = false;
            }
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
        SceneManager.LoadScene("Juego");
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

        // Obtener el tamaño del proyectil
        bala.transform.localScale = Vector3.one * (tamañoInicialProyectil * tamañoProyectil);

        // Obtener el componente Rigidbody2D de la bala
        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

        // Aplicar una fuerza para disparar la bala en la dirección adecuada
        rbBala.AddForce(direccionDisparo * velocidadBala, ForceMode2D.Impulse);

        //Obtengo Posicion Inicial de la bala
        posicionInicialBala = bala.transform.position;
        // Inicializar corutina de rango de bala
        StartCoroutine(ControlarRangoBala(rbBala));
    }

    private IEnumerator ControlarRangoBala(Rigidbody2D rbBala)
    {
        distanciaRecorrida = 0f;

        // Loop mientras la distancia recorrida sea menor o igual al rango de la bala
        while (rbBala != null)
        {
        distanciaRecorrida = (posicionInicialBala - (Vector2)rbBala.transform.position).magnitude;
        if (distanciaRecorrida > (rangoDisparoInicial * rangoDisparo) )
        {
            Destroy(rbBala.gameObject);
        }
        yield return null;
        }

        if (rbBala != null)
            {
                Destroy(rbBala.gameObject);
            }
        
    }


}
