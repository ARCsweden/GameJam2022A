using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGeneration : MonoBehaviour
{
    public GameObject castle;

    public int itemsToPlace = 6;
    public Vector2Int spread = new Vector2Int(20, 20);
    public float minimunDistance = 10;

    public int imageScale = 10;
    public int blurSize = 10;
    public int circleSize = 100;

    //private Vector2[] castlePos = new Vector2[itemsToPlace];

    // Start is called before the first frame update
    void Start()
    {
        Vector2[] castlePos = GenerateCastle();

        //Texture2D voroni = GenerateVoroni(castlePos);

        Texture2D[] biome = GenerateBiome(castlePos);

        GetComponent<SpriteRenderer>().sprite = Sprite.Create(biome[1], new Rect(0.0f, 0.0f, biome[1].width, biome[1].height), new Vector2(0.5f, 0.5f), imageScale);
    }

    Vector2[] GenerateCastle()
    {
        int toGenerate = itemsToPlace;
        List<Vector2> position = new List<Vector2>();

        while (toGenerate > 0)
        {
            float distance = 10000;
            Vector2 next = new Vector2(UnityEngine.Random.Range(-spread.x, spread.x), UnityEngine.Random.Range(-spread.y, spread.y));

            foreach (Vector2 i in position)
            {
                distance = Mathf.Min(Vector2.Distance(i, next), distance);
            }

            if (distance >= minimunDistance)
            {
                position.Add(next);
                Instantiate(castle, new Vector3(next.x, 0, next.y), Quaternion.identity);
                toGenerate--;
            }
        }

        return position.ToArray();
    }

    /*Texture2D GenerateVoroni(Vector2[] castlePos)
    {
        Vector2Int imageDim = spread * 2 * imageScale;
        Vector2Int[] centroids = new Vector2Int[itemsToPlace];
        Color[] regions = new Color[itemsToPlace];

        for (int i = 0; i < itemsToPlace; i++)
        {
            regions[i] = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
        }

        Texture2D voroni = new Texture2D(imageDim.x, imageDim.y);

        for (int x = 0; x < imageDim.x; x++)
        {
            for (int y = 0; y < imageDim.y; y++)
            {
                float distance = 10000;
                int index = 0;

                for (int i = 0; i < itemsToPlace; i++)
                {
                    if (Vector2.Distance((castlePos[i] + spread) * imageScale, new Vector2(x, y)) < distance) { index = i; }
                    distance = Mathf.Min(Vector2.Distance((castlePos[i] + spread) * imageScale, new Vector2(x, y)), distance);
                }

                voroni.SetPixel(x, y, regions[index]);
            }
        }

        voroni.Apply();
        return voroni;
    }*/

    Texture2D[] GenerateBiome(Vector2[] castlePos)
    {
        LinearBlur blur = new LinearBlur();

        Vector2Int imageDim = spread * 2 * imageScale;
        Vector2Int[] centroids = new Vector2Int[itemsToPlace];

        Texture2D[] voroni = new Texture2D[itemsToPlace];

        for (int i = 0; i < itemsToPlace; i++)
        {
            voroni[i] = new Texture2D(imageDim.x, imageDim.y);
        }

        for (int x = 0; x < imageDim.x; x++)
        {
            for (int y = 0; y < imageDim.y; y++)
            {
                float distance = 10000;
                int index = 0;

                for (int i = 0; i < itemsToPlace; i++)
                {
                    voroni[i].SetPixel(x, y, new Color(0f, 0f, 0f, 1f));

                    if (Vector2.Distance((castlePos[i] + spread) * imageScale, new Vector2(x, y)) < distance) { index = i; }
                    distance = Mathf.Min(Vector2.Distance((castlePos[i] + spread) * imageScale, new Vector2(x, y)), distance);
                }

                voroni[index].SetPixel(x, y, new Color(circleSize / distance, circleSize / distance, circleSize / distance, 1f));
            }
        }

        for (int i = 0; i < itemsToPlace; i++)
        {
            voroni[i] = blur.Blur(voroni[i], blurSize, 1);

            voroni[i].Apply();
        }

        return voroni;
    }


}
