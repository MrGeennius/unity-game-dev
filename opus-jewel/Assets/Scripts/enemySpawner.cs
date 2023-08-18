using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemySpawner : MonoBehaviour
{
    // public int enemigoNormal = 0;
    // public bool activarEnemigoNormal = true;
    // public int enemigoLarge = 0;
    // public bool activarEnemigoLarge = true;
    // public int enemigoCiego = 0;
    // public bool activarEnemigoCiego = true;
    // public int enemigoNavmesh = 0;
    // public bool activarEnemigoNavmesh = true;
    public GameObject enemyNormalPrefab;
    public GameObject enemyLargePrefab;
    public GameObject enemyCiegoPrefab;
    public GameObject enemyNavmeshPrefab;
    private salaManager manager;
    private int ContadorWaves = 4;
    public int ContadorBichos = 0;
    public bool enemigoMuerto = true;
    private EnemigoColisionAI enemigoColisionAI;


    // Start is called before the first frame update
    void Start()
    {
        enemigoColisionAI = GameObject.FindObjectOfType<EnemigoColisionAI>();
        manager = GameObject.FindObjectOfType<salaManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemigoMuerto && ContadorBichos == 0)
        {
            ContadorWaves--;
            // Debug.Log("Wave Numero: "+ ContadorWaves);
            // Debug.Log("Wave Completada: " + manager.wavesCompletadas);
            enemigoMuerto = false;
            StartCoroutine(waves());
        }
    }
    

    IEnumerator waves()
    {
        yield return new WaitForSecondsRealtime(1);

            if(ContadorWaves == 3)
            {
                StartCoroutine(moverse());
                Vector2 posicion = new Vector2(Random.Range(-8f, 3.5f), 4f);
                Instantiate(enemyNormalPrefab, posicion, Quaternion.identity);
                ContadorBichos++;
            }
            if(ContadorWaves == 2)
            {
                Vector2 posicion = new Vector2(Random.Range(-8f, 3.5f), 4f);
                Instantiate(enemyLargePrefab, posicion, Quaternion.identity);
                ContadorBichos++;
            }
            if(ContadorWaves == 1)
            {
                Vector2 posicion = new Vector2(Random.Range(-8f, 3.5f), 4f);
                Instantiate(enemyCiegoPrefab, posicion, Quaternion.identity);
                ContadorBichos++;

            }
            if(ContadorWaves == 0)
            {
                Debug.Log(ContadorWaves);
                manager.wavesCompletadas = true;
            }
            if(ContadorWaves == -1)
            {
                manager.wavesCompletadas = true;
                manager.xObj = -22;
            }
            if(ContadorWaves == -2)
            {
                manager.wavesCompletadas = true;
                manager.yObj = 14;
            }
            
    }

    public void sala2()
    {   StartCoroutine(moverse());
        if (!manager.salasDerrotadas[manager.salaActual] && manager.salaActual == "Sala2")
        {
            
            Vector2 posicion = new Vector2(Random.Range(-26, -18), Random.Range(-4, 4) );
            Instantiate(enemyNormalPrefab, posicion, Quaternion.identity);
    
            ContadorBichos++;
            posicion = new Vector2(Random.Range(-26, -18), Random.Range(-4, 4) );
            Instantiate(enemyNormalPrefab, posicion, Quaternion.identity);
    
            ContadorBichos++;
            posicion = new Vector2(Random.Range(-26, -18), Random.Range(-4, 4) );
            Instantiate(enemyNormalPrefab, posicion, Quaternion.identity);
            ContadorBichos++;
        }
        if (!manager.salasDerrotadas[manager.salaActual] && manager.salaActual == "Sala3")
        {
            
            Vector2 posicion = new Vector2(Random.Range(-26, -18), Random.Range(13, 17) );
            Instantiate(enemyNormalPrefab, posicion, Quaternion.identity);
    
            ContadorBichos++;
            posicion = new Vector2(Random.Range(-26, -18), Random.Range(13, 17) );
            Instantiate(enemyNormalPrefab, posicion, Quaternion.identity);
    
            ContadorBichos++;
            posicion = new Vector2(Random.Range(-26, -18), Random.Range(13, 17) );
            Instantiate(enemyNormalPrefab, posicion, Quaternion.identity);
            ContadorBichos++;
        }
    }
    

    IEnumerator moverse()
    {
        EnemigoMovActivoManager.puedeMoverse = false;
        yield return new WaitForSecondsRealtime(1f);
        EnemigoMovActivoManager.puedeMoverse = true;
    }

}

  

//  if (activarEnemigoNormal)
//                 {
                    
//                     for(int i=0;i<enemigoNormal;i++)
//                     {
//                         Vector2 posicion = new Vector2(Random.Range(-8f, 3.5f), 4f);
//                         Instantiate(enemyNormalPrefab, posicion, Quaternion.identity);
//                         manager.contadorBichos++;
//                     }
//                 }
//                 if (activarEnemigoLarge)
//                 {
                    
//                     for(int i=0;i<enemigoLarge;i++)
//                     {
//                         Vector2 posicion = new Vector2(Random.Range(-8f, 3.5f), 4f);
//                         Instantiate(enemyLargePrefab, posicion, Quaternion.identity);
//                         manager.contadorBichos++;
//                     }
//                 }
//                 if (activarEnemigoCiego)
//                 {
                    
//                     for(int i=0;i<enemigoCiego;i++)
//                     {
//                         Vector2 posicion = new Vector2(Random.Range(-8f, 3.5f), 4f);
//                         Instantiate(enemyCiegoPrefab, posicion, Quaternion.identity);
//                         manager.contadorBichos++;
//                     }
//                 }
//                 if (activarEnemigoNormal)
//                 {
                    
//                     for(int i=0;i<enemigoNavmesh;i++)
//                     {
//                         Vector2 posicion = new Vector2(Random.Range(-8f, 3.5f), 4f);
//                         Instantiate(enemyNavmeshPrefab, posicion, Quaternion.identity);
//                         manager.contadorBichos++;
//                     }
//                 }
