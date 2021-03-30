using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public float OreAmount = 10;
    public GameObject OreObject;
    private void Awake()
    {

        float yesorno = Random.Range(0, OreAmount);
        int Oreornottoore = Mathf.RoundToInt(yesorno);
        if (Oreornottoore < 1)
        {
            Instantiate(OreObject, transform.position, Quaternion.identity);
        }
    }
}
