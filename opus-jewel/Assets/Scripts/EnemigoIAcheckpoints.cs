using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//IA CON CHECKPOINTS
public class EnemigoIAcheckpoints : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer;
    public bool alcanzoPunto;

    private float limiteAbajo=-4.6f;
    private float limiteArriba=4.6f;
    private float limiteIzquierdo=-8.8f;
    private float limiteDerecho=8.8f;
    private EnemigoCiego enemigociego;
    void Start()
    {
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
        alcanzoPunto = false;
        enemigociego = GetComponent<EnemigoCiego>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfOnMap();
        if(!enemigociego.atacando){
            if (!alcanzoPunto && CheckIfOnMap() )
            {
                transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velocidadMovimiento * Time.deltaTime);

                if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
                {
                    alcanzoPunto = true;
                }
            }
            else
            {
                numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
                Girar();
                alcanzoPunto = false;
            }
            
        }
    }
    private bool CheckIfOnMap()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        return x >= limiteIzquierdo && x <= limiteDerecho && y >= limiteAbajo && y <= limiteArriba;
    }
    private void Girar(){
        if (transform.position.x < puntosMovimiento[numeroAleatorio].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    
    
}
