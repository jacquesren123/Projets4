using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Test
{
    public class NetwworkConnectionManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        public Button BtnConnectMaster;
        public Button BtnConnectRoom;

        public InputField username;

        public bool TriesToConnectToMaster;
        public bool TriesToConnectToRoom;

        public Transform Error;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            TriesToConnectToMaster = false;
            TriesToConnectToRoom = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(BtnConnectMaster != null)
                BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
            if(BtnConnectRoom != null)
                BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToRoom);
 
        }

        public void OnClickConnectToMaster()
        {
            //Settings(all optional and only for tutorial purpose)
            PhotonNetwork.OfflineMode = false;  //true would "fake" connection
            PhotonNetwork.NickName = "PlayerName" + Random.Range(1,500);
            //PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "v1"; //Seuls les joueurs avec la meme version peucent jouer ensemble

            TriesToConnectToMaster = true;
            //PhotonNetwork.ConnectToMaster(ip, port, appid) //manual connetion
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            TriesToConnectToMaster = false;
            TriesToConnectToRoom = false;
            Debug.Log(cause);
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            TriesToConnectToMaster = false;
            Debug.Log("Connected to Master");
        }

        public void OnClickConnectToRoom()
        {
            bool IsExist = false;
            foreach (var p in PhotonNetwork.PlayerList)
            {
                if (!IsExist)
                {
                    IsExist = username.text == p.NickName;
                }
            }
            if (!IsExist && username.text != "")
            {
                PhotonNetwork.NickName = username.text ;

                if (!PhotonNetwork.IsConnected)
                    return;

                TriesToConnectToRoom = true;
                //PhotonNetwork.CreateRoom("Player's Game 1"); //Create a specific room err: OnCreateRoomFailed
                //PhotonNetwork.JoinRoom("Player's Game 1"); //Join a specific room ù err : OnJoinRoomFailed
                PhotonNetwork.JoinRandomRoom();  //Join a random room err: OnJoinRandomRoomFailed
            }
            else
            {
                if(IsExist)
                {
                    Error.GetComponent<Text>().text = "PSEUDO ALREADY TAKEN";
                }
                else
                {
                    Error.GetComponent<Text>().text = "THIS FIELD IS OBLIGATORY";
                }
            }
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            TriesToConnectToRoom = false;
            Debug.Log("Master : " + PhotonNetwork.IsMasterClient + " | Players in Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name);
            SceneManager.LoadScene("SampleScene");
            PhotonNetwork.LoadLevel(2);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            //No room available
            //Create a room
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            Debug.Log(message);
            base.OnCreateRoomFailed(returnCode, message);
            TriesToConnectToRoom = false;
        }
        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            base.photonView.RPC("RemoveToList", RpcTarget.All, 0);
            PhotonNetwork.DestroyPlayerObjects(otherPlayer);
            base.OnPlayerLeftRoom(otherPlayer);
            Debug.Log(otherPlayer.NickName + "has left the game");
        }


    }
}
