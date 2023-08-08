using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    private int cantidadMaximaSpawn=1;
    public GameObject enemyNormalPrefab;
    public GameObject enemyLargePrefab;
    private salaManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<salaManager>();
        StartCoroutine(waves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waves()
    {
        for (int i = 0; i < cantidadMaximaSpawn; i++)
        {
            Vector2 posicion = new Vector2(Random.Range(-8f, 3.5f), 4f);
                Instantiate(enemyNormalPrefab, posicion, Quaternion.identity);
                manager.contadorBichos++;
                Instantiate(enemyLargePrefab, posicion, Quaternion.identity);
                manager.contadorBichos++;
            yield return new WaitForSecondsRealtime(1);
        }
    }
    
}
