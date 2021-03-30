using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colider : MonoBehaviour
{
    Transform MeshCenter;
    void Awake()
    {
        MeshCenter = GameObject.Find("MeshCenter").transform;
        transform.parent = MeshCenter;
    }

    
    
}
