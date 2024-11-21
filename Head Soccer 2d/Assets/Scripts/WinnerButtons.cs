using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerButtons : MonoBehaviour
{
    [SerializeField] private Button HomeButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;
    private void Start()
    {
        HomeButton.onClick.AddListener(StartGame);
        RestartButton.onClick.AddListener(RestartGame);
        QuitButton.onClick.AddListener(QuitGame);
    }

    private void RestartGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(1);
    }

    private void QuitGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(3);
    }

    private void StartGame()
    {

        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(0);
    }


}
