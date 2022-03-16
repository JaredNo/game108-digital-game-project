using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public float attackDamage;

    public GameObject otherPlayer;

    public Transform attackPointHigh;
    public Transform attackPointMid;
    public Transform attackPointLow;

    public float attackRange = 0.5f;

    public float maxHealth;
    float currentHealth;

    public LayerMask enemyLayers;

    public float attackRate;
    float nextAttackTime;

    public Slider slider;

    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    void Update()
    {
        if (currentHealth < slider.value)
        {
            slider.value--;
            if (slider.value <= 0)
            {
                this.GetComponent<PlayerMovement>().enabled = false;
                this.enabled = false;
            }
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.W))
            {
                AttackHigh();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetButtonDown("Fire1") && Input.GetButton("Crouch"))
            {
                AttackLow();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                AttackMid();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (currentHealth <= 0)
        {
            this.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    void AttackHigh()
    {
        Debug.Log("High Attack");

        animator.SetTrigger("High Hit");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointHigh.position, attackRange, enemyLayers);

        //Deal damage
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Player2Combat>().TakeDamageHigh(attackDamage);
        }

    }

    void AttackMid()
    {
        Debug.Log("Mid attack");

        animator.SetTrigger("Mid Hit");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointMid.position, attackRange, enemyLayers);

        //Deal damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Player2Combat>().TakeDamageMid(attackDamage);
        }

    }

    void AttackLow()
    {
        Debug.Log("Low attack");

        animator.SetTrigger("Low Hit");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);

        //Deal damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Player2Combat>().TakeDamageLow(attackDamage);
        }

    }

    public void TakeDamageHigh(float damage)
    {
        currentHealth -= damage;

        

        if (currentHealth <= 0)
            Die();
    }

    public void TakeDamageMid(float damage)
    {
        currentHealth -= damage/2;

        animator.SetTrigger("Mid Hurt");

        if (currentHealth <= 0)
            Die();
    }

    public void TakeDamageLow(float damage)
    {
        currentHealth -= damage;

        

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("" + this.name + " died");
        otherPlayer.GetComponent<Player2Combat>().Victory();
        //Play death animation
        
    }

    public void Victory()
    {
        //play victory animation

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
