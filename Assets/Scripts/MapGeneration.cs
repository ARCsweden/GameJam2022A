using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGeneration : MonoBehaviour
{
    [SerializeField]
    float[,,] chance;
    [SerializeField,Range(0,6)]
    int showBiome;
    int checkBiome;

    float[,,] biome;

    public GameObject castle;
    public Terrain terrain;

    public int itemsToPlace = 6;
    public Vector2Int spread = new Vector2Int(500, 500);
    public Vector2Int border = new Vector2Int(100, 100);
    public float minimunDistance = 200;

    public int imageScale = 20;
    public int blurSize = 2;
    public int circleSize = 5;

    //private Vector2[] castlePos = new Vector2[itemsToPlace];
    private Vector2Int imageDim;

    // Start is called before the first frame update
    void Start()
    {
        checkBiome = showBiome;
        imageDim = (spread+border) * 2 / imageScale;

        Vector2[] castlePos = GenerateCastle();

        //Texture2D voroni = GenerateVoroni(castlePos);

        biome = GenerateBiome(castlePos);

        terrain.terrainData.size = new Vector3(2*(spread.x + border.x), 600, 2*(spread.y + border.y));
        terrain.terrainData.alphamapResolution = imageDim.x;
        terrain.terrainData.SetAlphamaps(0,0,biome);

        Instantiate(terrain,new Vector3(-(spread.x + border.x),0, -(spread.y + border.y)), Quaternion.identity);

        chance = GenerateChanceMaps(biome);

        //GetComponent<SpriteRenderer>().sprite = Sprite.Create(biome[1], new Rect(0.0f, 0.0f, biome[1].width, biome[1].height), new Vector2(0.5f, 0.5f), imageScale);

        UpdateSprite();
        
        
    }


    private void Update()
    {
        if (checkBiome != showBiome)
        {
            UpdateSprite();
        }
    }

    void UpdateSprite()
    {

        Texture2D picture = new Texture2D(imageDim.x, imageDim.y);

        for (int x = 0; x < imageDim.x; x++)
        {
            for (int y = 0; y < imageDim.y; y++)
            {
                picture.SetPixel(x, y, new Color(biome[x, y, showBiome], biome[x, y, showBiome], biome[x, y, showBiome], 0.3f));
            }
        }

        picture.Apply();
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(picture, new Rect(0.0f, 0.0f, picture.width, picture.height), new Vector2(0.5f, 0.5f), 1f/imageScale);

     }

    Vector2[] GenerateCastle()
    {
        int toGenerate = itemsToPlace;
        List<Vector2> position = new List<Vector2>();
        int tries = 0;

        while (toGenerate > 0)
        {
            float distance = 10000;
            Vector2 next = new Vector2(UnityEngine.Random.Range(-spread.x, spread.x), UnityEngine.Random.Range(-spread.y, spread.y));

            foreach (Vector2 i in position)
            {
                distance = Mathf.Min(Vector2.Distance(i, next), distance);
            }

            if (distance >= minimunDistance || tries > 50)
            {
                position.Add(next);
                Instantiate(castle, new Vector3(next.x, 0, next.y), Quaternion.identity);
                toGenerate--;
            }

            tries++;
        }

        return position.ToArray();
    }


    float[,,] GenerateBiome(Vector2[] castlePos)
    {
        LinearBlur blur = new LinearBlur();

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

                    if (Vector2.Distance((castlePos[i] + spread + border) / imageScale, new Vector2(x, y)) < distance) { index = i; }
                    distance = Mathf.Min(Vector2.Distance((castlePos[i] + spread + border) / imageScale, new Vector2(x, y)), distance);
                }

                voroni[index].SetPixel(x, y, new Color(circleSize / (distance * imageScale), circleSize / (distance * imageScale), circleSize / (distance * imageScale), 1f));
            }
        }

        float[,,] map = new float[imageDim.x, imageDim.y, itemsToPlace+1];

        for (int i = 0; i < itemsToPlace; i++)
        {
            voroni[i] = blur.Blur(voroni[i], blurSize / imageScale, 1);

            voroni[i].Apply();   
        }

        for (int x = 0; x < imageDim.x; x++)
        { 
            for (int y = 0; y < imageDim.y; y++)
            {
                float[] big = { 0, 0, 0, 0, 0, 0 };

                for (int i = 0; i < itemsToPlace; i++)
                {
                    map[y, x, i] = voroni[i].GetPixel(x, y).b;
                    big[i] = map[y, x, i];
                }
                map[y, x, 6] = 1 - Mathf.Max(big);
            }
        }

        return map;
    }

    //generates a map that is a combination of PerlinNoise and biome
    //to get it to game coordinats scale with imageScale
    float[,,] GenerateChanceMaps(float[,,]  biome) 
    {
        float[,,] map = biome;

        for (int i = 0; i < itemsToPlace+1; i++)
        {

            for (int x = 0; x < imageDim.x; x++)
            {
                for (int y = 0; y < imageDim.y; y++)
                {
                    map[x,y,i] = Mathf.PerlinNoise(imageScale * x / 20f, imageScale * y / 20f) * map[x, y, i];
                }
            }

        }

        return map;
    }


}
