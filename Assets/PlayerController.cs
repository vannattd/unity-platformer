using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Used to control the player and it's interactions
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask platform;


    private float timeBetweenAttack;
    public float startTimeBetweenAttack;
    public Transform attackPos;
    public float attackRange;
    public int damage;
    public LayerMask whatIsEnemy;






    GameObject player;
    GameObject live1;
    GameObject live2;
    GameObject live3;

    Rigidbody2D rb;
    Animator ani;
    private int hitRange = 1;
    BoxCollider2D collider;
    Vector3 playerPos;


    int health;
    public float invulTime = 1f; // The time you stay invulnerable after a hit
    private bool invulnerable = false;  // this boolean gets checked inside of the Damage function.
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        player = GameObject.Find("Player");
        live1 = GameObject.Find("Life1");
        live2 = GameObject.Find("Life2");
        live3 = GameObject.Find("Life3");
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        float mag_velocity = Mathf.Abs(rb.velocity.x);
        ani.SetFloat("Speed", mag_velocity);
        float vert_velocity = Mathf.Abs(rb.velocity.y);
        ani.SetFloat("Falling", vert_velocity);
        Vector2 scale = player.transform.localScale;
        if (playerPos.y <= -20)
        {
            Application.LoadLevel(0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 left = new Vector2(-1f, 0);
            rb.AddForce(left, ForceMode2D.Impulse);
            scale.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector2 right = new Vector2(1f, 0);
            rb.AddForce(right, ForceMode2D.Impulse);
            scale.x = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Vector2 up = new Vector2(0, 10f);
            rb.AddForce(up, ForceMode2D.Impulse);
            ani.SetTrigger("Jumping");

        }
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetKey(KeyCode.E))
            {
                ani.SetTrigger("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
        player.transform.localScale = scale;
    }

    int getHealth()
    {
        return health;
    }


    private bool isGrounded()
    {
        float buffer = 0.01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + buffer);

        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            if (!invulnerable)
            {
                if (health == 3)
                {
                    Destroy(live1);
                    StartCoroutine(JustHurt());
                }
                if (health == 2)
                {
                    Destroy(live2);
                    StartCoroutine(JustHurt());
                }
                if (health == 1)
                {
                    Destroy(live3);
                    Application.LoadLevel(0);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            if (health == 3)
            {
                Destroy(live1);
                StartCoroutine(JustHurt());
            }
            if (health == 2)
            {
                Destroy(live2);
                StartCoroutine(JustHurt());
            }
            if (health == 1)
            {
                Destroy(live3);
                Application.LoadLevel(0);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    IEnumerator JustHurt()
    {
        invulnerable = true;
        Debug.Log("hit");
        yield return new WaitForSeconds(invulTime);
        health--;
        invulnerable = false;
    }
}
