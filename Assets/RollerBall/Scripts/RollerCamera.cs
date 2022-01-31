using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5;
    public float pitch = 45;
    public float sensitivity = 1;

    //float yaw = 0;

    // Update is called once per frame
    void Update()
    {
        Quaternion qPitch = Quaternion.AngleAxis(pitch, Vector3.right);
        Vector3 offset = qPitch * Vector3.back * distance;

        transform.position = target.position + offset;
    }
}
