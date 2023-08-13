using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoCiego : MonoBehaviour
{
    public float impulso = 10f; // Fuerza del impulso
    public float duracionImpulso = 1f; // Duración del impulso en segundos
    public bool atacando = false; // Para verificar si el enemigo está atacando
    public Transform jugadorTransform; // Transform del jugador
    private Rigidbody2D rb; // Rigidbody2D del enemigo
    private bool impulsando = false; // Para rastrear si está ocurriendo el impulso
    private Vector2 direccionImpulso; // Dirección del impulso
    public float frenado = 5f; // Fuerza de frenado gradual
    private EnemigoIAcheckpoints checkpoints;

    void Start()
    {
        atacando = false;
        jugadorTransform = GameObject.FindGameObjectWithTag("Jugador").transform;
        rb = GetComponent<Rigidbody2D>();   
        checkpoints = GetComponentInParent<EnemigoIAcheckpoints>();
        
    }

    void Update()
    {
        if (impulsando)
        {
            rb.AddForce(direccionImpulso * impulso, ForceMode2D.Impulse);
        } 
    }

    public void ActivarAtaque(Vector2 direccion)
    {
        
        if (!impulsando)
        {
            direccionImpulso = direccion.normalized;
            StartCoroutine(Impulsar());
        }
    }

    private IEnumerator Impulsar()
    {
        impulsando = true;
        yield return new WaitForSeconds(duracionImpulso);
        impulsando = false;

        // Aplicar fuerza de frenado gradual
        Vector2 velocidadActual = rb.velocity;
        float velocidadMagnitud = velocidadActual.magnitude;
        Vector2 direccionFrenado = -velocidadActual.normalized;
        float fuerzaFrenado = Mathf.Clamp(frenado * velocidadMagnitud, 0f, rb.mass * frenado);
        rb.AddForce(direccionFrenado * fuerzaFrenado, ForceMode2D.Force);
        checkpoints.alcanzoPunto = false;
        atacando = false;
    }

}
