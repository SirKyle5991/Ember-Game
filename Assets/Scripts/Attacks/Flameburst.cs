using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flameburst : MonoBehaviour
{
    [SerializeField] private GameObject AOE;

    private float AttackTime = 2.0f;
    private float Timer = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > AttackTime)
        {
            //Debug.Log("time flies");
            AOE.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("x key was pressed");
            if (AOE.activeSelf != true)
            {
                AOE.SetActive(true);
                Timer = 0.0f;
            }
        }
    }
}
