using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    private int cantidadMaximaSpawn=1;
    public GameObject enemyNormalPrefab;
    public GameObject enemyLargePrefab;
    // Start is called before the first frame update
    void Start()
    {
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
            Vector2 posicion = new Vector2(Random.Range(-10.5f, 3.5f), 4f);
                Instantiate(enemyNormalPrefab, posicion, Quaternion.identity);
                Instantiate(enemyLargePrefab, posicion, Quaternion.identity);
            yield return new WaitForSecondsRealtime(1);
        }
    }
    
}
