using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyButtonController : MonoBehaviour
{

    [SerializeField] private Button StartButton;
    [SerializeField] private Button InstructionButton;
    [SerializeField] private GameObject Instructions;
    [SerializeField] private Button QuitButton;
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        InstructionButton.onClick.AddListener(ShowInstructions);
        QuitButton.onClick.AddListener(QuitGame);

        if (Instructions != null)
        {
            Instructions.SetActive(false);
        }
    }

    public void StartGame()
    {

        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(1);
    }

    public void ShowInstructions()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);

        // Activate the Instructions GameObject
        if (Instructions != null)
        {
            Instructions.SetActive(true);
        }
    }


    public void QuitGame()
    {

        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(3);
    }


}
