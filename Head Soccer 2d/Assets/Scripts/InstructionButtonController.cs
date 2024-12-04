using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionButtonController : MonoBehaviour
{
    [SerializeField] private Button BackButton;
    void Start()
    {
        BackButton.onClick.AddListener(GoBack);
        
    }

    public void GoBack()
    {

        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(0);
    }
}
