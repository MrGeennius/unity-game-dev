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
     public Transform PuntoDisparo;

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

    void Disparo(){
            bala = Instantiate(Bala, PuntoDisparo.position, Quaternion.identity);
            bala.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,1)*veldisparo);
        }

    private void FixedUpdate(){
    }
}
