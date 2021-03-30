
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    bool Start1 = true;
    Vector3 Groundplacement;
    public GameObject Grass;
    public GameObject Mountain;
    public GameObject Desert;
    public GameObject Tundra;
   
    Transform Control;

    private void Update()
    {
        if (Control.position.y == 0)
        {
            Start1 = false;
            
        }
    }
    void Awake()
    {
        Control = GameObject.Find("Control").transform;
        if (Control.position.y != 0)
        {
            InvokeRepeating("TilePos", 0, 0.5f);

        }
            
    }
    void TilePos()
    {
        if(Start1 == false)
        {
            CancelInvoke();
            Destroy(this);
        }
        
        Vector3 Up = new Vector3(-3.4f, 0, 0) + transform.position;
        Vector3 Down = new Vector3(3.4f, 0, 0) + transform.position;
        Vector3 UpLeft = new Vector3(-1.7f, 0, 3) + transform.position;
        Vector3 DownLeft = new Vector3(1.7f, 0, 3) + transform.position;
        Vector3 UpRight = new Vector3(-1.7f, 0, -3) + transform.position;
        Vector3 DownRight = new Vector3(1.7f, 0, -3) + transform.position;
        float Sides = Random.Range(1, 6);
        int Side = Mathf.RoundToInt(Sides);
        if (Side == 1 && !Physics.Raycast(new Vector3(-3.4f, 10, 0) + transform.position, Vector3.down, 20))
        {
            Groundplacement = Up;
            Create();
        }
        if (Side == 2 && !Physics.Raycast(new Vector3(3.4f, 10, 0) + transform.position, Vector3.down, 20))
        {
            Groundplacement = Down;
            Create();
        }
        if (Side == 3 && !Physics.Raycast(new Vector3(-1.7f, 10, 3) + transform.position, Vector3.down, 20))
        {
            Groundplacement = UpLeft;
            Create();
        }
        if (Side == 4 && !Physics.Raycast(new Vector3(1.7f, 10, 3) + transform.position, Vector3.down, 20))
        {
            Groundplacement = DownLeft;
            Create();
        }
        if (Side == 5 && !Physics.Raycast(new Vector3(-1.7f, 10, -3) + transform.position, Vector3.down, 20))
        {
            Groundplacement = UpRight;
            Create();
        }
        if (Side == 6 && !Physics.Raycast(new Vector3(1.7f, 10, -3) + transform.position, Vector3.down, 20))
        {
            Groundplacement = DownRight;
            Create();
        }
        
    }

    void Create()
    {
        float Tile = Random.Range(0,10);
        
        int Tilepiece = Mathf.RoundToInt(Tile);
        

        if (Tilepiece >= 1)
        {
            int Sandorgrass = Mathf.RoundToInt(Random.Range(0, 10));
            if(Sandorgrass >= 1)
            {
                Instantiate(Grass, Groundplacement, Quaternion.identity);
            }
            else
            {
                float trueorfalse = Random.Range(1.4f, 1.6f);
                int intbool = Mathf.RoundToInt(trueorfalse);
                if (intbool == 1)
                {
                    Instantiate(Tundra, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Desert, Groundplacement, Quaternion.identity);
                }
                        
            }
        }
        else
        {
            Instantiate(Mountain, Groundplacement, Quaternion.identity);
            
        }
       
    
    }
}
