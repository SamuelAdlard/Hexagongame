using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{
    public GameObject ForestObject;
    private void Awake()
    {
        
        
        int Treeornottotree = Mathf.RoundToInt(Random.Range(0, 2));
        if(Treeornottotree < 1)
        {
            Instantiate(ForestObject, transform.position, Quaternion.identity);
        }
    }
}
