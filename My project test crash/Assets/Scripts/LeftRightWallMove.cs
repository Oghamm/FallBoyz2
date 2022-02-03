using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

using Debug = UnityEngine.Debug;

public class LeftRightWallMove : NetworkBehaviour
{

    [SyncVar] public Vector3 currentPos;
    private Vector3 originPos;
    private Vector3 memoryPos;
    [SerializeField] private bool isLeft = false;
    private float speed = 0.003f;

    [ClientRpc]
    private void RpcSyncMovement(Vector3 positionToSync)
    {
        currentPos = positionToSync;
    }

    void Start()
    {
        currentPos = transform.position;
        originPos = transform.position;
        memoryPos = transform.position;
    }

    void Update()
    {
        if (isServer)
        {
            Debug.Log("pirntprouyt");
            if (isLeft)
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);

            if (transform.position.x > 0)
            {
                if (transform.position.x > 5.75)
                {
                    isLeft = false;
                    memoryPos = transform.position;
                }
                else if (transform.position.x < 4.27)
                {
                    isLeft = true;
                    memoryPos = transform.position;
                }
            }
            else if (transform.position.x < 0)
            {
                if (transform.position.x > -4.27)
                {
                    isLeft = false;
                    memoryPos = transform.position;
                }
                else if (transform.position.x < -5.75)
                {
                    isLeft = true;
                    memoryPos = transform.position;
                }
            }

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
