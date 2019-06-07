using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class bonusdie : MonoBehaviourPun
{
    public GameObject lola;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [PunRPC]
    void die()
    {
        Destroy(lola);
    }
}
