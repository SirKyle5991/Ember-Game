using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject gameMenu;
    
    private InputAction playerGameMenu;

    public void Start()
    {
        var actionMap = GameManager.Instance.Input.actions.FindActionMap("Player Controls"); //ask for the action map
        playerGameMenu = actionMap.FindAction("GameMenu");
    }

    public void ToggleMenu()
    {
        gameMenu.SetActive(!gameMenu.activeInHierarchy);
        GameManager.Instance.SetPaused(gameMenu.activeInHierarchy);
    }
    public void MainMenu()
    {
        GameManager.Instance.SetPaused(false);
        SceneManager.LoadScene("Main Menu");
    }
    public void Resume()
    {
        ToggleMenu();
    }
    public void Restart()
    {
        GameManager.Instance.SetPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Actual Castle");
    }
    //public void Volume()
    //{
    //
    //}
    void Update()
    {
        if (playerGameMenu.WasPerformedThisFrame())
        {
            ToggleMenu();
        }
    }
}
