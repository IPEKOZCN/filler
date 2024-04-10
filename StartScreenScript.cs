using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenScript : MonoBehaviour
{
    private BoardManager board;
    // Start is called before the first frame update
    void Start()
    {
        board = this.GetComponent<BoardManager>();
    }

    public void PlayButton()
    {
        PlayerPrefs.SetInt("xSize", 3);
        PlayerPrefs.SetInt("ySize", 3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResumeButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}