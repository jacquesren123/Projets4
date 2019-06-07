using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class simplecontrol : MonoBehaviour
    {
        public int Speed = 5;
        private Vector3 DirectionDeplacement = Vector3.zero;
        private CharacterController Player;
        public float speedH = 2.0f;
        public float speedV = 2.0f;
        private float yaw = 0.0f;
        private float pitch = 0.0f;
        // Start is called before the first frame update
        void Start()
        {
            Player = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            DirectionDeplacement.z = Input.GetAxisRaw("Vertical");
            DirectionDeplacement.x = Input.GetAxisRaw("Horizontal");
            Player.Move(DirectionDeplacement * Time.deltaTime * Speed);
            yaw += speedH * Input.GetAxisRaw("Mouse X");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}