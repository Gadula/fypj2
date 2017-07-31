using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public static SceneChange instance;

    public void Awake()
    {
        instance = this;
    }

    public void goToHome()
    {
        //Application.LoadLevel ("Home");
        SceneManager.LoadScene("Home");
    }

    public void goToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void goToHelp()
    {
        SceneManager.LoadScene("Help");
    }

    public void goToEnd()
    {
        SceneManager.LoadScene("End");
    }

    public void goToScore()
    {
        SceneManager.LoadScene("Score");
    }

    public void goToFight()
    {
        SceneManager.LoadScene("Fight");
    }
}