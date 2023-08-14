using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ESTE SCRIPT DEBE MANEJAR EL RADIO DE ATAQUE DEL ENEMIGO CIEGO

public class RadioAtaque : MonoBehaviour
{
    private EnemigoCiego enemigo;
    private EnemigoIAcheckpoints checkpoints;
    private Transform enemigoTransform; // Transform del enemigo
    void Start ()
    {
        //checkpoints = GetComponentInParent<EnemigoIAcheckpoints>();
        enemigo = GetComponentInParent<EnemigoCiego>();
        enemigoTransform = enemigo.transform;
    }
    void Update ()
    {
        if (enemigoTransform != null)
        {
            transform.position = enemigoTransform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador") && !enemigo.atacando)
        {
            StartCoroutine(enemigo.ActivarAtaqueDespuesDeEspera());
            enemigo.sr.color = Color.red;
            enemigo.atacando = true;
        }
    }
    
}