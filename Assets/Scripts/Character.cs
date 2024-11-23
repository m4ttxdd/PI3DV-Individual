using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float health = 100f;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Character died");
    }
}
