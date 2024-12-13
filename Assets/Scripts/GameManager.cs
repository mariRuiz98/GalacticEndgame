using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameTime = 60;
    // pauseMenu
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;
    private float timer;
    //private Text timerText;
    //private Text scoreText;
    public int score { get; private set; }
    public int stonesCaptured { get; private set; }
    public int asteroidsDestroyed { get; private set; }
    // private int score;
    // private int stonesCaptured;
    // private int asteroidsDestroyed;


    private bool isGameOver = false;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI stonesText;
    [SerializeField] private TextMeshProUGUI timeText;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = gameTime;
        pauseMenu.SetActive(false);  // Ocultar el menú de pausa
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime;
            timeText.text = timer.ToString("F0");
            // timerText.text = "Time: " + timer.ToString();
            // scoreText.text = "Score: " + score.ToString();
            
            if (timer <= 0)
            {
                timer = 0;
                EndGame();
            }
            // Detectar si se presiona Escape
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    // Método para sumar los Asteroides destruidos
    public void AddPoint(int points)
    {
        score += points;
        asteroidsDestroyed += points;
        scoreText.text = "SCORE: " + asteroidsDestroyed.ToString();
    }

    public void AddStone(int stones)
    {
        score += stones;
        stonesCaptured += stones;
        stonesText.text = "STONES: " + stonesCaptured.ToString();
    }
 
    public void EndGame()
    {
        isGameOver = true;
        PlayerPrefs.SetInt("Score", score);
        Debug.Log("Score: " + score);
        PlayerPrefs.SetInt("StonesCaptured", stonesCaptured);
        Debug.Log("Stones Captured: " + stonesCaptured);
        PlayerPrefs.SetInt("AsteroidsDestroyed", asteroidsDestroyed);
        Debug.Log("Asteroids Destroyed: " + asteroidsDestroyed);
        PlayerPrefs.Save();
        Debug.Log("¡Tiempo agotado! Fin del juego.");
        SceneManager.LoadScene("EndGame");
        //Time.timeScale = 0;  // Esto pausa el juego
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);  // Mostrar el menú de pausa
        Time.timeScale = 0; // Pausar el juego

        // Habilitar el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;  // Reanudar el juego

        // Deshabilitar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
