using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flameburst : MonoBehaviour
{
    [SerializeField] private GameObject AOE;

    private float AttackTime = 1.0f;
    private float Timer = 0.0f;

    public float Damage;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(+1 * Damage);
        }
    }

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
        if (AOE.activeSelf != true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("x key was pressed");
                AOE.SetActive(true);
                Timer = 0.0f;
            }
        }
    }
}
