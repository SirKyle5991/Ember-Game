using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TutorialActivate;

    IEnumerator TutorialDelay()
    {
        yield return new WaitForSeconds(10f);
        TutorialActivate.SetActive(false);
        if (gameObject.CompareTag("TutorialActivate"))
        {
            gameObject.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        //if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("duuuuh");
            TutorialActivate.SetActive(true);
            StartCoroutine(TutorialDelay());
        }
    }
}
