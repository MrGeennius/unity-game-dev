using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemigoAiNavmesh : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform objetivo;
    private NavMeshAgent navMeshAgent;
    private Rigidbody2D rb;
    public bool activado = true;
    private float limiteAbajo = -4.6f;
    private float limiteArriba = 4.6f;
    private float limiteIzquierdo = -8.6f;
    private float limiteDerecho = 8.6f;
    private bool isOnMap = false;
    void Start()
    {
        isOnMap=CheckIfOnMap();
        gameObject.SetActive(activado);
        if(isOnMap)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOnMap)
        {
            navMeshAgent.SetDestination(objetivo.position);
        }
        
    }
    private bool CheckIfOnMap()
    {
        float posx = transform.position.x;
        float posy = transform.position.y;
        return posx >= limiteIzquierdo && posx <= limiteDerecho && posy >= limiteAbajo && posy <= limiteArriba;
    }
}
