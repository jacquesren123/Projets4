using Photon.Pun;
using UnityEngine;

namespace Test
{
    public class Player : MonoBehaviourPun, IPunObservable
    {
        [HideInInspector]
        public InputStr Input2;
        public struct InputStr
        {
            public float LookX;
            public float LookZ;
            public float RunX;
            public float RunZ;
            public bool Jump;
        }


       

        public const float Speed = 10f;
        public const float JumpForce = 5f;

        protected Rigidbody rigidbody;
        //protected Animator Animator;
        protected Quaternion LookRotation;

        protected bool Grounded;

        private void Awake()
        {
           
            //Animator = GetComponentInChildren<Animator>();

            //destroy the controller if the player is not controlled by me
           // if (!photonView.IsMine && GetComponent<Playercontrol>() !=null )
            {
                Debug.Log("Destroyed");
                //Destroy(GetComponent<simplecontrol>());
               
               
                GetComponent<Playercontrol>().enabled = false;
               

               
            }
                
        }
      


        public static void RefreshInstance(ref Player player, Player Prefab)
        { 
            var position = new Vector3 (0, 2, 0);
            var rotation = Quaternion.identity;
            if (player != null)
            {
                position = player.transform.position;
                rotation = player.transform.rotation;
                PhotonNetwork.Destroy(player.gameObject);
            }

           player = PhotonNetwork.Instantiate("Player",position, rotation).GetComponent<Player>();
            player.transform.Find("Camera").gameObject.SetActive(true);
            ((MonoBehaviour)player.GetComponent("Playercontrol")).enabled = true;
           
            
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(Input2.RunX);
                stream.SendNext(Input2.RunZ);
                stream.SendNext(Input2.LookX);
                stream.SendNext(Input2.LookZ);
            }
            else
            {
                Input2.RunX = (float)stream.ReceiveNext();
                Input2.RunZ = (float)stream.ReceiveNext();
                Input2.LookX = (float)stream.ReceiveNext();
                Input2.LookZ = (float)stream.ReceiveNext();
            }
            
        }
    }
}
