using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI playerAScoreText;
    public TextMeshProUGUI playerBScoreText;
    public TextMeshProUGUI winText;

    private int playerAScore = 0;
    private int playerBScore = 0;

    [SerializeField] private int winningScore = 2;

    private void Start()
    {
        // Ensure all text components are assigned
        if (playerAScoreText == null || playerBScoreText == null || winText == null)
        {
            Debug.LogError("Ensure all TextMeshProUGUI references are assigned in the inspector.");
            return;
        }

        playerAScore = GameManager.PlayerAScore;
        playerBScore = GameManager.PlayerBScore;

        RefreshUI();
        winText.text = ""; // Clear win message
    }

    // Increase score for a specific player
    public void IncreaseScore(string playerTag, int increment)
    {
        if (playerTag == "LeftArea")
        {
            playerAScore += increment;
            CheckWinCondition("Penguin");
            Debug.Log("Bro score");
            SoundManager.Instance.Play(Sounds.Goal);
            StartCoroutine(RestartGame());
        }
        else if (playerTag == "RightArea")
        {
            playerBScore += increment;
            CheckWinCondition("Bro");
            Debug.Log("Penguin score");
            SoundManager.Instance.Play(Sounds.Score);
            StartCoroutine(RestartGame());
        }

        RefreshUI();
    }

    private void CheckWinCondition(string playerName)
    {
        if (GameManager.PlayerAScore >= winningScore || GameManager.PlayerBScore >= winningScore)
        {
            //winText.text = playerName + " Wins!";
            //StartCoroutine(RestartGame());
            SceneManager.LoadScene(2);
        }
    }

    private void RefreshUI()
    {
        playerAScoreText.text = "Penguin:" + playerAScore;
        playerBScoreText.text = "Bro:" + playerBScore;

        Debug.Log($"Refreshing UI: Penguin = {playerAScore}, Bro = {playerBScore}");
    }

    // Reset scores and restart game after a delay
    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.5f);

        // Optionally reset scores
        //playerAScore = 0;
        //playerBScore = 0;
        //winText.text = "";
        //RefreshUI();
        // Save scores before scene reload
        GameManager.PlayerAScore = playerAScore;
        GameManager.PlayerBScore = playerBScore;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }
}