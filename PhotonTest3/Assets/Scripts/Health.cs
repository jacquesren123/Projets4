using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Test {
    public class Health : MonoBehaviourPun
    {

        private bool IsDead = false;

        public Text Propleft;
        public GameObject GM;

        public GameObject spectateur;
        [SerializeField]
        public int currentHealth;
        [SerializeField]
        public int maxHealth;
        public Transform HealthCanvas;
        public GameObject prop;
        private bool DEAD = false;
        public float percent = 1;
        public int propleft;
        public Canvas playerleft;
        
        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
            //propleft = GameObject.Find("GameManager").GetComponent<GameManager>().propnumb;
            //Propleft.text = 3.ToString();


        }

        private void UI()
        {
            HealthCanvas.GetComponent<Text>().text = currentHealth.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentHealth < 50)
            {
                HealthCanvas.GetComponent<Text>().color = Color.red;
            }
            if (currentHealth <= 0 && !DEAD)
            {
                DEAD = true;
                propleft -= 1;
                currentHealth = 0;

               // base.photonView.RPC("Die", RpcTarget.All, true);
               Die(true);
                DEAD = true;
            }
          UI();
          /*  Debug.Log(propleft + "propleft");
            base.photonView.RPC("deathfix", RpcTarget.All);*/

        }
        
       void deathfix()
        {
            Propleft.text = "Prop left" + propleft.ToString();
        }

        [PunRPC]
        void TakeDamage(int damage)
        {
            if (currentHealth > 0)
            {
                Debug.Log(propleft);
                UI();
                currentHealth -= damage;
                percent = (float)currentHealth / (float)maxHealth;
            }


        }
        private void OnDestroy()
        {
            
            PhotonNetwork.NickName = PhotonNetwork.LocalPlayer.NickName.Substring(0, name.Length - 1) + 'M';
            Debug.Log("you are dead");
        }

        public string SuppLast(string s)
        {
            return s;
        }

       
        [PunRPC]
        void Die(bool dead)
        {
            if (!IsDead)
            {
                
                IsDead = true;
                //base.photonView.RPC("GM.GetComponent<GameManager>().RemoveToList", RpcTarget.All, 1);    
                prop.GetComponent<FPSController>().enabled = !dead;
                prop.transform.position = spectateur.transform.position;
                prop.transform.rotation = spectateur.transform.rotation;
                PhotonNetwork.LocalPlayer.NickName = PhotonNetwork.LocalPlayer.NickName.Substring(0, name.Length - 1) + 'M';
                //PhotonNetwork.Destroy(prop);
                Debug.Log(propleft);
                
            }
        }

    }
}
