using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise
{
    
    public static float[,] Generatenoise(int width, int height)
    {
        


        float[,] Noisemap = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Noisemap[x, y] = Mathf.PerlinNoise((Seed() + x) * 0.1f, (Seed() + y) * 0.1f) ;
            }
        }
        return Noisemap;
        
    }
    static float Seed()
    {
        
        System.Random random = new System.Random(69420);
        return random.Next() / 100f;
    }

}

