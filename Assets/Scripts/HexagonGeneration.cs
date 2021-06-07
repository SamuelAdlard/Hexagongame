using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGeneration : MonoBehaviour
{
    public Material Textureatlas, water;
    public float Sandlevel, Grasslevel, Mountainlevel, Snowlevel;
    public GameObject[,] Hexagons = new GameObject[100,100];
    public GameObject Tree, Ore, Fruit, Towncenter;
    public GameObject Waterlayer;
    void Start()
    {
        
        float[,] Noisemap = Noise.Generatenoise(Hexagons.GetLength(0), Hexagons.GetLength(1));

        
        float Xpos = 0;
        float Zpos = 0;
        float Ypos = 0;

        for (int x = 0; x < Hexagons.GetLength(0); x++)
        {
            for (int y = 0; y < Hexagons.GetLength(1); y++)
            {
                //Gets the value from the noisemap and sets it as the height
                Zpos = Zpos + Noisemap[x, y] * 10;
                //Creates all the hexagons and puts them in the array
                GameObject Hexagon = new GameObject();
                //Changes the name of the gameobject to the cordinates
                if(x == 1)
                {
                    Destroy(Hexagon);
                }
                Hexagon.name = $"Hex x:{x.ToString("00")}y:{y.ToString("00")}";
                //Adds the hex script the the gameobject
                Hexagon.AddComponent<MeshFilter>();
                Hexagon.AddComponent<MeshRenderer>();
                Hexagon.layer = 8;
                
                if (Noisemap[x, y] < Sandlevel || x == 0 || y == 0 || x == Hexagons.GetLength(0) - 1 || y == Hexagons.GetLength(1) - 1)
                {
                    
                    
                    Hexagon.GetComponent<MeshFilter>().mesh = Hex.Generatehex();
                    Hexagon.transform.parent = Waterlayer.transform;
                    Hexagon.transform.tag = "Water";
                    Zpos = 3f;

                    Hexagon.GetComponent<MeshRenderer>().material = Textureatlas;
                }
                

                Hexagon.transform.position = new Vector3(Xpos, Zpos, Ypos);
                //Add the Gameobject to the array 
                Hexagons[x, y] = Hexagon;


                Terrainheight(Hexagon, Noisemap, x, y);
                
                



                if (y % 2 == 0)
                {
                    Xpos = Xpos + 0.86f;
                }
                else
                {
                    Xpos = Xpos + -0.86f;
                }

                Zpos = 0;
                Ypos = Ypos + -1.5f;
                
                if (x == 0)
                {
                    Destroy(Hexagon);
                }
                Hexagon.AddComponent<MeshCollider>().convex = true;
            }

            Xpos = 1.72f * x;
            Ypos = 0;
            
            
        }
        Waterlayer.GetComponent<Meshcombine>().Combine();
        foreach (GameObject item in Hexagons)
        {
            if(item.transform.tag == "Water")
            {
                Destroy(item);
            }
        }
        Placecenter();
    }
    
    void Terrainheight(GameObject Hex, float[,] Noisemap,int x, int y)
    {
        if(x != 1)
        {
            if (Noisemap[x, y] > Sandlevel && Noisemap[x, y] < Grasslevel || x == 0 || y == 0 || x == Hexagons.GetLength(0) - 1 || y == Hexagons.GetLength(1) - 1)
            {
                //Sets the uv cordinates for the texture atlas
                Hex.GetComponent<MeshFilter>().mesh = Hexwithside.Generatehex(0, 3);
                Hex.GetComponent<MeshRenderer>().material = Textureatlas;

            }
            else if (Noisemap[x, y] > Grasslevel && Noisemap[x, y] < Mountainlevel)
            {
                Hex.GetComponent<MeshFilter>().mesh = Hexwithside.Generatehex(1, 3);
                Hex.GetComponent<MeshRenderer>().material = Textureatlas;

                //Generates a random number to see if there will be a forest on this hex
                if (Random.Range(0, 2) == 1 && Hex.tag != "Water")
                {

                    Hex.GetComponent<MeshRenderer>().material = Textureatlas;
                    Instantiate(Tree, Hex.transform.position + new Vector3(0.2f, 0.66f, -0.5f), Quaternion.Euler(-90, 0, 0));
                    Hex.tag = "Forest";
                }
                else if (Hex.tag != "Water")
                {
                    Hex.tag = "Grass";
                }
            }
            else if (Noisemap[x, y] > Mountainlevel && Noisemap[x, y] < Snowlevel && Hex.tag != "Water")
            {
                Hex.GetComponent<MeshFilter>().mesh = Hexwithside.Generatehex(2, 3);
                Hex.GetComponent<MeshRenderer>().material = Textureatlas;
                //creates the ore
                if (Random.Range(0, 20) == 1)
                {
                    Instantiate(Ore, Hex.transform.position + new Vector3(0f, 0.09f, 0.5f), Quaternion.Euler(-90, 0, 0));
                    Hex.tag = "Ore";
                }

            }
            else if (Noisemap[x, y] > Snowlevel)
            {
                Hex.GetComponent<MeshFilter>().mesh = Hexwithside.Generatehex(0, 0);
                Hex.GetComponent<MeshRenderer>().material = Textureatlas;
            }
        }
        

        //sets the position of the hexagon
        GameObject.Find("Pathfinding").GetComponent<Pathfinding>().Gethexes();
    }

    void Placecenter()
    {
        bool hasplaced = false;
        int x = 0;
        int y = 0;
        while(hasplaced == false && x < Hexagons.GetLength(0))
        {
            
            
            
            if (Hexagons[x,y].transform.tag == "Grass")
            {
                    
                GameObject TownCenter = Instantiate(Towncenter, Hexagons[x, y].transform.position + new Vector3(0,0.23f,0), Quaternion.Euler(-90,0,0));
                
                hasplaced = true;
            }
            y++;
            
            x++;
        }
    }


}
