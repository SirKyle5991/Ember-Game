using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.Serialization;

public class LostSoul : Enemy
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;
    private LostSoulState _currentState = LostSoulState.IDLE;

    [SerializeField] private float moveSpeed;

    public Transform playerTransform;
    public bool isChasing;
    public float attackDistance = 1;
    public float maximumChaseDistance = 4;

    public override bool ShouldRespawn()
    {
        return true;
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (Vector2.Distance(transform.position, playerTransform.position) > maximumChaseDistance)
        {
            _currentState = LostSoulState.IDLE;
        }
        else
        {
            if(Vector2.Distance(transform.position, playerTransform.position) > attackDistance)
            {
                _currentState = LostSoulState.CHASE;
            }
            else
            {
                _currentState = LostSoulState.ATTACK;
            }
        }

        switch (_currentState)
        {
            case LostSoulState.IDLE:
                DoIdle();
                break;
            case LostSoulState.CHASE:
                DoChase();
                break;
            case LostSoulState.ATTACK:
                DoAttack();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    private void DoIdle()
    {
        anim.SetBool("moving", false);
    }

    private void DoChase()
    {
        if(transform.position.x > playerTransform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            anim.SetBool("moving", true);
        }
        if (transform.position.x < playerTransform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            anim.SetBool("moving", true);
        }
    }

    private void DoAttack()
    {
        Debug.Log("the player is in sight");
        if (cooldownTimer >= attackCooldown)
        {
            isChasing = false;
            cooldownTimer = 0;
            anim.SetTrigger("hug");
            Debug.Log("lost soul is hugging");
            GameManager.Instance.Player.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maximumChaseDistance);
    }


    public enum LostSoulState
    {
        IDLE,
        CHASE,
        ATTACK
    }
    
}
