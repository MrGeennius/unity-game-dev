using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Escudo : MonoBehaviour
{
    public Transform jugador;
    public float fuerzaEmpujar = 10f;
    public float duracionActiva = 2f;
    private float tiempoEnfriamiento = 4f;
    private float tiempoInicioEnfriamiento = 0f;
    private bool puedeActivar = true;
    public bool activado = false;

    private void Start()
    {
        DesactivarEscudo(); // Desactivar el escudo original al inicio
    }

    private void Update()
    {
        if (activado)
        {
            duracionActiva -= Time.deltaTime;
            if (duracionActiva <= 0)
            {
                DesactivarEscudo();
            }
            transform.position = jugador.position;
        }
        // Verificar si ha pasado el tiempo de enfriamiento
        if (Time.time >= tiempoInicioEnfriamiento + tiempoEnfriamiento)
        {
        puedeActivar = true;
        Debug.Log("Escudo enfriado en el script del escudo");
        }
        
    }

    public void ActivarEscudo()
    {
        if (puedeActivar)
        {
            puedeActivar = false;
            activado = true;
            
            duracionActiva = 2f; // Establecer la duración activa inicial del escudo
            

            // Crear una nueva instancia del escudo
            gameObject.SetActive(true); // Activar el objeto del escudo actual
            
            transform.position = jugador.position; // Establecer la posición del escudo igual a la del jugador
            // Actualizar el tiempo de inicio del enfriamiento
            tiempoInicioEnfriamiento = Time.time;
            
        }
        else{
            Debug.Log("tiempo Inicio + tiempo Enfriamiento : "+ tiempoInicioEnfriamiento + tiempoEnfriamiento);
            Debug.Log(Time.time >= tiempoInicioEnfriamiento + tiempoEnfriamiento);
        }
    }

    private void DesactivarEscudo()
    {
        activado = false;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    if (activado)
    {
        // Colisionar con enemigos y proyectiles enemigos
        if (collision.gameObject.CompareTag("Enemigo") || collision.gameObject.CompareTag("ProyectilEnemigo"))
        {
            Debug.Log("Colisión con enemigo o proyectil enemigo");
            EmpujarObjeto(collision.gameObject);
            DesactivarEscudo();
        }
    }
    }

    private void EmpujarObjeto(GameObject objeto)
    {
        // Calcular la dirección desde el jugador al objeto
        Vector2 direccion = objeto.transform.position - jugador.position;

        // Aplicar fuerza para empujar el objeto hacia afuera del jugador
        Rigidbody2D rbObjeto = objeto.GetComponent<Rigidbody2D>();
        if (rbObjeto != null)
        {
            rbObjeto.AddForce(direccion.normalized * 10f, ForceMode2D.Impulse);
        }
    }
}
