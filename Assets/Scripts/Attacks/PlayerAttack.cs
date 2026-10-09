using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform attackspawn;
    [SerializeField] private Projectile fireballPrefab;

    //[SerializeField] private GameObject[] Fireball;
    private Animator anim;
    private PlayerController playerController;
    private float cooldownTimer = Mathf.Infinity;
    
    private InputAction playerFireball;

    //private int Health currentHealth;
    //private bool CanAttack => currentHealth > 2;

    private void Start()
    {
        var actionMap = GameManager.Instance.Input.actions.FindActionMap("Player Controls"); //ask for the action map
        playerFireball = actionMap.FindAction("Fireball");
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerFireball.WasPerformedThisFrame() && cooldownTimer > attackCooldown)
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        Debug.Log("attack");

        var fireball = Instantiate(fireballPrefab, attackspawn.position, Quaternion.identity);
        fireball.SetDirection(Mathf.Sign(transform.localScale.x));

        //Fireball[FindFireball()].transform.position = attackspawn.position;
        //Fireball[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    //private int FindFireball()
    //{
    //    for (int i = 0; i < Fireball.Length; i++)
    //    {
    //        if (!Fireball[i].activeInHierarchy)
    //            return i;
    //    }
    //    return 0;
    //}
}
