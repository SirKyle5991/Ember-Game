using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject victoryMenu;
    
    private List<WallSconce> _registeredSconces = new();

    public int LitSconceCount { get; private set; }

    public int TotalSconceCount => _registeredSconces.Count;


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
