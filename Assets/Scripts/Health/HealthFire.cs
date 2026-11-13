using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFire : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip firepitSound;
    [SerializeField] private AudioClip healmeSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var health = collision.GetComponent<Health>();
            if(health != null)
                health.AddHealth(healthValue);
            if (healmeSound != null)
                GetComponent<AudioSource>().PlayOneShot(healmeSound);
        }
        GetComponent<AudioSource>().PlayOneShot(firepitSound);
    }
}
