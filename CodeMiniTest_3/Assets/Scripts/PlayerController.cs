using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator AniPlayer;

    public float speed;
    bool IsMove = false;
    public Text PUscore;
    float icount = 0.0f;
    public Animator bridgeT;
    float iTotalPowerUpLeft = 4.0f;
    bool BridgeTurn = false;
    public Text Timer;
    float timercount = 10.0f;
    float CountDown = 0.0f;
    float jumpForce = 10.0f;
    float gravityModifier = 2.0f;
    bool isGrounded;
    public bool isHitCrate = false;



    Rigidbody PlayerRb;
    public Renderer playerRdr;

    public Material[] playerMtrs;




    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        AniPlayer = GetComponent<Animator>();

        isGrounded = true;
        Physics.gravity *= gravityModifier;



    }

    // Update is called once per frame
    void Update()
    {





        float verticalinput = Input.GetAxis("Vertical");
        float horizontalinput = Input.GetAxis("Horizontal");




        PlayerJump();



        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            AniPlayer.SetBool("IsMove", true);
            StartRun();

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            AniPlayer.SetBool("IsMove", true);
            StartRun();

        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        {
            AniPlayer.SetBool("IsMove", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            AniPlayer.SetBool("IsMove", true);
            StartRun();

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            AniPlayer.SetBool("IsMove", true);
            StartRun();

        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            AniPlayer.SetBool("IsMove", false);
        }

        if (BridgeTurn == true && timercount >= 0)
        {
            Timer.GetComponent<Text>().text = "Time: " + timercount.ToString("0");
            timercount -= Time.deltaTime;
        }
        else
        {
            Timer.GetComponent<Text>().text = "==" + "==";
            bridgeT.SetBool("ConeTrue", false);
        }



    }






    private void OnTriggerEnter(Collider col)
    {

        if (iTotalPowerUpLeft == 0)
        {
            if (col.gameObject.CompareTag("Cone"))
            {
                bridgeT.SetBool("ConeTrue", true);
                BridgeTurn = true;

            }

        }
        else if (iTotalPowerUpLeft < 1)
        {
            if (col.gameObject.CompareTag("Cone"))
            {
                Debug.Log("Collect all power up");

            }

        }
        if (col.gameObject.CompareTag("Coins"))
        {
            iTotalPowerUpLeft--;
            icount++;
            PUscore.GetComponent<Text>().text = "Coin Collected: " + icount.ToString();

            Destroy(col.gameObject);
        }

    }

    void StartRun()
    {
        AniPlayer.SetFloat("StartRun", 0.26f);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Grounded"))
        {
            isGrounded = true;


            playerRdr.material.color = playerMtrs[1].color;

            Debug.Log("change");
        }

        if(collision.gameObject.CompareTag("TagBox"))
        {
            Debug.Log("Collided with box");
            isHitCrate = true;
        }



    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;


            playerRdr.material.color = playerMtrs[0].color;

            Debug.Log("change again");
        }
    }

   


}