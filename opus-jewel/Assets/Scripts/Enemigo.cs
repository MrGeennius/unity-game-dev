using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vidasMaximas = 3;
    private int vidasActuales;
    private Rigidbody2D rb;

    void Start()
    {
        vidasActuales = vidasMaximas;
        rb = GetComponent<Rigidbody2D>();
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