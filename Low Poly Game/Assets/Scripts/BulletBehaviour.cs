using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletBehaviour : MonoBehaviour
{
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
