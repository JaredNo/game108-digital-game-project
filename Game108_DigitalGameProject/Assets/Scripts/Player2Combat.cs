using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Combat : MonoBehaviour
{
    public Animator Animator;

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
                this.GetComponent<Player2Movement>().enabled = false;
                this.enabled = false;
            }
        }

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
            enemy.GetComponent<PlayerCombat>().TakeDamageHigh(attackDamage);
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
            enemy.GetComponent<PlayerCombat>().TakeDamageMid(attackDamage);
        }

    }

    void AttackLow()
    {
        Debug.Log("Low attack");

        Animator.SetTrigger("Low Hit");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);

        //Deal damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<PlayerCombat>().TakeDamageLow(attackDamage);
        }

    }

    public void TakeDamageHigh(float damage)
    {
        currentHealth -= damage;

        Animator.SetTrigger("High Hurt");

        if (currentHealth <= 0)
            Die();
    }

    public void TakeDamageMid(float damage)
    {
        currentHealth -= damage/2;

        Animator.SetTrigger("Mid Hurt");

        if (currentHealth <= 0)
            Die();
    }

    public void TakeDamageLow(float damage)
    {
        currentHealth -= damage;

        Animator.SetTrigger("Low Hurt");

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("" + this.name + " died");
        otherPlayer.GetComponent<PlayerCombat>().Victory();

        Animator.SetTrigger("Death");
    }

    public void Victory()
    {
        Animator.SetTrigger("Victory");

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
