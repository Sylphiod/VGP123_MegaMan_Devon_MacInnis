using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{
    public Transform Player;

    public float minXClamp = -15.35f;
    public float maxXClamp = 0f;





    void LateUpdate()
    {
        if (Player)
        {
            Vector3 cameraTransform;

            cameraTransform = transform.position;

            cameraTransform.x = Player.transform.position.x;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);

            transform.position = cameraTransform;
        }
    }
}
