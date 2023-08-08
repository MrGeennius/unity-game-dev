using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UNA pizza
public class Biggity : MonoBehaviour
{
    private personaje jugador;
    private GameObject escudo; // Referencia al objeto del escudo
    public GameObject escudoObject;
    private Escudo escudoScript; // Referencia al script del escudo
    
    // TODO LO EDITABLE
    public float tamañoProyectil = 1f;
    public float tamañoJugador = 1f;
    public float velocidadBala = 1f;
    public float velocidadMovimientoJugador = 1f;
    public float velocidadDisparo = 1f;
    public float tamañoEscudo = 1f;
    public int cantidadBloqueosEscudo = 1;
    public float duracionActivaEscudo = 1f;
    public float rangoDisparo = 1f;



    public 
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<personaje>();
        escudoScript = escudoObject.GetComponent<Escudo>();
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Jugador"))
        {
        jugador.tamañoProyectil *= tamañoProyectil;
        jugador.tamañoJugador *= tamañoJugador;
        jugador.velocidadMovimiento *= velocidadMovimientoJugador;
        jugador.velocidadBala *= velocidadBala;
        jugador.velocidadDisparo *= velocidadDisparo;
        jugador.rangoDisparo *= rangoDisparo;
        escudoScript.tamañoEscudo *= tamañoEscudo;
        escudoScript.cantidadBloqueos *= cantidadBloqueosEscudo;
        escudoScript.duracionActiva *= duracionActivaEscudo;
        Destroy(gameObject);
        }
    }
}
