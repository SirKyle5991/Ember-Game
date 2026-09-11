using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TutorialActivate;

    IEnumerator DeathAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        TutorialActivate.SetActive(false);
        if (gameObject.CompareTag("TutorialActivate"))
        {
            gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("duuuuh");
            TutorialActivate.SetActive(true);
            StartCoroutine(DeathAfterDelay());
        }
    }
}
