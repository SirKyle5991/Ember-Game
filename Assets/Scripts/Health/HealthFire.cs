using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFire : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var health = collision.GetComponent<Health>();
            if(health != null)
                health.AddHealth(healthValue);
        }
    }
}
