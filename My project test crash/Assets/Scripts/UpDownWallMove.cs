using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

using Debug = UnityEngine.Debug;

public class UpDownWallMove : NetworkBehaviour
{

    [SyncVar] public Vector3 currentPos;
    private bool isUp = false;
    private float speed = 0.003f;

    [ClientRpc]
    private void RpcSyncMovement(Vector3 positionToSync)
    {
        currentPos = positionToSync;
    }

    void Start()
    {
        currentPos = transform.position;
    }

    void Update()
    {
        if (isServer)
        {
            Debug.Log("pirntprouyt");
            if (isUp)
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            if (transform.position.y > 1.2)
                isUp = true;
            else if (transform.position.y < -1)
                isUp = false;
            RpcSyncMovement(this.transform.position);
        }
    }

    private void LateUpdate()
    {
        if (!isServer)
        {
            this.transform.position = currentPos;
        }
    }
}
