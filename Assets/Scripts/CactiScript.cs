using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactiScript : MonoBehaviour
{
    public GameObject CactiObject;
    private void Awake()
    {

        float trueorfalse = Random.Range(0, 10);
        int Cacti = Mathf.RoundToInt(trueorfalse);
        if (Cacti < 1)
        {
            Instantiate(CactiObject, transform.position, Quaternion.identity);
        }
    }
}
