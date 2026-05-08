using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private const int FIREBALL_MAX_LIFE = 5;
    private const float FIREBALL_DEATH_DELAY = 0.5f;

    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float Lifetime;
    
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        

        Lifetime += Time.deltaTime;
        if (Lifetime > FIREBALL_MAX_LIFE)
            Destroy(gameObject, FIREBALL_DEATH_DELAY);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
        Destroy(gameObject, FIREBALL_DEATH_DELAY);

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    public void SetDirection(float _direction)
    {
        Lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivcate()
    {
        gameObject.SetActive(false);
    }
}
