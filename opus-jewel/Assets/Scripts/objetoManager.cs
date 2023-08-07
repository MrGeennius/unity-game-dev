using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class objetoManager
{
    public GameObject objeto;
    [Range(0f, 1f)]
    public float probabilidad;
}