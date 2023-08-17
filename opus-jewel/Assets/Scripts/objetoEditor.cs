using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objetoEditor : MonoBehaviour
{
    private Jugador jugador;
    private GameObject escudo; // Referencia al objeto del escudo
    public GameObject escudoObject;
    public GameObject proyectilObject;
    private GameObject bala; //referencia al objeto proyectil
    private Proyectil ProyectilScript;
    
    private Escudo escudoScript; // Referencia al script del escudo
    
    // TODO LO EDITABLE
    [Header("SUMADOR")]

    [Header("JUGADOR")]
    public float tamañoJugador = 0f;
    public float velocidadMovimiento = 0f;
    [Header("PROYECTIL")]
    public float dañoProyectil = 0f;
    public float tamañoProyectil = 0f;
    public float velocidadProyectil = 0f;
    public float cadenciaDisparo = 0f;
    public float rangoDisparo = 0f;
    public float retrocesoProyectil = 0f;
    [Header("ESCUDO")]
    public float tamañoEscudo = 0f;
    public int cantidadBloqueosEscudo = 0;
    public float duracionActivaEscudo = 0f;

    [Header("MULTIPLICADOR")]

    [Header("JUGADOR")]
    public float MtamañoJugador = 1f;
    public float MvelocidadMovimiento = 1f;
    [Header("PROYECTIL")]
    public float MdañoProyectil = 1f;
    public float MtamañoProyectil = 1f;
    public float MvelocidadProyectil = 1f;
    public float McadenciaDisparo = 1f;
    public float MrangoDisparo = 1f;
    public float MretrocesoProyectil = 1f;
    [Header("ESCUDO")]
    public float MtamañoEscudo = 1f;
    public int McantidadBloqueosEscudo = 1;
    public float MduracionActivaEscudo = 1f;
    
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").GetComponent<Jugador>();
        escudoScript = escudoObject.GetComponent<Escudo>();
        ProyectilScript = proyectilObject.GetComponent<Proyectil>();
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Jugador"))
        {
        jugador.tamañoProyectil += tamañoProyectil;
        jugador.tamañoJugador += tamañoJugador;
        jugador.velocidadMovimiento += velocidadMovimiento;
        jugador.velocidadBala += velocidadProyectil;
        jugador.velocidadDisparo += cadenciaDisparo;
        jugador.rangoDisparo += rangoDisparo;
        escudoScript.tamañoEscudo += tamañoEscudo;
        escudoScript.cantidadBloqueos += cantidadBloqueosEscudo;
        escudoScript.duracionActiva += duracionActivaEscudo;
        ProyectilScript.fuerzaRetroceso += retrocesoProyectil;
        ProyectilScript.dañoProyectil += dañoProyectil;
        Destroy(gameObject);

        jugador.tamañoProyectil *= MtamañoProyectil;
        jugador.tamañoJugador *= MtamañoJugador;
        jugador.velocidadMovimiento *= MvelocidadMovimiento;
        jugador.velocidadBala *= MvelocidadProyectil;
        jugador.velocidadDisparo *= McadenciaDisparo;
        jugador.rangoDisparo *= MrangoDisparo;
        escudoScript.tamañoEscudo *= MtamañoEscudo;
        escudoScript.cantidadBloqueos *= McantidadBloqueosEscudo;
        escudoScript.duracionActiva *= MduracionActivaEscudo;
        ProyectilScript.fuerzaRetroceso *= MretrocesoProyectil;
        ProyectilScript.dañoProyectil *= MdañoProyectil;
        Destroy(gameObject);
        }
    }
}
