using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] GameObject mainMenu;
    //
    //private InputAction playerMainMenu;
    //
    //public void Start()
    //{
    //    var actionMap = GameManager.Instance.Input.actions.FindActionMap("Player Controls"); //ask for the action map
    //    playerMainMenu = actionMap.FindAction("Main Menu");
    //}
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
