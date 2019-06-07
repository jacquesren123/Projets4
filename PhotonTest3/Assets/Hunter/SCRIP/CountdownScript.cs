
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;
   
    

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    private void Start()
    {
        timer = mainTimer;
    }

    private void Update()
    {
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F");
        }
        else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0.00";
            timer = 0.0f;
            player.transform.position = respawnPoint.transform.position;
        }
    }
    public void ResetBtn()
    {
        timer = mainTimer;
        canCount = true;
        doOnce = false;
    }
}
