using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI stonesText;
    [SerializeField] private TextMeshProUGUI asteroidsText;

    // Start is called before the first frame update
    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        int stonesCaptured = PlayerPrefs.GetInt("StonesCaptured", 0);
        int asteroidsDestroyed = PlayerPrefs.GetInt("AsteroidsDestroyed", 0);
        Debug.Log(stonesCaptured + asteroidsDestroyed);
        scoreText.text = "TOTAL SCORE: " + score;
        stonesText.text = "Total Infinity Stones: " + stonesCaptured;
        asteroidsText.text = "Total Asteroids Destroyed: " + asteroidsDestroyed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
