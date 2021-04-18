using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    Rigidbody2D rb;

    GameObject eye;
    GameObject eye1;

    GameObject eye2;

    Vector3 eyePos;
    Vector3 eyePos1;
    Vector3 eyePos2;


     public float t=8f; // speed
    public float l= 10f; // length from 0 to endpoint.
    public float posY = 1f; // Offset
    // Start is called before the first frame update
    void Start()
    {
        eye = GameObject.Find("Eye1");
        eye1 = GameObject.Find("Eye2");
        eye2 = GameObject.Find("Eye3");
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;    



    }

    // Update is called once per frame
    void Update()
    {
        eyePos = eye.transform.position;
        eyePos1 = eye1.transform.position;
        eyePos2 = eye2.transform.position;

        Vector3 pos = new Vector3 ( eyePos.x, posY+Mathf.PingPong (t * Time.time, l), 0);
        Vector3 pos1 = new Vector3 ( eyePos1.x, posY+Mathf.PingPong (t * Time.time, l), 0);
        Vector3 pos2 = new Vector3 ( eyePos2.x, posY+Mathf.PingPong (t * Time.time, l), 0);

        // if(eyePos.y < 10){
        //     eyePos.y += 1;
        // }
        // else if (eyePos.y > 0){
        //     eyePos.y -= 1;
        // }
        eye.transform.position = pos;
        eye1.transform.position = pos1;
        eye2.transform.position = pos2;
    }

     }
