using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex
{
    
    
    static float Oppositelength;
    static float xpoint;
    public static float radius = 1;
    public static Mesh Generatehex()
    {

        Mesh mesh = new Mesh();
        Vector3[] newVertices = new Vector3[7];
        Oppositelength = radius / 2;
        xpoint = Mathf.Sqrt(radius * radius - Oppositelength * Oppositelength);

        newVertices[0] = new Vector3(0, 0.1f, 0);
        newVertices[1] = new Vector3(0, 0, 1);
        newVertices[2] = new Vector3(xpoint, 0, Oppositelength);
        newVertices[3] = new Vector3(xpoint, 0, -Oppositelength);
        newVertices[4] = new Vector3(0, 0, -1);
        newVertices[5] = new Vector3(-xpoint, 0, -Oppositelength);
        newVertices[6] = new Vector3(-xpoint, 0, Oppositelength);

        mesh.vertices = newVertices;
        mesh.RecalculateBounds();

        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6, 0, 6, 1 };
        mesh.uv = UV(0,2);
        mesh.RecalculateNormals();

        return mesh;
    }
    static Vector2[] UV(int Chunkx, int Chunky)
    {
        const float chunksize = 0.25f;
        float UVstartx;
        float UVstarty;
        float UVendx;
        float UVendy;

        UVstartx = chunksize * Chunkx + 0.05f;
        UVstarty = chunksize * Chunky + 0.05f;
        UVendx = UVstartx + chunksize - 0.1f;
        UVendy = UVstarty + chunksize - 0.1f;
        return new Vector2[] {
            new Vector2((UVstartx + UVendx) / 2, (UVstarty + UVendy) / 2),
            new Vector2((UVstartx + UVendx) / 2, UVendy),
            new Vector2(UVendx,UVendy),
            new Vector2(UVendx, UVstarty),
            new Vector2((UVstartx + UVendx) / 2 , UVstarty),
            new Vector2(UVstartx, UVstarty),
            new Vector3(UVstartx, UVendy),
            


        };
    }

}
