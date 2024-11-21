using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerButtons : MonoBehaviour
{
    [SerializeField] private Button HomeButton;

    private void Start()
    {
        HomeButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {

        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(0);
    }


}
