using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSconce : MonoBehaviour
{
    public GameObject TorchLitPrefab;
    //public Transform spawnPosition;

    private int SconceCounter = 0;
 
    private void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.GetComponent<PlayerController>())
       {
           Debug.Log("i think you hit something");
           Vector3 objectPosition = transform.position;
           Debug.Log("my objects position is: " + objectPosition);
           Instantiate(TorchLitPrefab, objectPosition, transform.rotation);
           SconceCounter = SconceCounter + 1;
           Debug.Log("Scounter is " + SconceCounter);
           Destroy(gameObject);
       }
        if (col.gameObject.GetComponent<Projectile>())
        {
            Debug.Log("i think you shot something");
            Vector3 objectPosition = transform.position;
            Debug.Log("my objects position is: " + objectPosition);
            Instantiate(TorchLitPrefab, objectPosition, transform.rotation);
            Destroy(gameObject);
        }
       if (col.gameObject.GetComponent<Flameburst>())
       {
           Debug.Log("i think you flamebursted something");
           Vector3 objectPosition = transform.position;
           Debug.Log("my objects position is: " + objectPosition);
           Instantiate(TorchLitPrefab, objectPosition, transform.rotation);
           Destroy(gameObject);
       }
    }
}
