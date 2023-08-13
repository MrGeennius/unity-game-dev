using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoColisionAI : MonoBehaviour
{
    private personaje jugador;
    private Rigidbody2D rb;
    [SerializeField] private float velocidadMovimiento = 1f;
    private float limiteAbajo=-4.6f;
    private float limiteArriba=4.6f;
    private float limiteIzquierdo=-8.8f;
    private float limiteDerecho=8.8f;

    private bool isOnMap = true;


    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<personaje>();
        rb = GetComponent<Rigidbody2D>();

        if (gameObject.CompareTag("Enemigo"))
        {
            isOnMap = CheckIfOnMap();
        }
    }

    void Update()
    {
        if (jugador != null && isOnMap==true)
        {
            Vector2 direccion = jugador.transform.position - transform.position;
            direccion.Normalize();
            transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
        }
    }

    private bool CheckIfOnMap()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        return x >= limiteIzquierdo && x <= limiteDerecho && y >= limiteAbajo && y <= limiteArriba;
    }
}
