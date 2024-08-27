using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimerRotate : MonoBehaviour
{
    //Speed of the rotation in degrees per second
    public float rotationSpeed = 50f;

    private void Update()
    {
        // Rotate the object around its Y axis 
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
