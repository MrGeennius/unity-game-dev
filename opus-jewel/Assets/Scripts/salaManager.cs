using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salaManager : MonoBehaviour
{
    public int contadorBichos = 0;
    public bool enemigoMuerto = false;
    public bool ganaste = false;
    public bool terminada = false;
    
    public List<objetoManager> objetosConProbabilidades;
    // Start is called before the first frame update
    void Start()
    {
        // Obtener una referencia al objeto con el script salaManager
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contadorBichos == 0 && enemigoMuerto == true && ganaste == false && terminada == false)
        {
            Debug.Log("ganaste");
            terminarSala();
            ganaste=true;
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
}
