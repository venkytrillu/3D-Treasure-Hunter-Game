using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
  
    public bool  isDead;
    public float Health = 100f;


    public void ApplayDamage(float damage)
    {
        if (isDead)
            return;
        Health -= damage;
        Controller.instance.SetSlider(Health);
        Die();
    }

   public void Die()
    {
        if(Health<=0 && Controller.instance.GetoxygenCount()==0)
        {
            isDead = true;
            Controller.instance.GameOver();
            Destroy(gameObject.GetComponent<CharacterController>());
            Destroy(gameObject.GetComponent<PlayerMovement>());
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
        else if(Health <= 0&&Controller.instance.GetoxygenCount()>0)
        {
            Controller.instance.SetOxygenCount(-1);
            Health = 100;
        }
    }




} // class





































