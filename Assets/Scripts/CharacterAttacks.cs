using UnityEngine;

public class CharacterAttacks : MonoBehaviour
{
    public MeshCollider swordCollider;
    public MeshCollider shieldCollider;

    public float attackActiveTime = 1f;
    public float blockActiveTime = 2f;

    public void Attack()
    {
        swordCollider.enabled = true;
        Invoke(nameof(StopAttack), attackActiveTime);
    }

    private void StopAttack()
    {
        swordCollider.enabled = false;
    }

    public void Block()
    {
        shieldCollider.enabled = true;
        Invoke(nameof(StopBlock), blockActiveTime);
    }

    private void StopBlock()
    {
        shieldCollider.enabled = false;
    }
}
