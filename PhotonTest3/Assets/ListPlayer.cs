using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListPlayer : MonoBehaviour
{
    public Transform ListCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        ListCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ListCanvas.gameObject.SetActive(true);
            string s = "";
            Player[] listPlayer = PhotonNetwork.PlayerList;
            foreach (var p in listPlayer)
            {
                s = s + " - " + p.NickName;
            }
            ListCanvas.GetComponent<Text>().text = s;
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            ListCanvas.gameObject.SetActive(false);
        }
    }
}
