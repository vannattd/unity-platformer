using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Vector3 playerPos;
    GameObject camera;

    public Transform[] backgrounds;
    public float[] parallaxScales;
 public float smoothing;
 
 private Transform cam;
 
 private Vector3 previousCamPos;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("Player");
       camera = GameObject.Find("Camera");

            cam = Camera.main.transform;
     
     previousCamPos = cam.position;
     
     parallaxScales = new float[backgrounds.Length];
     
     
     for (int i = 0; i < backgrounds.Length; i++) {
         
         parallaxScales[i] = backgrounds[i].position.z * -1;
         Debug.Log(parallaxScales[i]);
         
         
         
         
     }
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        playerPos.z = -10;
        camera.transform.position = playerPos;

    }

    void FixedUpdate () {
     
     for (int i = 0; i < backgrounds.Length; i++) {
         
         float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
         
         float backgroundTargetPosX = backgrounds[i].position.x + parallax;
         
         Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
         
         backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
         
         
     }
     
     previousCamPos = cam.position;
     
     
 }
}
