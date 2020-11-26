using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMoving : MonoBehaviour
{
    bool isMovingforward = true;
    bool isMovingBack = false;
    float zLowerLimit = 24.37f;
    float moveSpeed = 2.0f;
    float zUpperLimit = 31.76f;
    public GameObject Players;


    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Players.GetComponent<PlayerController>().isHitCrate)
        {


            if (isMovingBack && !isMovingforward)
            {
                if (transform.position.z >= zLowerLimit)
                {
                    transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
                }
                else
                {
                    isMovingBack = !isMovingBack;
                    isMovingforward = !isMovingforward;
                }
            }


            if (isMovingforward && !isMovingBack)
            {
                if (transform.position.z <= zUpperLimit)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
                }
                else
                {
                    isMovingBack = !isMovingBack;
                    isMovingforward = !isMovingforward;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }




}
