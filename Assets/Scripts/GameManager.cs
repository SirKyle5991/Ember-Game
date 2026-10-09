using System;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field:SerializeField] public PlayerInput Input { get; private set; }

    [SerializeField] GameObject victoryMenu;
    
    private List<WallSconce> _registeredSconces = new();

    public int LitSconceCount { get; private set; }

    public int TotalSconceCount => _registeredSconces.Count;

    private Action OnPlayerDeath;

    public GameObject Player { get; private set; }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public void RegisterEnemy(Enemy enemy)
    {
        if (enemy.ShouldRespawn())
        {
            OnPlayerDeath += enemy.Health.Respawn;
        }
    }

    public void RegisterPlayer(PlayerController player)
    {
        Player = player.gameObject;
    }

    public void RegisterSconce(WallSconce sconce)
    {
        _registeredSconces.Add(sconce);
    }

    public void OnSconceLit()
    {
        LitSconceCount++;
        Debug.Log("SCONCE LIT");
        if (LitSconceCount >= _registeredSconces.Count)
        {
            victoryMenu.SetActive(true);
            Debug.Log("ALL SCONCES LIT");
        }
    }

    public void SetPaused(bool isPaused)
    {
        Time.timeScale = isPaused ? 0 : 1;
        if(isPaused)
            Input.actions.FindActionMap("Player Controls").Disable();
        else
            Input.actions.FindActionMap("Player Controls").Enable();
    }

}
