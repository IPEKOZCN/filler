using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabTile;
    
    public GameObject[,] grid;

    public int xSize,ySize;

    public static bool newGame;

    void Start()
    {
        xSize = PlayerPrefs.GetInt("xSize", xSize);
        ySize = PlayerPrefs.GetInt("ySize", ySize);
        
        CreateBoard(xSize, ySize);
        

    }

    private void ColorTile(int x, int y, Color color)
    {
        grid[x, y].GetComponent<SpriteRenderer>().color = color;
    }

    private Vector2 GetSize(GameObject tile)
    {
        return new Vector2(tile.GetComponent<SpriteRenderer>().bounds.size.x, tile.GetComponent<SpriteRenderer>().bounds.size.y);
    }

    public void CreateBoard(int width, int height)
    {
        Color[] possibleColors = { Color.green, Color.yellow, Color.blue, Color.red };

        grid = new GameObject[width, height];
        //Vector2 startPos = this.transform.position;

        // Calculate the starting position to center the grid on the screen
        Vector2 startPos = new Vector2(-((GetSize(prefabTile).x * width) / 2), -((GetSize(prefabTile).y * height) / 2));
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject tile = Instantiate(prefabTile);
                tile.transform.parent = this.transform;
                tile.transform.position = new Vector2(startPos.x + (GetSize(prefabTile).x * x), startPos.y + (GetSize(prefabTile).y * y));
                grid[x, y] = tile;
                grid[x, y].GetComponent<SpriteRenderer>().color = possibleColors[Random.Range(0, possibleColors.Length)];
            }


        }


    }
}