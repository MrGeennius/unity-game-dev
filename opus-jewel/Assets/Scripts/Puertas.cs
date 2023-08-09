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
    void Start()
    {
        salaManager = SalaManagerReference; // Inicializa la referencia al script salaManager
        salaManager.salaActual = salaActual;
    }


    void Update()
    {

    }
    
    // public Puertas puertaDestino; // Referencia a la puerta de destino
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            Debug.Log("Colisión detectada");
            Debug.Log("SalaManager bool sala derrotada: "+ salaManager.SalaFueDerrotada(salaActual) );
            Debug.Log(salaManager.salaActual);
            if (salaManager != null && salaManager.ganaste && PuertasManager.puedeTeletransportar && salaManager.SalaFueDerrotada(salaActual)) // Verificar si el nivel ha sido vencido
            {
                
                // Teletransportar al jugador a la puerta de destino
                PuertasManager.puedeTeletransportar = false;
                collision.transform.position = puntoTeletransporte.transform.position;
                // salaManager.terminada = true;
                if (cameraTarget != null)
                {
                    StartCoroutine(MoveCameraSmoothly(cameraTarget.position));
                }
                StartCoroutine(HabilitarTeletransporte());
                // salaManager.CambiarSalaActual(salaDestino);
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