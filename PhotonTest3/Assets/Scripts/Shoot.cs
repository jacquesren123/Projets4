using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace Test
{
    public class Shoot : MonoBehaviourPun
    {
       
        private bool IsDead = false;
        public GameManager GM;

        public Camera spectateur;

        public GameObject Hunter;

        private AudioSource audios;
        int damage = 30;
        int hunterdamage = 5;

        public int currentHealth;
        [SerializeField]
        public int maxHealth;
        public Transform HealthCanvas;
        // Start is called before the first frame update
        void Start()
        {
            HealthCanvas.gameObject.SetActive(true);
            audios = GetComponent<AudioSource>();
            maxHealth = currentHealth;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                PhotonNetwork.LocalPlayer.NickName = "Non";
            }
            if (Input.GetKeyDown("mouse 0"))
            {
                audios.Play();
                Debug.Log("hit");
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    Debug.Log("HIT SOMETHING");
                    PhotonView pv;
                    pv = hit.collider.gameObject.GetComponent<PhotonView>();
                    if (hit.collider.tag == "lola")
                    {
                        currentHealth = 100;
                        pv.RPC("die", RpcTarget.All);
                        UI();

                    }
                    try
                    {


                        pv.RPC("TakeDamage", RpcTarget.All, damage);
                    }
                    catch
                    {
                        Debug.Log("Enlever vie");
                        if (currentHealth > damage)
                        {
                            currentHealth -= hunterdamage;
                        }
                        else
                        {
                            currentHealth = 0;
                            //photonView.RPC("Die", RpcTarget.All, true);
                            Die(true);
                        }
                    }
                }
                else
                {
                    if (currentHealth > damage)
                    {
                        currentHealth -= hunterdamage;
                    }

                    else
                    {
                        currentHealth = 0;
                        //photonView.RPC("Die", RpcTarget.All, true);
                        Die(true);

                    }
                    Debug.Log("Enlever vie rien touché");
                }


                if (currentHealth == 0 && !IsDead)
                {
                    IsDead = true;
                    string name = PhotonNetwork.LocalPlayer.NickName;

                    name = name.Substring(0, name.Length - 1) + 'M';
                    PhotonNetwork.LocalPlayer.NickName = name;
                }
                UI();
            }
        }

        private void UI()
        {
            HealthCanvas.GetComponent<Text>().text = currentHealth.ToString();
        }

        //[PunRPC]
        void Die(bool dead)
        {
            /*if (!IsDead)
            {
                IsDead = true;
                //GM.GetComponent<GameManager>().DieHunter();
                /*base.photonView.RPC("GM.GetComponent<GameManager>().RemoveToList", RpcTarget.All, 1);
                GM.GetComponent<GameManager>().Propsnumber--;
                Hunter.transform.position = spectateur.transform.position;
                Hunter.transform.rotation = spectateur.transform.rotation;
                Hunter.GetComponent<FPSController>().enabled = !dead;
                Debug.Log("you are dead");
                string name = PhotonNetwork.LocalPlayer.NickName;
                name = name.Substring(0, name.Length-1) + 'M';
                PhotonNetwork.LocalPlayer.NickName = name;
            }*/
        }
      
    }
}
