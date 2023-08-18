using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoColisionAI : MonoBehaviour
{
    
    private Jugador jugador;
    private Rigidbody2D rb;
    [SerializeField] private float velocidadMovimiento = 1f;
    private float limiteAbajo=-4.6f;
    private float limiteArriba=1000f;
    private float limiteIzquierdo=-1000f;
    private float limiteDerecho=8.8f;
    private bool isOnMap = true;


    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<Jugador>();
        rb = GetComponent<Rigidbody2D>();

        if (gameObject.CompareTag("Enemigo"))
        {
            isOnMap = CheckIfOnMap();
        }
    }

    void Update()
    {   
        if (jugador != null && isOnMap==true && EnemigoMovActivoManager.puedeMoverse)
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
