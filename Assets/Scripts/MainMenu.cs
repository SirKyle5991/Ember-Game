using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public GameObject Button;
    public GameObject Play;

    EventSystem m_EventSystem;
    public void Controls()
    {
        EventSystem.current.SetSelectedGameObject(Button);
    }
    public void BackPlay()
    {
        EventSystem.current.SetSelectedGameObject(Play);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
