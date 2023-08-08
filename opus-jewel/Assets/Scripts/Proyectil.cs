using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private float fuerzaRetrocesoInicial = 3f;
    public float fuerzaRetroceso = 1f;
    public float dañoProyectil = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Enemigo ha sido dañado.");

            // Obtenemos el script del enemigo y le restamos vidas
            Enemigo enemigo = collision.gameObject.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirGolpe(dañoProyectil);
            }
            // Aplicamos fuerza de retroceso al enemigo para que se mueva hacia atrás
            Rigidbody2D rbEnemigo = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rbEnemigo != null)
            {
                Vector2 direccionRetroceso = (rbEnemigo.position - (Vector2)transform.position).normalized;
                rbEnemigo.AddForce(direccionRetroceso * (fuerzaRetrocesoInicial * fuerzaRetroceso), ForceMode2D.Impulse);
            }

            // Destruimos el proyectil
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
    }
}
}
