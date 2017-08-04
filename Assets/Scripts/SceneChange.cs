using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        if (Scoring.CurrentScore > Scoring.GetHighScore)
        {
            Scoring.Write(Scoring.CurrentScore);
            Scoring.Read();
            Facebook.Unity.FB.API("me/scores", Facebook.Unity.HttpMethod.POST);
        }
        SceneManager.LoadScene("End");
    }

    public void goToScore()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void goToFight()
    {
        SceneManager.LoadScene("Fight");
    }
}