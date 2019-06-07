using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Test
{
    public class FPSController : MonoBehaviourPun
    {
        public GameObject cam;
        public float speed = 2f, sensitivity = 2f, jumpDistance = 5f;
        float moveFB, moveLR, rotX, rotY, verticalVelocity;
        CharacterController charCon;
        Animator anim;
        public AudioSource music;
       public GameObject prop;
        public AudioSource zikmu;
        private bool rotation = true;
        // Start is called before the first frame update
        void Start()
        {
            charCon = gameObject.GetComponent<CharacterController>();
            anim = gameObject.GetComponent<Animator>();
            zikmu.Play();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                base.photonView.RPC("footstep", RpcTarget.All);


            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                rotation = !rotation;
                //base.photonView.RPC("freeze", RpcTarget.All);
            }
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Cursor.lockState = CursorLockMode.None;
            }


            moveFB = Input.GetAxis("Vertical") * speed;
            moveLR = Input.GetAxis("Horizontal") * speed;
                rotX = Input.GetAxis("Mouse X") * sensitivity;
                rotY -= Input.GetAxis("Mouse Y") * sensitivity;
                rotY = Mathf.Clamp(rotY, -60f, 60f);
            
            Vector3 movement = new Vector3(moveLR, verticalVelocity, moveFB);
            if (rotation)
                transform.Rotate(0, rotX, 0);
            else
                transform.RotateAround(transform.position, new Vector3(0,1,0), rotY);
            
            cam.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
            
                movement = transform.rotation * movement;
            charCon.Move(movement * Time.deltaTime);
            if (charCon.isGrounded)
            {
                if (Input.GetButton("Jump"))
                {
                    verticalVelocity = jumpDistance;

                }
            }
            if (charCon.velocity.x > 0 || charCon.velocity.z > 0)
            {
                anim.SetFloat("Speed", 1);
            }
            else
            {
                anim.SetFloat("Speed", 0);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetBool("Crouch", true);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                anim.SetBool("Crouch", false);
            }
        }
        private void FixedUpdate()
        {
            if (!charCon.isGrounded)
            {
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
                anim.SetBool("Jump", true);
            }
            else
            {
                anim.SetBool("Jump", false);
            }
        }
        [PunRPC]
        void footstep()
        {
            music.Play();
        }
        [PunRPC]
        void freeze()
        {
            prop.transform.rotation = Quaternion.identity;

        }
        

        
    }
}