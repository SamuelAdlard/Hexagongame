﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberhut : MonoBehaviour
{
    int Nearbyforests = 0;
    float Moneysent = 0;
    int seconds = 0;
    private void Awake()
    {

        Invoke("findforests", 0.5f);
        InvokeRepeating("Getmoney", 0.6f, 1);
    }
    void findforests()
    {
        
        if (transform.tag == "Building")
        {
            
            Ray Sphere = new Ray(transform.position, Vector3.down);
            RaycastHit[] sphere = Physics.SphereCastAll(Sphere, 1f, 0);
            foreach (RaycastHit item in sphere)
            {

                if (item.transform.tag == "Forest")
                {
                    Nearbyforests++;
                }

                sphere = null;
            }
            
        }
        
    }
    void Getmoney()
    {
        seconds++;
        float Moneygiven = Nearbyforests * 0.025f;
        Moneysent = Moneysent + Moneygiven;
        if(seconds >= 30)
        {
            
            seconds = 0;
            GameObject.Find("GameManager").GetComponent<GameManager>().Money = GameObject.Find("GameManager").GetComponent<GameManager>().Money + Mathf.RoundToInt(Moneysent);
        }
    }
}
