using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    [SerializeField] private int healthPoints;
    private Animator currentAnim;
    private int randomDistraction;
    private bool isDistracted = false;
    private bool isWorking = true;
    private bool isDead = false;
    private GameManager gameManager;
    

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentAnim = GetComponent<Animator>();
        currentAnim.SetInteger("randomDistraction", 0);
        isDead = false;
    }

    public void Update()
    {
        if (isWorking)
        {
            gameManager.LevelProgress();
        }

        if (currentAnim.GetInteger("randomDistraction") == 0 && !isDistracted)
        {
            Invoke("SetRandomDistraction", Random.Range(8, 26));
            isDistracted = true;
        }
    }

    public void SetRandomDistraction()
    {
        currentAnim.SetInteger("randomDistraction", Random.Range(1, 6));
        isWorking = false;
    }

    public void OnCollisionEnter(Collision col)
    {
        Hit();

        if (isWorking && isDistracted)
        {
            gameManager.DecreaseProgress();
        }

        if(col.gameObject.tag == "bullet")
        {
            TakeDamage();
        }
    }

    public void Hit()
    {
        Debug.Log("Hit target!");
        Debug.Log("Distraction = " + currentAnim.GetInteger("randomDistraction"));

        if (currentAnim.GetInteger("randomDistraction") != 0)
        {
            currentAnim.SetInteger("randomDistraction", 0);
            isDistracted = false;
            isWorking = true;
        }
    }

    public void TakeDamage()
    {
        healthPoints -= 50;
        Debug.Log("-50 dmg");
        
        if (healthPoints <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        isWorking = false;
        isDead = true;
        //play death animation
        //substract count of humans
        Object.Destroy(gameObject, 3.0f);
    }
}
