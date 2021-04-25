using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    Rigidbody2D rb;
    Animator ani;

    private float timeBetweenAttack;
    public float startTimeBetweenAttack;
    public Transform attackPos;
    public float attackRange;
    public int damage;
    public LayerMask whatIsEnemy;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        rb.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            ani.SetTrigger("Death");
            speed = 0;
            //Destroy(gameObject);
        }
        if (timeBetweenAttack <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            if (enemiesToDamage.Length > 0)
            {
                ani.SetTrigger("Attack");
                // Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<PlayerController>().TakeDamage(damage);
                }
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        ani.SetTrigger("Hurt");
        Debug.Log("OWWWWWWWWWWWWWWWWWWW");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
