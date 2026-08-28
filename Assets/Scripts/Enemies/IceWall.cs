using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class IceWall : Enemy
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Projectile>())
        {
            Debug.Log("i think you shot something");
            Vector3 objectPosition = transform.position;
            Debug.Log("my objects position is: " + objectPosition);
            Destroy(gameObject);
        }
        if (col.gameObject.GetComponent<Flameburst>())
        {
            Debug.Log("i think you flamebursted something");
            Vector3 objectPosition = transform.position;
            Debug.Log("my objects position is: " + objectPosition);
            Destroy(gameObject);
        }
    }

    public override bool ShouldRespawn()
    {
        return false;
    }
}
