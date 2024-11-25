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

       

        Debug.Log($"Initial Player A Score: {GameManager.PlayerAScore}");
        Debug.Log($"Initial Player B Score: {GameManager.PlayerBScore}");

        RefreshUI();
        winText.text = ""; // Clear win message
    }

    

    public void IncreaseScore(string playerTag, int increment)
    {
        if (playerTag == "RightArea")
        {
            playerAScore += increment;
            GameManager.PlayerAScore = playerAScore;
            Debug.Log("Bro score");
            SoundManager.Instance.Play(Sounds.Goal);

            // Check win condition
            if (GameManager.PlayerAScore >= winningScore)
            {
                Debug.Log("Player A (Bro) Wins! Loading Scene 3...");
                SceneManager.LoadScene(2); // Change to the correct winning scene
                return; // Exit method to prevent further actions
            }

            StartCoroutine(RestartGame());
        }
        else if (playerTag == "LeftArea")
        {
            playerBScore += increment;
            GameManager.PlayerBScore = playerBScore;
            Debug.Log("Penguin score");
            SoundManager.Instance.Play(Sounds.Score);

            // Check win condition
            if (GameManager.PlayerBScore >= winningScore)
            {
                Debug.Log("Player B (Penguin) Wins! Loading Scene 3...");
                SceneManager.LoadScene(2); // Change to the correct winning scene
                return; // Exit method to prevent further actions
            }

            StartCoroutine(RestartGame());
        }
        Debug.Log($"Checking Win Condition: Player A Score = {playerAScore}, Player B Score = {playerBScore}, Winning Score = {winningScore}");

        if (GameManager.PlayerAScore >= winningScore || GameManager.PlayerBScore >= winningScore)
        {
            Debug.Log("Player A (Bro) Wins! Loading Scene 3...");
            SceneManager.LoadScene(2); // Change to the correct winning scene
            return; // Exit method to prevent further actions
        }



        RefreshUI();
    }

   

    private void RefreshUI()
    {
        playerAScoreText.text = "Penguin:" + playerAScore;
        playerBScoreText.text = "Bro:" + playerBScore;


        
    }

    // Reset scores and restart game after a delay
    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0.1f);

        GameManager.PlayerAScore = playerAScore;
        GameManager.PlayerBScore = playerBScore;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }
}
