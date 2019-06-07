using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Photon.Realtime;

namespace Test
{
    public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        public Canvas propshunterleft;
        public GameObject spawn;

        public bool Prop = false;

        public Canvas canvas;

        public Transform Timer;

        private float time = 5f;
        private bool StartTimer = false;
        private bool StartTimerBefore = false;

        public List<string> Ready;

        public bool MancheIsStart = false;

        public Transform ListPlayerCanvas;
        public Canvas Lobby;

        private bool AllSpawn = false;

        public Transform Victory;
        /*  public List<string> Hunters;
          public List<string> Props;*/
        public int Hunternumber = 0;
        public int Propsnumber = 0;
        public GameObject play;
        public GameObject hunterbutton;
        public GameObject propbutton;
        public Text Error;
        public Text Huntersleft;
        public Text Propsleft;
        public GameObject Player;
        public GameObject Props;
        public GameObject choisis;
        public int hunternumb;
        public int propnumb;

        private bool DisconnectActive = false;
        public Canvas Disconnect;

        public Canvas TabList;

        private bool unique = false;

        public Canvas BlockView;
        

        private bool GoHunter = false;
        private void Awake()
            
        {
            //Debug.Log(Which.GetProp());
            Propsnumber = 0;
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Menu");
                return;
            }

        }

        // Use this for initialization
        void Start()
        {
            //Prop = Which.GetProp();
            //Spawn();
            MancheIsStart = false;
            AllSpawn = false;
            Disconnect.gameObject.SetActive(false);
            propshunterleft.enabled = false;
            Timer.gameObject.SetActive(false);
            ListPlayerCanvas.gameObject.SetActive(false);
            Lobby.gameObject.SetActive(false);
            hunterbutton.SetActive(false);
            propbutton.SetActive(false);
            TabList.gameObject.SetActive(false);
            BlockView.gameObject.SetActive(false);
           

        }

        // Update is called once per frame
        void Update()
        {
          
            if (GoHunter)
            {
                int i1 = 0;
                int i2 = 0;
                foreach(var p in PhotonNetwork.PlayerList)
                {
                    if(p.NickName[p.NickName.Length-2] == 'H' && p.NickName[p.NickName.Length - 1] == 'V')
                    {
                        i1++;
                    }
                    if (p.NickName[p.NickName.Length - 2] == 'P' && p.NickName[p.NickName.Length - 1] == 'V')
                    {
                        i2++;
                    }
                }
                hunternumb = i1;
                Propsnumber = i2;
               base.photonView.RPC("HunterCanvas", RpcTarget.All);
                base.photonView.RPC("PropCanvas", RpcTarget.All);
            }
            if(GoHunter)
            {
                TimerManche();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                string s = "";

                foreach (var p in PhotonNetwork.PlayerList)
                {
                    s = s + p.NickName.Substring(0,p.NickName.Length-2) + "                                                         ";
                }

                TabList.gameObject.SetActive(true);
                TabList.GetComponentInChildren<Text>().text = s;
            }
            if(Input.GetKeyUp(KeyCode.Tab))
            {
                TabList.gameObject.SetActive(false);
            }
            
           if(Input.GetKeyDown(KeyCode.Escape))
            {
                DisconnectActive = !DisconnectActive;
                if(DisconnectActive)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                Disconnect.gameObject.SetActive(DisconnectActive);
            }

           
            propnumb = Propsnumber;
            hunternumb = Hunternumber;
            //Debug.Log(PhotonNetwork.PlayerList.Length == (Propsnumber + Hunternumber));
            if (PhotonNetwork.PlayerList.Length == (Propsnumber + Hunternumber))
            {
                AllSpawn = true;
                //Debug.Log("Test");
                if(!GoHunter)
                    TimerFct();
            }

            if(AllSpawn && !unique)
            {
                unique = true;
                Timer.gameObject.SetActive(true);
                time = 30f;
                

            }

            Fin();

            /*    if(StartTimer)
            {
                TimerFct();
            }
                if(StartTimerBefore && !StartTimer)
            {
                base.photonView.RPC("TimerBeforeStart", RpcTarget.All);
            }*/
            if (Input.GetKeyDown(KeyCode.M))
            {
                string s = "Nombre de prop : " + Propsnumber.ToString() + " Nb de Hunter : " + Hunternumber.ToString() + " AllSpawn : " + AllSpawn;
                Debug.Log(s);
            }
            if (!MancheIsStart)
            {


                string s = "";

                foreach (var p in PhotonNetwork.PlayerList)
                {
                    s = s + p.NickName;
                    if (Ready.Contains(p.NickName))
                    {
                        s = s + " READY                                                         ";
                    }
                    else
                    {
                        s = s + "                                                                ";
                    }
                }


                base.photonView.RPC("ReadyUpdate", RpcTarget.All, s);
            }


            if (Ready.Count == PhotonNetwork.PlayerList.Length && !MancheIsStart)
            {
                //Debug.Log(time);
                Timer.gameObject.SetActive(true);
                time -= Time.deltaTime;
                string timerstring = ((int)(time)) + " sec";
                Timer.GetComponent<Text>().text = timerstring;
                if (time <= 0 && !MancheIsStart)
                {
                    MancheIsStart = true;
                    propshunterleft.enabled = true;
                    canvas.gameObject.SetActive(true);
                    Timer.gameObject.SetActive(false);
                    ListPlayerCanvas.gameObject.SetActive(false);
                    Lobby.gameObject.SetActive(false);

                    canvas.gameObject.SetActive(true);
                    hunterbutton.SetActive(true);
                    propbutton.SetActive(true);
                    play.SetActive(false);

                    /* for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                     {
                         if ( i % 2 == 0)
                         {
                             SpawnHunter();
                             base.photonView.RPC("addToList", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, Hunters);
                         }
                         else
                         {

                             SpawnProp();
                             base.photonView.RPC("addToList", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, Props);
                         }
                     } */
                }
            }
            else if(!unique)
            {
                Timer.gameObject.SetActive(false);
                time = 6f;
            }

            //ListPlayerCanvas.GetComponent<Text>().text = s;

            //base.photonView.RPC("HunterCanvas", RpcTarget.All);
           // base.photonView.RPC("PropCanvas", RpcTarget.All);



        }
      
        
        public void SpawnProp()
        {

           /* if (Propsnumber == 1 && Hunternumber == 0)
            {
                Error.GetComponent<Text>().text = "Too many props";
            }
            if (Hunternumber == 0)
            {
                Error.GetComponent<Text>().text = "First player must be a hunter";
            }
            
            if (Propsnumber / Hunternumber < 3)*/
            {
                canvas.enabled = false;
                canvas.gameObject.SetActive(false);
                base.photonView.RPC("addToList", RpcTarget.All, 0);
                GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("Prop", spawn.transform.position, spawn.transform.rotation, 0);
                myPlayer.transform.Find("Camera").gameObject.SetActive(true);
                myPlayer.transform.Find("Canvas").gameObject.SetActive(true);
                ((MonoBehaviour)myPlayer.GetComponent("FPSController")).enabled = true;
                choisis = Props;
                base.photonView.RPC("HunterCanvas", RpcTarget.All);
                base.photonView.RPC("PropCanvas", RpcTarget.All);
                
                PhotonNetwork.LocalPlayer.NickName = PhotonNetwork.LocalPlayer.NickName + "PV";
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }

        /*public void OnTriggerEnter()
        {
                Debug.Log("sa marche pas");
                base.photonView.RPC("RemoveToList", RpcTarget.All, 0);
                base.photonView.RPC("HunterCanvas", RpcTarget.All);
                base.photonView.RPC("PropCanvas", RpcTarget.All);
        }*/

        public void SpawnHunter()
        {
            BlockView.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Propsnumber == 0 && Hunternumber == 1)
            {
                Error.GetComponent<Text>().text = "Too many hunters";
            }
            if (Hunternumber == 0 || Propsnumber / Hunternumber >= 3)
            {
                canvas.enabled = false;
                canvas.gameObject.SetActive(false);
                base.photonView.RPC("addToList", RpcTarget.All, 1);
                GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("Player", spawn.transform.position, spawn.transform.rotation, 0);
                myPlayer.transform.Find("Camera").gameObject.SetActive(true);
                myPlayer.transform.Find("Canvas").gameObject.SetActive(true);
                ((MonoBehaviour)myPlayer.GetComponent("FPSController")).enabled = true;
                choisis = Player;
                base.photonView.RPC("HunterCanvas", RpcTarget.All);
                base.photonView.RPC("PropCanvas", RpcTarget.All);
                PhotonNetwork.LocalPlayer.NickName = PhotonNetwork.LocalPlayer.NickName + "HV";

            }
        }

        public void Spawn()
        {
            /*
            Timer.gameObject.SetActive(true);
            StartTimerBefore = true;
            if (StartTimer)
            {*/
            canvas.gameObject.SetActive(false);
            Lobby.gameObject.SetActive(true);
            ListPlayerCanvas.gameObject.SetActive(true);

            /*if (PhotonNetwork.CurrentRoom.PlayerCount % 3 == 0)
                {
                    SpawnHunter();
                }
                else
                {
                    SpawnProp();
                }*/

            //}
        }
        public void DisconnectPlayer()
        {
            base.photonView.RPC("RemoveToList", RpcTarget.All, 0);
            PhotonNetwork.Destroy(choisis);
            StartCoroutine(DisconnectAndLoad());
        }
        IEnumerator DisconnectAndLoad()
        {

            PhotonNetwork.Disconnect();
            while (PhotonNetwork.IsConnected)
            {
                yield return null;
            }
            SceneManager.LoadScene("Menu");
            
        }       
        private void TimerFct()
        {
            //Debug.Log(time);
            time -= Time.deltaTime;
            string timerstring = ((int)(time % 60)) + " sec before Hunters appears";
            Timer.GetComponent<Text>().text = timerstring;
            if (time <= 0)
            {
                Timer.GetComponent<Text>().text = "";
                BlockView.gameObject.SetActive(false);
                time = 300f;
                GoHunter = true;
            }
        }
        [PunRPC]
        void HunterCanvas()
        {
            Huntersleft.text = " "+(Hunternumber.ToString());
        }
        [PunRPC]
        void PropCanvas()
        {
            Propsleft.text = " "+(Propsnumber.ToString()) ;
        }
        [PunRPC]

        private void TimerBeforeStart()
        {
            time -= Time.deltaTime;
            string timerstring = ((int)(time / 60)) + " : " + ((int)(time % 60)) + " left before start";
            Timer.GetComponent<Text>().text = timerstring;
            if (time <= 0)
            {
                time = 180f;

                StartTimer = true;
                Spawn();
            }
        }

        public void TimerManche()
        {
            time -= Time.deltaTime;
            string timerstring = ((int)(time / 60)) + " : " + ((int)(time % 60)) + " left before end";
            Timer.GetComponent<Text>().text = timerstring;
            if (time <= 0)
            {
                Hunternumber = 0;
                Timer.GetComponent<Text>().text = "";
            }
        }


        public void IsReady()
        {
            if (!Ready.Contains(PhotonNetwork.LocalPlayer.NickName))
            {

                Debug.Log(PhotonNetwork.LocalPlayer.NickName);
                //base.photonView.RPC("TestAdd", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName);
                base.photonView.RPC("addReady", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName);
            }
            else
            {
                //base.photonView.RPC("Ready.Remove", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName);
                base.photonView.RPC("notReady", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName);
            }
        }
        [PunRPC]
        void addReady(string s)
        {
            Ready.Add(s);
        }
        [PunRPC]
        void notReady(string s)
        {
            Ready.Remove(s);
        }
        [PunRPC]
        void ReadyUpdate(string s)
        {
            ListPlayerCanvas.GetComponent<Text>().text = s;
        }
        [PunRPC]
        void addToList(int classe)
        {
           if ( classe == 1)
            {
                Hunternumber += 1;
            }
           else
            {
                Propsnumber+= 1;
            }

           /* Debug.Log(Hunternumber);
            Debug.Log(Propsnumber );
            */
                              
        }

        [PunRPC]
        void RemoveToList(int classe)
        {
            if (classe == 1)
            {
                Hunternumber -= 1;
            }
            else
            {
                Propsnumber -= 1;
            }

            Debug.Log(Hunternumber + "hunter");
            Debug.Log(Propsnumber + "props");

        }

        [PunRPC]

        void TestAdd(string s)
        {
            Ready.Add(s);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.IsWriting)
            {
                stream.SendNext(Ready);
            }
            else if(stream.IsReading)
            {
                Ready = ((List<string>)stream.ReceiveNext());
            }
        }

       public void DieProp()
       {
           base.photonView.RPC("RemoveToList", RpcTarget.All, 0);
       }

       /* public void DieHunter()
        {
            base.photonView.RPC("RemoveToList", RpcTarget.All, 1);
        }*/
       
        [PunRPC]
        void Test()
        {
            Debug.Log("oui");
            Propsnumber--;
        }

        public void Fin()
        {
            //string s = "Nombre de prop : " + Propsnumber.ToString() + " Nb de Hunter : " + Hunternumber.ToString();
            //Debug.Log(s);
            if(Hunternumber <= 0 && AllSpawn)
            {
                Victory.GetComponent<Text>().text = "Props won";
                Timer.gameObject.SetActive(false);
            }
            if (Propsnumber <= 0 && AllSpawn)
            {
                Victory.GetComponent<Text>().text = "Hunter won";
                Timer.gameObject.SetActive(false);
            }
        }


    }




}