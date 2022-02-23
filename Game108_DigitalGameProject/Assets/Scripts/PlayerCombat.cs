using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //public Animator animator;

    public Transform attackPointHigh;
    public Transform attackPointMid;
    public Transform attackPointLow;

    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.W))
        {
            AttackHigh();
        } else if (Input.GetButtonDown("Fire1") && Input.GetButton("Crouch"))
        {
            AttackLow();
        } else if (Input.GetButtonDown("Fire1"))
        {
            AttackMid();
        }
    }

    void AttackHigh()
    {
        //Debug.Log("High Attack");
        //Play an attack animation

        //Detect enemies in range of attack

        //Deal damage

    }

    void AttackMid()
    {
        //Debug.Log("Mid attack");
        //Play an attack animation

        //Detect enemies in range of attack

        //Deal damage

    }

    void AttackLow()
    {
        //Debug.Log("Low attack");
        //Play an attack animation

        //Detect enemies in range of attack

        //Deal damage

    }
}
