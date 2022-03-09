using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Combat : MonoBehaviour
{
    public Animator Animator;

    public float attackDamage;

    public Transform attackPointHigh;
    public Transform attackPointMid;
    public Transform attackPointLow;

    public float attackRange = 0.5f;

    public float maxHealth;
    float currentHealth;

    public LayerMask enemyLayers;

    public float attackRate;
    float nextAttackTime;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire2") && Input.GetKey(KeyCode.UpArrow))
            {
                AttackHigh();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetButtonDown("Fire2") && Input.GetButton("Crouch2"))
            {
                AttackLow();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                AttackMid();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void AttackHigh()
    {
        Debug.Log("High Attack");
        Animator.SetTrigger("High Hit");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointHigh.position, attackRange, enemyLayers);

        //Deal damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }

    }

    void AttackMid()
    {
        Debug.Log("Mid attack");
        Animator.SetTrigger("Mid Hit");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointMid.position, attackRange, enemyLayers);

        //Deal damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
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
            enemy.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //play hurt animation

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("" + this.name + " died");
        GetComponent<SpriteRenderer>().enabled = false; //this is just a debug line to see the player disapear

        //Play death animation
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
