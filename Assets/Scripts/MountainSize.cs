using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainSize : MonoBehaviour
{
    private void Awake()
    {
        float Height = Random.Range(2, 10);
        transform.localScale = new Vector3(1, Height, 1); 
    }
}
