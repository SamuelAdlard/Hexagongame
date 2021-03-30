using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Control : MonoBehaviour
{
    
    
    private void Start()
    {
        Invoke("Stop", 4);
    }
    void Stop()
    {
        
            transform.position = new Vector3(0, 0, 0);
            
        
    }
}
