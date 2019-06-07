using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public GameObject Canvas;
    public Image img;
    private float timeLeft;
    public Camera fpsCam; 
    public int ballestotal = 60; 
    public int chargeur = 20;
    public Transform shotText;
    public Transform ballesText;
    private float firerate = 10;
    // Start is called before the first frame update

    void Start()
    {
        img = Canvas.GetComponent<Image>();
        
    }
    void ammo()
    {
        if (chargeur > 0)
        chargeur -= 1;
    }

    void ui()
    {
        shotText.GetComponent<Text>().text = chargeur.ToString();
    }



    // Update is called once per frame
    void Update()
    {        
        if(Input.GetButtonDown("Fire1") && chargeur > 0)
        {            
            InvokeRepeating("Shoot", 0f, 1/firerate);          
            InvokeRepeating("ammo", 0f, 1 / firerate);
            InvokeRepeating("ui", 0f, 1 / firerate);
        }
        else if (Input.GetButtonUp("Fire1") || chargeur <= 0) 
        {
            CancelInvoke("Shoot");
            CancelInvoke("ui");
            CancelInvoke("ammo");
        }
        timeLeft -= Time.deltaTime;
        if (img.color == Color.red && timeLeft <= 0)
        {
            img.color = Color.white;
        }
        if(Input.GetKey(KeyCode.R) && ballestotal > 0)
        {
            if (ballestotal - (20 - chargeur) > 0)
            {
                ballestotal = ballestotal - (20 - chargeur);
                chargeur = 20;
            }
            else
            {
                chargeur += ballestotal;
                ballestotal = 0;
            }
            
            ballesText.GetComponent<Text>().text = ballestotal.ToString();
            shotText.GetComponent<Text>().text = chargeur.ToString();
        }

    }
  
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {


            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                img.color = Color.red;
                timeLeft = 0.25f;
            }
            
        }
    }
}
