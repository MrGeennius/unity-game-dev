using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Estadisticas")]
    [SerializeField] private float velocidadMovimiento = 1f;
    [SerializeField] private int vidasMaximas = 3;
    private int vidasActuales;
    private Rigidbody2D rb;
    private Transform jugador;

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

    private void DestruirEnemigo()
    {
        Destroy(gameObject);
    }
}