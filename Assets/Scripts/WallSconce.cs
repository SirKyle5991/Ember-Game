using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSconce : MonoBehaviour
{
    public GameObject TorchLitPrefab;
    //public Transform spawnPosition;

    public bool isLit;

    private void Start()
    {
        GameManager.Instance.RegisterSconce(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            LightSconce();
        }
        if (col.gameObject.GetComponent<Projectile>())
        {
            LightSconce();
        }
        if (col.transform.parent && col.transform.parent.GetComponent<Flameburst>())
        {
            LightSconce();
        }
    }

    private void LightSconce()
    {

        if (isLit) return;
        
        isLit = true;
        
        Debug.Log("i think you hit something");
        Vector3 objectPosition = transform.position;
        Debug.Log("my objects position is: " + objectPosition);
        Instantiate(TorchLitPrefab, objectPosition, transform.rotation);
        GameManager.Instance.OnSconceLit();
        Destroy(gameObject);
        
    }
}
