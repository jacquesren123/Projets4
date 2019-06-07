using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Playercontrol : MonoBehaviourPun , IPunObservable
{
    public int Speed = 5;
    public int RunSpeed = 10;
    private Vector3 DirectionDeplacement = Vector3.zero;
    private CharacterController Player;
    
    public int Jump = 5;
    public int gravite = 20;
    private Animator Anim;
    private int sautmax = 0;
    public GameObject Arme1;
    public GameObject Arme2;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    CursorLockMode wantedMode;
    Vector3 realPosition;
    Quaternion realRotation;
   


    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();
        Arme2.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;



    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, .1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, .1f);
        }
        // CURSOR lockmode
        if (Input.GetKeyDown(KeyCode.Mouse1) && (Cursor.lockState != CursorLockMode.Locked))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && (Cursor.lockState == CursorLockMode.Locked))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        DirectionDeplacement.z = Input.GetAxisRaw("Vertical");
        DirectionDeplacement.x = Input.GetAxisRaw("Horizontal");
        DirectionDeplacement = transform.TransformDirection(DirectionDeplacement);       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Arme1.SetActive(true);
            Arme2.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Arme2.SetActive(true);
            Arme1.SetActive(false);
        }
        // Deplacement
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Player.Move(DirectionDeplacement * Time.deltaTime * RunSpeed);
        }
        else
        {
            Player.Move(DirectionDeplacement * Time.deltaTime * Speed);
        }

        yaw += speedH * Input.GetAxisRaw("Mouse X");
        
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        

               
        

        //SAUT
        if (Input.GetKeyDown(KeyCode.Space) & sautmax <1)
        {
            DirectionDeplacement.y = Jump;
            Anim.SetTrigger("Jumping");
          
        }
        //Gravity

        if (!Player.isGrounded)
        {
            DirectionDeplacement.y -= gravite * Time.deltaTime;
            sautmax = 1;
        }
        else
            sautmax = 0;
        //Animation
        if (Input.GetKey(KeyCode.Z) & !Input.GetKey(KeyCode.LeftShift))
        {
            Anim.SetBool("Walk", true);
            Anim.SetBool("Run", false);
        }       
        else  if(Input.GetKey(KeyCode.Z) & Input.GetKey(KeyCode.LeftShift))
        {
           Anim.SetBool("Walk", false);
            Anim.SetBool("Run", true);
        }
        if (!Input.GetKey(KeyCode.Z) & !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) 
        {                    
            Anim.SetBool("Walk", false);
            Anim.SetBool("Run", false);
        }
        

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();

        }
    }
}
