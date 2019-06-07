using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkCharacter : MonoBehaviourPun , IPunObservable

{
    Vector3 realPosition;
    Quaternion realRotation;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, .1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, .1f);
        }

    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(anim.GetFloat("Speed"));
            stream.SendNext(anim.GetBool("Jump"));
            stream.SendNext(anim.GetBool("Crouch"));
        }
        else
        {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
            anim.SetFloat("Speed", (float)stream.ReceiveNext());
            anim.SetBool("Jump", (bool)stream.ReceiveNext());
            anim.SetBool("Crouch", (bool)stream.ReceiveNext());

        }

    }

   
}
