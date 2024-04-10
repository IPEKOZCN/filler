using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloodFill : MonoBehaviour
{
    [SerializeField] private float fillDelay = 0.01f;
    private BoardManager board;
    private BoardManager backupBoard;
    public Color old;
    public Color neww;
    private int moveCount;
    public TMPro.TextMeshProUGUI moveCount_txt;
    public Color[,] initialColors;
    public int rows, columns;
    private bool check;



    //UI variables
    public GameObject YouWinPanel;

    void Start()
    {
        board = this.GetComponent<BoardManager>();
        backupBoard = this.GetComponent<BoardManager>();

        check = false;
        rows = board.xSize;
        columns = board.ySize;

        initialColors = new Color[board.xSize, board.ySize];

        for (int x = 0; x < board.xSize; x++)
        {
            for (int y = 0; y < board.ySize; y++)
            {
                initialColors[x, y] = board.grid[x, y].GetComponent<SpriteRenderer>().color;
            }
        }

        moveCount = 0;
        moveCount_txt.text = moveCount.ToString();

    }

    public IEnumerator Flood(int x, int y, Color oldColor, Color newColor)
    {
        WaitForSeconds wait = new WaitForSeconds(fillDelay);
        if (x >= 0 && x < board.xSize && y >= 0 && y < board.ySize)
        {
            yield return wait;
            if (board.grid[x, y].GetComponent<SpriteRenderer>().color == oldColor)
            {
                board.grid[x, y].GetComponent<SpriteRenderer>().color = newColor;
                StartCoroutine(Flood(x + 1, y, oldColor, newColor));
                StartCoroutine(Flood(x - 1, y, oldColor, newColor));
                StartCoroutine(Flood(x, y + 1, oldColor, newColor));
                StartCoroutine(Flood(x, y - 1, oldColor, newColor));
            }
        }
    }
    private void CountHowManyMoves(Color newColor, Color oldColor)
    {
        if (newColor == oldColor)
            return;
        else
            moveCount++;
    }

    public void ChangeColorToYellow()
    {
        old = board.grid[0, 0].GetComponent<SpriteRenderer>().color;
        neww = Color.yellow;
        CountHowManyMoves(neww, old);
        StartCoroutine(Flood(0, 0, old, Color.yellow));


    }

    public void ChangeColorToGreen()
    {
        old = board.grid[0, 0].GetComponent<SpriteRenderer>().color;
        neww = Color.green;
        CountHowManyMoves(neww, old);
        StartCoroutine(Flood(0, 0, old, Color.green));
    }

    public void ChangeColorToBlue()
    {
        old = board.grid[0, 0].GetComponent<SpriteRenderer>().color;
        neww = Color.blue;
        CountHowManyMoves(neww, old);

        StartCoroutine(Flood(0, 0, old, Color.blue));
    }

    public void ChangeColorToRed()
    {
        old = board.grid[0, 0].GetComponent<SpriteRenderer>().color;
        neww = Color.red;
        CountHowManyMoves(neww, old);

        StartCoroutine(Flood(0, 0, old, Color.red));
    }


    public void restartButton()
    {
        moveCount = 0;
        for (int x = 0; x < board.xSize; x++)
        {
            for (int y = 0; y < board.ySize; y++)
            {
                board.grid[x, y].GetComponent<SpriteRenderer>().color = initialColors[x, y];
            }
        }
        YouWinPanel.SetActive(false);
    }

    // checks if all the tiles are the same color
    public bool CheckWin()
    {



        Color firstTileColor = board.grid[0, 0].GetComponent<SpriteRenderer>().color;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (board.grid[row, col].GetComponent<SpriteRenderer>().color != firstTileColor)
                {
                    return false;
                }
            }
        }

        return true;
    }


    public void NextLevelButton()
    {
        check = true;
        PlayerPrefs.SetInt("xSize", board.xSize + 1);
        PlayerPrefs.SetInt("ySize", board.ySize + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        YouWinPanel.SetActive(false);

        /*check = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("NextLevel");
        YouWinPanel.SetActive(false);*/


    }

    private void Update()
    {
        moveCount_txt.text = moveCount.ToString();

        // checks if we have won the game
        if (!check && CheckWin())
        {
            YouWinPanel.SetActive(true);
        }
    }
}