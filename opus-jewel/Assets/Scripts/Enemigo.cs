using UnityEngine;

public class Enemigo : MonoBehaviour
{

    [Header("Estadisticas")]
    [SerializeField] private float velocidadMovimiento = 1f;
    [SerializeField] private int vidasMaximas = 3;
    private int vidasActuales;
    private Rigidbody2D rb;
    private personaje jugador;

    [Header("Velocidad Golpes")]
    [SerializeField] private float fuerzaRetroceso = 5f;

    void Start()
    {
        vidasActuales = vidasMaximas;
        jugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<personaje>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Mover al enemigo hacia el jugador
        if (jugador != null)
        {
            Vector2 direccion = jugador.transform.position - transform.position;
            direccion.Normalize();
            transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
        }
    }
    public void RecibirGolpe()
    {
        vidasActuales--;

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
            personaje jugador = collision.gameObject.GetComponent<personaje>();
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
            personaje jugador = collision.gameObject.GetComponent<personaje>();
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
    }
}