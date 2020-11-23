using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Animator AniPlayer;
    
    public float speed;
    bool IsMove = false;





    Rigidbody PlayerRb;






    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        AniPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalinput = Input.GetAxis("Vertical");
        float horizontalinput = Input.GetAxis("Horizontal");



        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation= Quaternion.Euler (0, 0, 0);
            AniPlayer.SetBool("IsMove", true);
            StartRun();
            
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            AniPlayer.SetBool("IsMove", true);
            StartRun();
            
        }
        else if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        {
            AniPlayer.SetBool("IsMove", false);
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Cone"))
        {
            Debug.Log("Bridge");
        }
    }

    void StartRun()
    {
        AniPlayer.SetFloat("StartRun", 0.26f);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    

}
