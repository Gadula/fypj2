using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static Scoring instance;
    public GameObject scoringText;

    private int score = 0;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    private void Start()
    {
        score = 0;
        PlayerPrefs.SetInt("PlayerScore", score);
        scoringText.GetComponent<Text>().text = score.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void UpdateScore()
    {
        PlayerPrefs.SetInt("PlayerScore", score);
    }

    public void AddScore(int score)
    {
        Debug.Log(this.score + " " + score);
        this.score += score;
        scoringText.GetComponent<Text>().text = this.score.ToString();
    }
}