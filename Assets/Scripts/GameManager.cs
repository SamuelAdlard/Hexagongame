using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    GameObject Ghostbuilding;
    public Camera cam;
    bool Building = false;
    public GameObject Terrain;
    public GameObject Stopbuilding;
    public GameObject startbuilding;
    public GameObject Buildingmenu;
    public GameObject[] BuildingOptions;
    public int[] Buildingprices;
    public Material Red, Green;
    
    Dropdown Buildingdropdown;
    GameObject currentbuilding;
    int Dropdownvalue;
    public int Money;
    public void Startbuilding()
    {
        Building = true;
        
        startbuilding.SetActive(false);
        Stopbuilding.SetActive(true);
        Buildingmenu.SetActive(false);
        Buildingdropdown = Buildingmenu.GetComponent<Dropdown>();
        Dropdownvalue = Buildingdropdown.value;
        currentbuilding = BuildingOptions[Dropdownvalue];
        Ghostbuilding = Instantiate(BuildingOptions[Dropdownvalue]);
        Destroy(Ghostbuilding.GetComponent<BoxCollider>());
        Ghostbuilding.tag = "Ghostbuilding";
    }
    
    public void Endbuilding()
    {
        Building = false;
        startbuilding.SetActive(true);
        Stopbuilding.SetActive(false);
        Buildingmenu.SetActive(true);
        Destroy(Ghostbuilding);
    }
    
    void Update()
    {
        
        if(Building == true)
        {
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                Ray buildingray = new Ray(hit.transform.position + new Vector3(0, 5, 0), Vector3.down);
                RaycastHit buildingcheck;
                Physics.Raycast(buildingray, out buildingcheck);


                Ghostbuilding.transform.position = hit.transform.position + new Vector3(0, 0.25f, 0);
                if(Ghostbuilding.gameObject.layer != 9)
                {
                    if (Buildingdistance(hit.transform.position) < 4 && hit.transform.tag != "Forest" && hit.transform.gameObject.layer == 8 && buildingcheck.transform.tag != "Building" && Money >= Buildingprices[Dropdownvalue])
                    {
                        Ghostbuilding.GetComponent<MeshRenderer>().material = Green;
                        if (Input.GetMouseButtonDown(0))
                        {
                            GameObject placedbuilding = Instantiate(currentbuilding);
                            placedbuilding.transform.position = hit.transform.position + new Vector3(0, 0.25f, 0);
                            Money = Money - Buildingprices[Dropdownvalue];
                        }

                    }
                    else
                    {
                        Ghostbuilding.GetComponent<MeshRenderer>().material = Red;
                    }
                }
                else
                {
                    bool oncoast = false;
                    
                    Ray Sphere = new Ray(hit.transform.position, Vector3.down);
                    RaycastHit[] sphere = Physics.SphereCastAll(Sphere, 2f, 0);
                    foreach (RaycastHit item in sphere)
                    {
                        
                        if(item.transform.tag == "Water")
                        {
                            oncoast = true;
                        }
                        
                        sphere = null;
                    }
                    if(oncoast == true && Buildingdistance(hit.transform.position) < 4 && hit.transform.tag != "Forest" && hit.transform.gameObject.layer == 8 && buildingcheck.transform.tag != "Building" && Money >= Buildingprices[Dropdownvalue])
                    {
                        Ghostbuilding.GetComponent<MeshRenderer>().material = Green;
                        if (Input.GetMouseButtonDown(0))
                        {
                            GameObject placedbuilding = Instantiate(currentbuilding);
                            placedbuilding.transform.position = hit.transform.position + new Vector3(0, 0.25f, 0);
                            Money = Money - Buildingprices[Dropdownvalue];
                        }

                    }
                    else
                    {
                        Ghostbuilding.GetComponent<MeshRenderer>().material = Red;
                    }
                }



            }
            
            
        }
    }
    private float Buildingdistance(Vector3 Targetedhex) 
    {
        GameObject[] Buildings = GameObject.FindGameObjectsWithTag("Building");
        float Closestdistance = Mathf.Infinity;
        foreach (GameObject item in Buildings)
        {
            float Curentdistance = Vector3.Distance(item.transform.position, Targetedhex);
            if(Curentdistance < Closestdistance)
            {
                Closestdistance = Curentdistance;
            }
        }
        return Closestdistance;
    }
        
}
