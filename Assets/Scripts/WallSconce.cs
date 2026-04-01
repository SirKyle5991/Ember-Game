using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSconce : MonoBehaviour
{
    public GameObject TorchLitPrefab;
    //public Transform spawnPosition;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("i think you hit something");
            Vector3 objectPosition = transform.position;
            Debug.Log("my objects position is: " + objectPosition);
            Instantiate(TorchLitPrefab, objectPosition, transform.rotation);
            Destroy(gameObject);
        }
    }
}
