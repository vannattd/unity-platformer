using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
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
        if(Input.GetKeyDown(KeyCode.Space)){
            Vector2 up = new Vector2(0, 10f);
            rb.AddForce(up, ForceMode2D.Impulse);
            ani.SetTrigger("Jumping");

        }
        if(Input.GetKey(KeyCode.E)){
            ani.SetTrigger("Attack");
        }
        player.transform.localScale = scale;
    }
}
