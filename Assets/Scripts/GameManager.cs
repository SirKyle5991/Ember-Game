using System;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject victoryMenu;
    
    private List<WallSconce> _registeredSconces = new();

    public int LitSconceCount { get; private set; }

    public int TotalSconceCount => _registeredSconces.Count;

    private Action OnPlayerDeath;


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

}
