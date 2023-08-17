using UnityEngine;

public class Enemigo : MonoBehaviour
{

    [Header("Estadisticas")]
    [SerializeField] private float vidasMaximas = 3f;
    private float vidasActuales;
    private Rigidbody2D rb;
    private Jugador jugador;

    [Header("Velocidad Golpes")]
    [SerializeField] private float fuerzaRetroceso = 5f;
    
    private salaManager manager;
    private enemySpawner enemySpawner;

    void Start()
    {
        manager = GameObject.FindObjectOfType<salaManager>();
        enemySpawner = GameObject.FindObjectOfType<enemySpawner>();
        vidasActuales = vidasMaximas;
        jugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<Jugador>();
        rb = GetComponent<Rigidbody2D>();

        // Actualizar la variable isOnMap según la posición inicial del enemigo

    }
    
    void Update()
    {
        // Mover al enemigo hacia el jugador

    }

    public void RecibirGolpe(float daño)
    {
        vidasActuales -= daño;

        if (vidasActuales <= 0)
        {
            DestruirEnemigo();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador") && jugador.esInvulnerable==false)
        {
            Debug.Log("Golpeado");
            // Obtener el script del jugador y restarle vidas
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();
            if (jugador != null)
            {
                jugador.RecibirGolpe();
            }

            // Aplicar fuerza de retroceso
            Rigidbody2D rbEnemigo = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rbEnemigo != null)
            {
                Vector2 direccionRetroceso = (rbEnemigo.position - (Vector2)transform.position).normalized;
                rbEnemigo.AddForce(direccionRetroceso * fuerzaRetroceso, ForceMode2D.Impulse);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador") && !jugador.esInvulnerable)
        {
            Debug.Log("Golpeado por Mantenerse en Contacto");
            // Obtener el script del jugador y restarle vidas
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();
            if (jugador != null)
            {
                jugador.RecibirGolpe();
            }

            // Aplicar fuerza de retroceso
            Rigidbody2D rbEnemigo = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rbEnemigo != null)
            {
                Vector2 direccionRetroceso = (rbEnemigo.position - (Vector2)transform.position).normalized;
                rbEnemigo.AddForce(direccionRetroceso * fuerzaRetroceso, ForceMode2D.Impulse);
            }  
        }
    }
    private void DestruirEnemigo()
    {
        Destroy(gameObject);
        enemySpawner.ContadorBichos--;
        enemySpawner.enemigoMuerto=true;
    }
}