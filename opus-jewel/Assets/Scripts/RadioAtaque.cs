using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAtaque : MonoBehaviour
{
    private EnemigoCiego enemigo;
    private EnemigoIAcheckpoints checkpoints;
    private Transform enemigoTransform; // Transform del enemigo
    void Start ()
    {
        checkpoints = GetComponentInParent<EnemigoIAcheckpoints>();
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
        if (collision.CompareTag("Jugador"))
        {
            enemigo = GetComponentInParent<EnemigoCiego>();
            StartCoroutine(ActivarAtaqueDespuesDeEspera());
        }
    }
    private IEnumerator ActivarAtaqueDespuesDeEspera()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos
        Vector2 direccion = (enemigo.jugadorTransform.position - enemigo.transform.position).normalized;
        enemigo.ActivarAtaque(direccion);
        enemigo.atacando = true;
        checkpoints.alcanzoPunto = true;
    }
}