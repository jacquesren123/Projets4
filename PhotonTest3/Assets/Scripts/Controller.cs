using UnityEngine;

namespace Test
{

    public class Controller : MonoBehaviour
    {

        protected Player Player;
        private Camera cam;

        //Parameters
        protected const float RotationSpeed = 10;

        //Camera Controll
        public Vector3 CameraPivot;
        public float CameraDistance;
        protected float InputRotationX;
        protected float InputRotationY;

        protected Vector3 CharacterPivot;
        protected Vector3 LookDirection;

        // Use this for initialization
        void Start()
        {

            Player = GetComponent<Player>();


        }

        // Update is called once per frame
        void FixedUpdate()
        {

            //input
            InputRotationX = InputRotationX * RotationSpeed * Time.deltaTime % 360f;
            InputRotationY = Mathf.Clamp(InputRotationY * RotationSpeed * Time.deltaTime, -88f, 88f);

            //left and forward
            var characterForward = Quaternion.AngleAxis(InputRotationX, Vector3.up) * Vector3.forward;
            var characterLeft = Quaternion.AngleAxis(InputRotationX + 90, Vector3.up) * Vector3.forward;

            //look and run direction
            var runDirection = characterForward * (Input.GetAxisRaw("Vertical")) + characterLeft * (Input.GetAxisRaw("Horizontal"));
            LookDirection = Quaternion.AngleAxis(InputRotationY, characterLeft) * characterForward;

            //set player values
            Player.Input2.RunX = runDirection.x;
            Player.Input2.RunZ = runDirection.z;
            Player.Input2.LookX = LookDirection.x;
            Player.Input2.LookZ = LookDirection.z;
            Player.Input2.Jump = Input.GetKeyDown("space");

            CharacterPivot = Quaternion.AngleAxis(InputRotationX, Vector3.up) * CameraPivot;
        }

        private void LateUpdate()
        {
            //set camera values
            Camera.main.transform.position = (transform.position + CharacterPivot) - LookDirection * CameraDistance;
            Camera.main.transform.rotation = Quaternion.LookRotation(LookDirection, Vector3.up);
        }
    }
}
