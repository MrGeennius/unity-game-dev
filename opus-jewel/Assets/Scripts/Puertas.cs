using UnityEngine;
using System.Collections;

public class Puertas : MonoBehaviour
{
    private salaManager salaManager;
    public salaManager SalaManagerReference;
    public float tiempoEspera = 1.2f;
    public Transform puntoTeletransporte;
    void Start()
    {
        salaManager = SalaManagerReference; // Inicializa la referencia al script salaManager
    }


    void Update()
    {

    }
    
    // public Puertas puertaDestino; // Referencia a la puerta de destino
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            if (salaManager != null && salaManager.ganaste && PuertasManager.puedeTeletransportar) // Verificar si el nivel ha sido vencido
            {
                // Teletransportar al jugador a la puerta de destino
                PuertasManager.puedeTeletransportar = false;
                collision.transform.position = puntoTeletransporte.transform.position;
                salaManager.terminada = true;
                Camera.main.transform.position = new Vector3(-22, 0, -10);
                StartCoroutine(HabilitarTeletransporte());
            }
        }
    }
    private IEnumerator HabilitarTeletransporte()
    {
        yield return new WaitForSeconds(tiempoEspera);
        PuertasManager.puedeTeletransportar = true;
    }

}