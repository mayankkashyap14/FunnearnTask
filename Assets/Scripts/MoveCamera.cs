using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    private Vector3 camerOffset;

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;


    private void Start()
    {
        camerOffset = transform.position - target.position;
    }
    private void LateUpdate()
    {
        Vector3 newPos = target.position + camerOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
    }

    


}//class
