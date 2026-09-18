using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    public Animator anim;
    public bool dead => currentHealth <= 0;
    public bool invulnerable;

    public UnityEvent onDeath = new();

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
        Debug.Log("DAMAGE");
        
        if (dead) return;
        
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            anim.SetTrigger("die");
            onDeath.Invoke();
        }
    }

    IEnumerator DeathAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        if (gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(true);
            GetComponent<PlayerController>().enabled = true;
        }
    }



    private void Update()
    {
        if (gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.W))
                TakeDamage(1);
        }
        if (dead == true)
        {
                StartCoroutine(DeathAfterDelay());
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        AddHealth(startingHealth);
        gameObject.SetActive(true);
        anim.ResetTrigger("die");
        anim.Play("Idle");
    }
}
