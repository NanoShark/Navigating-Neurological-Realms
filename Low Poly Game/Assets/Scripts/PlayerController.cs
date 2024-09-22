using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;



    // Start is called before the first frame update
    void Start()
    {
             
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //TODO move Player left
            //Debug.Log("A being pressed!");
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //TODO move Player right
            //Debug.Log("D being pressed!");
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }
}
