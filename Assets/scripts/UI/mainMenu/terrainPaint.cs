using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainPaint : MonoBehaviour
{
    public Terrain terrain;
    public int colorRadious;
    public Color color;
    Texture2D[] texture;

    private void Start()
    {
        texture=terrain.terrainData.alphamapTextures;
    }

    public void paintAtPosition(int x,int y)
    {
        for (int X=0; X< texture[0].width;X++)
        {
            for (int Y = 0; Y < texture[0].height; Y++)
            {
                //texture[0].SetPixel(X,Y, color);
                //texture = terrain.terrainData.SetAlphamaps(X,Y,);
            }
        }

        
    }

}
