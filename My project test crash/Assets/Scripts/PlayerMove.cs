using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMove : NetworkBehaviour
{
    private float speed = 4f;
    private Rigidbody rigidbody;
    public float gravity;
    public float addGravity;

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
            //transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            //Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
            //transform.position = transform.position + movement;
        }
    }
}
