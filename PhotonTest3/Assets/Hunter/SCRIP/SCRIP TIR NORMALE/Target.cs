using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    public float health = 50f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }


    void Die()
    {
        Destroy(this.gameObject);
    }
}
