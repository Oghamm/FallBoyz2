using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RotatingWalls : NetworkBehaviour
{
    
    [SyncVar] public Quaternion currentRot;
    public GameObject pivot;
    private bool isLeft = false;
    private float speed = 0.25f;

    [ClientRpc]
    private void RpcSyncMovement(Quaternion rotationToSync)
    {
        currentRot = rotationToSync;
    }

    void Start()
    {
        pivot = transform.parent.gameObject;
        currentRot = transform.rotation;
    }

    void Update()
    {
        if (isServer)
        {
            Debug.Log(transform.rotation.y);
            if (!isLeft)
                transform.RotateAround(pivot.transform.position, Vector3.up, speed);
            else
                transform.RotateAround(pivot.transform.position, Vector3.up, -speed);
            if (transform.rotation.y > 0.75)
                isLeft = true;
            else if (transform.rotation.y < -0.75)
                isLeft = false;
            RpcSyncMovement(this.transform.rotation);
        }
    }

    private void LateUpdate()
    {
        if (!isServer)
        {
            this.transform.rotation = currentRot;
        }
    }
}
