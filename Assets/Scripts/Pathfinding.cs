using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pathfinding : MonoBehaviour
{
    
    GameObject[,] Hexes;

    public Material GREEN;
    public Material RED;
    public Material BLUE;
    public Vector2 start;
    public Vector2 end;
    Exploredhex[] exploredhexes = new Exploredhex[5000];
    List<Frontier> frontier = new List<Frontier>();
    List<Vector2> path = new List<Vector2>();
    List<Exploredhex> exploredhexlist = new List<Exploredhex>();
    bool FoundEnd = false;
    bool[,] Isfound = new bool[100,100];
    int i = 0;
    Vector2 lasthex;
    public void Gethexes()
    {
        Hexes = GameObject.Find("Terrain").GetComponent<HexagonGeneration>().Hexagons;
    }

    public void Startpathfinding()
    {
        lasthex = end;
        while (FoundEnd == false)
        {
            Pathfind();
        }
        path.Add(end);
        
        foreach (Exploredhex item in exploredhexes)
        {
            if(item != null)
            {
                exploredhexlist.Add(item);
            }
        }
        Getpath();
        
        
    }
    void Pathfind()
    {


        exploredhexes[0] = new Exploredhex() { hexexplored = start, hexorigin = start };
        Vector2 hex;


        hex = exploredhexes[i].hexexplored;
        
        if (hex.y % 2 == 0)
        {
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x - 1, hex.y),origin = hex });
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x - 1, hex.y - 1), origin = hex });
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x - 1, hex.y + 1), origin = hex });
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x, hex.y + 1), origin = hex });
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x, hex.y - 1), origin = hex });
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x + 1, hex.y), origin = hex });
        }
        else
        {
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x - 1, hex.y), origin = hex });
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x + 1, hex.y - 1), origin = hex });
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x + 1, hex.y + 1), origin = hex });
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x, hex.y + 1), origin = hex });
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x, hex.y - 1), origin = hex });
            frontier.Add(new Frontier() { frontierhex = new Vector2(hex.x + 1, hex.y), origin = hex });
        }
        float bestdistance = Mathf.Infinity;
        Frontier Besthex = new Frontier() { frontierhex = new Vector2(0,0),origin = new Vector2(0, 0) };

        
        foreach (Frontier item in frontier)
        {

            if (Hexes[Mathf.RoundToInt(item.frontierhex.x), Mathf.RoundToInt(item.frontierhex.y)] != null && Isfound[Mathf.RoundToInt(item.frontierhex.x), Mathf.RoundToInt(item.frontierhex.y)] != true)
            {
                
                float distancetogoal = Vector2.Distance(item.frontierhex, end);
                float distancetostart = Vector2.Distance(item.frontierhex, start);
                if (distancetogoal + distancetostart < bestdistance)
                {
                    bestdistance = distancetogoal + distancetostart;
                    Besthex = item;
                }
            }
        }
        Isfound[Mathf.RoundToInt(Besthex.frontierhex.x), Mathf.RoundToInt(Besthex.frontierhex.y)] = true;
        frontier.Remove(Besthex);
        if (Besthex.frontierhex == end)
        {
            FoundEnd = true;
        }
        

        i++;
        exploredhexes[i] = new Exploredhex() { hexexplored = Besthex.frontierhex, hexorigin = Besthex.origin };
    }

    void Getpath()
    {



        for (int a = 0; a < exploredhexlist.Count; a++)
        {
            foreach (Exploredhex item2 in exploredhexlist)
            {
                if (item2.hexexplored == lasthex)
                {
                    path.Add(item2.hexorigin);
                    lasthex = item2.hexorigin;

                }
                
            }
            if (lasthex == start)
            {
                break;
            }
        }
        
        
        foreach (Vector2 item in path)
        {
            Hexes[Mathf.RoundToInt(item.x), Mathf.RoundToInt(item.y)].GetComponent<MeshRenderer>().material = BLUE;
        }
        
        
    }
    void Resetpath()
    {
        i = 0;
        
    }
}


class Frontier
{
    public Vector2 frontierhex;

    public Vector2 origin;



}


class Exploredhex
{
    public Vector2 hexexplored;

    public Vector2 hexorigin;



}
