using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salaManager : MonoBehaviour
{
    public int contadorBichos = 0;
    public bool enemigoMuerto = false;
    public bool ganaste = false;
    public bool terminada = false;
    private Dictionary<string, bool> salasDerrotadas = new Dictionary<string, bool>();
    public List<objetoManager> objetosConProbabilidades;
    private Puertas puertas;
    public string salaActual;
    public bool wavesCompletadas = false;
    // Start is called before the first frame update
    void Start()
    {
        puertas = FindObjectOfType<Puertas>();
    }
        

    // Update is called once per frame
    void Update()
    {
        if (!salasDerrotadas.ContainsKey(salaActual))
        {
            salasDerrotadas.Add(salaActual, false);
        }
        if (wavesCompletadas && !salasDerrotadas[salaActual])
        {
            Debug.Log("ganaste");
            terminarSala();
            ganaste=true;
            MarcarSalaComoDerrotada(salaActual);
            wavesCompletadas = false;
        }
    }
    public void terminarSala()
    {
    // Generar un número aleatorio entre 0 y 1
    float randomValue = Random.value;

    // Recorrer la lista de objetos con probabilidades
        foreach (objetoManager objetoProbabilidad in objetosConProbabilidades)
        {
        // Si el número aleatorio es menor o igual a la probabilidad del objeto, lo seleccionamos
        if (randomValue <= objetoProbabilidad.probabilidad)
        {
            // Instanciar el objeto seleccionado
            Instantiate(objetoProbabilidad.objeto, transform.position, Quaternion.identity);
            break; // Salir del bucle, ya que ya hemos seleccionado un objeto
        }

        // Si el objeto no fue seleccionado, restar su probabilidad al número aleatorio y seguir buscando
        randomValue -= objetoProbabilidad.probabilidad;
        }
    }
    // Método para marcar una sala como derrotada


    public void MarcarSalaComoDerrotada(string salaActual)
    {
        if (salasDerrotadas.ContainsKey(salaActual))
        {
            salasDerrotadas[salaActual] = true;
        }
    }

    public bool SalaFueDerrotada(string salaActual)
    {
        if (salasDerrotadas.ContainsKey(salaActual))
        {
            return salasDerrotadas[salaActual];
        }
        return false;
    }
    public void CambiarSalaActual(string salaDestino)
    {
        salaActual=salaDestino;
    }
}