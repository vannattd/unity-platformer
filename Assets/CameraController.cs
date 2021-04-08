using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Vector3 playerPos;
    GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("Player");
       camera = GameObject.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        playerPos.z = -10;
        camera.transform.position = playerPos;

    }
}
