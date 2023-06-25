using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public bool isDead { get; private set; }

    private float currentHealth;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("当前生命值为" + currentHealth);

        if(currentHealth <= 0 && !isDead)
        {
            isDead =true;
            Destroy(gameObject, 2f);
        }
    }
}
