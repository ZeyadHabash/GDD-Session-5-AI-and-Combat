//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    // maybe add variables for boundaries if needed

    void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            // cant set camera position to target position bec weird interaction with the z axis
            // camera needs to stay in the same location in z axis
            Vector3 CamPos = new Vector3(target.position.x, target.position.y, transform.position.z);

            // Lerp does some linear interpolation maths stuff idc
            // tl;dr it smoothly moves from previous position to new position
            transform.position = Vector3.Lerp(transform.position, CamPos, smoothing);
        }
    }
}
