using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject gameMenu;
    public void Menu()
    {
        gameMenu.SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Resume()
    {
        gameMenu.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //public void Volume()
    //{
    //
    //}
}
