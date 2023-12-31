using UnityEngine;
using System.Collections;

public class Puertas : MonoBehaviour
{
    private salaManager salaManager;
    public salaManager SalaManagerReference;
    public float tiempoEspera = 1.2f;
    public Transform puntoTeletransporte;
    public Transform cameraTarget;
    private float cameraMoveSpeed = 120f;
    public string salaActual;
    public string salaDestino;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool cambiarColor =true;
    private enemySpawner enemySpawner;
    void Start()
    {
        enemySpawner = GameObject.FindObjectOfType<enemySpawner>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        salaManager = SalaManagerReference; // Inicializa la referencia al script salaManager
        salaManager.salaActual = SalaActual.salaActual;   
    }


    void Update()
    {   
        if (salaManager.salasDerrotadas.ContainsKey(salaActual) && !salaManager.salasDerrotadas.ContainsKey(salaDestino))
        {
            salaManager.salasDerrotadas.Add(salaDestino, false);
        }

        if(salaManager.SalaFueDerrotada(salaActual) && cambiarColor==true)
        {
            spriteRenderer.color = Color.green;
            cambiarColor = false;
        }
    }
    
    // public Puertas puertaDestino; // Referencia a la puerta de destino
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            Debug.Log("Colisión detectada");
            Debug.Log(salaManager.salaActual);
            if (salaManager != null && salaManager.ganaste && PuertasManager.puedeTeletransportar && salaManager.SalaFueDerrotada(salaActual)) // Verificar si el nivel ha sido vencido
            {
                // Teletransportar al jugador a la puerta de destino
                EnemigoMovActivoManager.puedeMoverse = false;
                
                PuertasManager.puedeTeletransportar = false;
                collision.transform.position = puntoTeletransporte.transform.position;
                salaManager.CambiarSalaActual(salaDestino);
                if (cameraTarget != null)
                {
                    StartCoroutine(MoveCameraSmoothly(cameraTarget.position));
                }
                
                StartCoroutine(HabilitarTeletransporte());
                Debug.Log("SalaManager SalasDerrotadas: " + salaManager.salasDerrotadas[salaManager.salaActual]);
                
                enemySpawner.sala2();

            }
        }
    }
    private IEnumerator MoveCameraSmoothly(Vector3 targetPosition)
    {
        Vector3 initialPosition = Camera.main.transform.position;
        float startTime = Time.time;
        float distance = Vector3.Distance(initialPosition, targetPosition);

        while (Camera.main.transform.position != targetPosition)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / (distance / cameraMoveSpeed));
            Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            yield return null;
        }
    }
    private IEnumerator HabilitarTeletransporte()
    {
        yield return new WaitForSeconds(tiempoEspera);
        PuertasManager.puedeTeletransportar = true;
    }
}