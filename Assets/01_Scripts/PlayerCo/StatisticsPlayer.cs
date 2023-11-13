using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadisticsPlayer : MonoBehaviour
{
    public int life=100;
    public bool invincible = false;
    public float timeInvincible = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubtractLife(int amount) 
    {
        if (!invincible && life>0)
        {
            life -= amount;
            StartCoroutine(Invulnerator());
            if (life ==0)
            {
                DeadPlayer();
            }
        }
        
    }
    void DeadPlayer()
    {
        Debug.Log("GAME OVER!!");
    }
    IEnumerator Invulnerator() 
    { 
        invincible = true;
        yield return new WaitForSeconds(timeInvincible);
        invincible = false;
    
    }
}
