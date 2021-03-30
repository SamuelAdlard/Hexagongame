using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool Building = false;
    public void Startbuilding()
    {
        Building = true;
        print("Hello");
    }

    public void Endbuilding()
    {
        Building = false;
    }
    void Update()
    {
        if(Building == true)
        {
            
        }
    }
}
