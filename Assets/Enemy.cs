using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    Rigidbody2D rb;
    Animator ani;


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
        if(health <= 0){
            ani.SetTrigger("Death");
            speed = 0;
            //Destroy(gameObject);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage){
        health -= damage;
        Debug.Log("OWWWWWWWWWWWWWWWWWWW");
    }


}
