using System.IO;
using UnityEngine;
/// <summary>
/// general character script for handling health and death
/// </summary>
public abstract class Character : MonoBehaviour
{
    public event System.Action TakeDamageEvent;
    public event System.Action OnDeath;

    public float health = 100f;

    private bool dead = false;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        TakeDamageEvent?.Invoke();//event used for updating healthbar
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (dead) { return; }

        OnDeath?.Invoke();//event used for disabling healthbar

        if (TryGetComponent(out RagdollEnabler ragdoll))//if the character has a ragdoll enable it
        {
            ragdoll.ToggleRagdoll();
        }
        Debug.Log("Character died");
        dead = true;
    }
}
