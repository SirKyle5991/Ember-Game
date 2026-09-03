using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    IEnumerator DeathBeforeRespawn()
    {
        //Set to 0 as no animation has been made for it yet
        Debug.Log(currentCheckpoint);
        GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(2);
        transform.position = currentCheckpoint.position;
        Debug.Log(currentCheckpoint);
        playerHealth.Respawn();
        GameManager.Instance.PlayerDeath();
        Debug.Log("respawn");
    }

    public void Respawn()
    {
        Debug.Log("Death");
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
