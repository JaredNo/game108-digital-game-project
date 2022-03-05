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
        Debug.Log("High Attack");
        //Play an attack animation

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointHigh.position, attackRange, enemyLayers);

        //Deal damage
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
        }

    }

    void AttackMid()
    {
        Debug.Log("Mid attack");
        //Play an attack animation

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointMid.position, attackRange, enemyLayers);

        //Deal damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
        }

    }

    void AttackLow()
    {
        Debug.Log("Low attack");
        //Play an attack animation

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);

        //Deal damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointHigh == null || attackPointMid == null || attackPointLow == null)
            return;

        Gizmos.DrawWireSphere(attackPointHigh.position, attackRange);
        Gizmos.DrawWireSphere(attackPointMid.position, attackRange);
        Gizmos.DrawWireSphere(attackPointLow.position, attackRange);
    }
}
