using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GRENADE : MonoBehaviour
{
    float force = 400;
    float timer = 2;
    float countdown;
    bool hasExploded;
    float radius = 5;
    float damage = 5;
    private float timeLeft;
    public Image img;
    public GameObject Canvas;
    public Camera fpsCam;

    [SerializeField] GameObject exploParticle;
    // Start is called before the first frame update
    void Start()
    {
        countdown = timer;
     

    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        timeLeft -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded)
        {
            Explode();
        }              
    }
    void Explode()
    {
       GameObject spawnedParticle = Instantiate (exploParticle, transform.position, transform.rotation);
        Destroy(spawnedParticle, 1);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();           
            if (rb !=null)
            {
                
                rb.AddExplosionForce(force, transform.position, radius);              
            }
            
            
        }
        foreach (Collider carre in colliders)
        {
            Target target = carre.GetComponent<Target>();
            if (target != null)
            {

                target.TakeDamage(damage);
                NewMethod();
               
            }
        }
        
        hasExploded = true;
        Destroy(gameObject);
        
        
    }

    private void NewMethod()
    {
        img.color = Color.red;
        timeLeft = 0.25f;
    }


}
