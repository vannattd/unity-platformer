using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask platform;
    GameObject player;
    Rigidbody2D rb;
    Animator ani;
    private int hitRange = 1;
    BoxCollider2D collider;

    int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rb.freezeRotation = true;    
    }

    // Update is called once per frame
    void Update()
    {
        float mag_velocity = Mathf.Abs(rb.velocity.x);
        ani.SetFloat("Speed", mag_velocity);
        float vert_velocity = Mathf.Abs(rb.velocity.y);
        ani.SetFloat("Falling", vert_velocity);
        Vector2 scale = player.transform.localScale;
        if(Input.GetKey(KeyCode.A)){
            Vector2 left = new Vector2(-0.05f, 0);
            rb.AddForce(left, ForceMode2D.Impulse);
            scale.x = -1;
        }
        if(Input.GetKey(KeyCode.D)){
            Vector2 right = new Vector2(0.05f, 0);
            rb.AddForce(right, ForceMode2D.Impulse);
            scale.x = 1;
        }
       if(Input.GetKeyDown(KeyCode.Space) && isGrounded()){
            Vector2 up = new Vector2(0, 10f);
            rb.AddForce(up, ForceMode2D.Impulse);
            ani.SetTrigger("Jumping");

        }
        if(Input.GetKey(KeyCode.E)){
            ani.SetTrigger("Attack");
            Attack();
        }
        player.transform.localScale = scale;
    }

    int getHealth(){
        return health;
    }

    void Attack(){
     RaycastHit hit;
     Vector3 forward = transform.TransformDirection(Vector3.forward);
     Vector3 origin = transform.position;
 
     if(Physics.Raycast(origin, forward, out hit, hitRange ))
     {
         if(hit.transform.gameObject.tag == "Enemy")
         {
             hit.transform.gameObject.SendMessage("TakeDamage", 30);
         }
     }
    }

    private bool isGrounded(){
        float buffer = 0.01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + buffer);

       Debug.Log(raycastHit.collider);
       return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.transform.tag == "Enemy")
         {
             Debug.Log("Hit");
         }
     }
}
