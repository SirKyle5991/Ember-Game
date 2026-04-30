using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    private Animator anim;

    private void Awake()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            //player hurt
        }
        else
        {
            //player dead
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            TakeDamage(1);
    }
}
