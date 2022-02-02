// Script that powers the title screen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    PlayerControls _playerInput;
    void Awake()
    {
        _playerInput = new PlayerControls();
        
        _playerInput.General.Quit.performed += OnQuitPressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnQuitPressed(InputAction.CallbackContext context) {
        QuitGame();
    }   

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
