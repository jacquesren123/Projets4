using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Projectil;
    public int force = 10;
    public int ammo = 2;
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammo > 0)
        {
            ammo -= 1;
            GameObject Boule = Instantiate(Projectil, transform.position, Quaternion.identity) as GameObject;
            Boule.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * force);
            Destroy(Boule, 5f);

        }
    }



}