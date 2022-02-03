using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerMove : NetworkBehaviour
{
    private float speed = 4f;
    private Rigidbody rigidbody;
    public float gravity;
    public float addGravity;
    private Camera mainCamera;
    private CamFollowSmooth followScript;

    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
        followScript = mainCamera.GetComponent<CamFollowSmooth>();
        followScript.target = this.transform;

    }

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        transform.position = new Vector3(transform.position.x, 1.3f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rigidbody.velocity.y, Input.GetAxis("Vertical") * speed);
            Debug.Log(transform.position.y);
            if (transform.position.y <= -10)
            {
                transform.position = new Vector3(-5.5f, 0f, -3f);
            }
            if (Input.GetKey("space"))
            {
                Debug.Log("escape");
                SceneManager.LoadScene("Main Menu");
            }
            //transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            //Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
            //transform.position = transform.position + movement;
        }
    }
}
