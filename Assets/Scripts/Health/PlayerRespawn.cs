using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    IEnumerator DeathBeforeRespawn()
    {
        //Set to 0 as no animation has been made for it yet
        yield return new WaitForSeconds(0);
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
        Debug.Log("respawn");
    }

    public void Respawn()
    {
        StartCoroutine(DeathBeforeRespawn());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            Debug.Log("you hit the checkpoint");
        }
    }
}
