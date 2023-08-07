using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salaManager : MonoBehaviour
{
    public int contadorBichos = 0;
    public bool enemigoMuerto = false;
    private bool ganaste = false;
    // Start is called before the first frame update
    void Start()
    {
        // Obtener una referencia al objeto con el script salaManager
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contadorBichos == 0 && enemigoMuerto == true && ganaste == false)
        {
            Debug.Log("ganaste");
            ganaste=true;
        }
    }
}
