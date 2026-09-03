using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

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

    [SerializeField] private float moveSpeed;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

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

        if (isChasing)
        {
            if(transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                //anim.SetBool("moving", true);
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                //anim.SetBool("moving", true);
            }
            if (PlayerInSight())
            {
                Debug.Log("the player is in sight");
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    DamagePlayer();
                    anim.SetTrigger("hug");
                    Debug.Log("lost soul is hugging");
                    //playerHealth.TakeDamage(damage);
                }
            }
            if (enemyPatrol != null)
                enemyPatrol.enabled = !PlayerInSight();
        }
        
        if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
        {
            isChasing = false;
            anim.SetBool("moving", false);
        }

        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
        }

        //attack only when player in sight?
        //if (PlayerInSight())
        //{
        //    if (cooldownTimer >= attackCooldown)
        //    {
        //        cooldownTimer = 0;
        //        anim.SetTrigger("hug");
        //    }
        //}
        //if (enemyPatrol != null)
        //    enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);


        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //If player still in range damage him
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}
