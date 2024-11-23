using System.IO;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float health = 100f;

    private bool dead = false;

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
        if (dead) { return; }

        if (TryGetComponent(out RagdollEnabler ragdoll))
        {
            ragdoll.ToggleRagdoll();
        }
        Debug.Log("Character died");
        dead = true;
    }
}
