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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("Hit target!!!");
            GameManager.TargetHit();
            Destroy(collision.gameObject); // target
            Destroy(gameObject, 2.0f);    //bullet
            
        }
    }
}
