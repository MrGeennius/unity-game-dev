using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Estadisticas")]
    [SerializeField] private float velocidadMovimiento = 1f;
    [SerializeField] private int vidasMaximas = 3;
    private int vidasActuales;
    private Rigidbody2D rb;
    private Transform jugador;

    [Header("Velocidad Golpes")]
    [SerializeField] private float tiempoGolpe=1f;
    [SerializeField] private float proximoGolpe=1f;

    void Start()
    {
        vidasActuales = vidasMaximas;
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Mover al enemigo hacia el jugador
        if (jugador != null)
        {
            Vector2 direccion = jugador.position - transform.position;
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador") && Time.time > proximoGolpe)
        {
            // Obtener el script del jugador y restarle vidas
            personaje jugador = collision.gameObject.GetComponent<personaje>();
            if (jugador != null)
            {
                jugador.RecibirGolpe();
            }

            // Establecer el tiempo para el próximo golpe
            proximoGolpe = Time.time + tiempoGolpe;
        }
    }
    private void DestruirEnemigo()
    {
        Destroy(gameObject);
    }
}