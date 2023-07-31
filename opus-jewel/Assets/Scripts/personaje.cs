using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personaje : MonoBehaviour
{
     public float velocidadMovimiento = 1f;
     private Rigidbody2D rb;
     public GameObject Bala;
     private GameObject bala;
     private Transform Posicion; 
     public Transform PuntoDisparoArriba;
     public Transform PuntoDisparoAbajo;
     public Transform PuntoDisparoIzquierda;
     public Transform PuntoDisparoDerecha;
     public Transform PuntoDisparoArribaIzquierda;
     public Transform PuntoDisparoArribaDerecha;
     public Transform PuntoDisparoAbajoIzquierda;
     public Transform PuntoDisparoAbajoDerecha;
     private Transform PuntoDisparo;
     private Vector2 direccionDisparo;

     public float veldisparo=1f;
     public float tiempoDisparo=1f, proximoDisparo=1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // INICIO MOVIMIENTO JUGADOR
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * velocidadMovimiento * Time.deltaTime;
        transform.Translate(movement);
        // FIN MOVIMIENTO JUGADOR
        // DISPARO
        if (Input.GetMouseButtonDown(0) && Time.time>proximoDisparo){
            Disparo();
            proximoDisparo=Time.time+tiempoDisparo;
        }
        //FIN DISPARO

    }
    
    void Disparo()
    {
        // Obtener la dirección del disparo basada en la posición del mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direccionDisparo = (worldMousePosition - (Vector3)transform.position).normalized;

        // Redondear los valores del vector de dirección a múltiplos de 45 grados
        float angle = Mathf.Atan2(direccionDisparo.y, direccionDisparo.x) * Mathf.Rad2Deg;
        int roundedAngle = Mathf.RoundToInt(angle / 45f) * 45;
        float roundedAngleRad = roundedAngle * Mathf.Deg2Rad;
        direccionDisparo = new Vector2(Mathf.Cos(roundedAngleRad), Mathf.Sin(roundedAngleRad));

        // Selecionar el punto de disparo correspondiente a la dirección redondeada
        if (direccionDisparo == Vector2.up)
        {
            PuntoDisparo = PuntoDisparoArriba;
        }
        else if (direccionDisparo == Vector2.down)
        {
            PuntoDisparo = PuntoDisparoAbajo;
        }
        else if (direccionDisparo == Vector2.left)
        {
            PuntoDisparo = PuntoDisparoIzquierda;
        }
        else if (direccionDisparo == Vector2.right)
        {
            PuntoDisparo = PuntoDisparoDerecha;
        }
        else if (direccionDisparo == new Vector2(1, 1).normalized)
        {
            PuntoDisparo = PuntoDisparoArribaDerecha;
        }
        else if (direccionDisparo == new Vector2(-1, 1).normalized)
        {
            PuntoDisparo = PuntoDisparoArribaIzquierda;
        }
        else if (direccionDisparo == new Vector2(1, -1).normalized)
        {
            PuntoDisparo = PuntoDisparoAbajoDerecha;
        }
        else if (direccionDisparo == new Vector2(-1, -1).normalized)
        {
            PuntoDisparo = PuntoDisparoAbajoIzquierda;
        }

        // Aquí agregamos un ajuste a la posición del punto de disparo según la dirección del disparo
        PuntoDisparo.localPosition = direccionDisparo;

        // Crear la bala en la posición del punto de disparo
        bala = Instantiate(Bala, PuntoDisparo.position, Quaternion.identity);

        // Obtener el componente Rigidbody2D de la bala
        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

        // Aplicar una fuerza para disparar la bala en la dirección adecuada
        rbBala.AddForce(direccionDisparo * veldisparo, ForceMode2D.Impulse);
    }

}
