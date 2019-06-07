using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Switch : MonoBehaviourPun
    {
        public List<GameObject> ListSkin;
        public List<string> TagList;
        //public GameObject Trigger;
        public GameObject Skin1;
        public GameObject Skin2;
        public int sw = 0;
        protected Player Player;
        protected MeshRenderer playe;




        private int posActif = 0;
        private int posNext;

        protected Rigidbody rigidbody;

        public void Update()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
            Debug.DrawRay(transform.position, forward, Color.green);
        }
        private void Start()
        {
            //PhotonView pv = GameObject.Find("Scripts").GetComponent<PhotonView>();
            //playe = GetComponent<MeshRenderer>();

            //Initialise la liste des tags en fonction du nombre d'objet
            CreateTagList();

            base.photonView.RPC("RPC_Changeskin", RpcTarget.All, 1, 0);
            Player = GetComponent<Player>();
        }

        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A");
                sw = (sw + 1) % 2;
                Debug.Log(sw);
                if (sw % 2 == 1)
                {
                    base.photonView.RPC("RPC_Changeskin", RpcTarget.All, false, true);
                }


                else
                {
                    base.photonView.RPC("RPC_Changeskin", RpcTarget.All, true, false);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
               
                Debug.Log("Test");
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 10) && TagList.Contains(hit.transform.gameObject.tag) && TagList.IndexOf(hit.transform.gameObject.tag) != posActif)
                {
                    Debug.Log(hit.transform.gameObject.tag);
                    posNext = TagList.IndexOf(hit.transform.gameObject.tag);
                    base.photonView.RPC("RPC_Changeskin", RpcTarget.All, posActif, posNext);
                    posActif = posNext;
                }
            }
        }



        private void OnTriggerStay(Collider other)
        {
            //Debug.Log(other.gameObject.tag);
            /*if (Input.GetKeyDown(KeyCode.E) && TagList.Contains(other.gameObject.tag) && TagList.IndexOf(other.gameObject.tag) != posActif)
            {
                Debug.Log("Here");
                posNext = TagList.IndexOf(other.gameObject.tag);
                base.photonView.RPC("RPC_Changeskin", RpcTarget.All, posActif, posNext);
                posActif = posNext;
            }*/




            
        }
    


[PunRPC]


        private void RPC_Changeskin(int actualpos, int nextpos)
        {
            ListSkin[actualpos].SetActive(false);          
            ListSkin[nextpos].SetActive(true);
           float old =  ListSkin[actualpos].GetComponent<Health>().percent;
            ListSkin[nextpos].GetComponent<Health>().percent = old;
            ListSkin[nextpos].GetComponent<Health>().currentHealth = (int)((float)ListSkin[nextpos].GetComponent<Health>().maxHealth * old);


        }

        private void CreateTagList()
        {
            Debug.Log("Pourquoi ?");
            TagList = new List<string>();
            for (int i = 1; i < ListSkin.Count + 1; i++)
            {
                string u = "Skin" + i.ToString();
                Debug.Log(u);

                TagList.Add(u);
            }
        }
    }
}