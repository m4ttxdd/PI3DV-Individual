using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacks : MonoBehaviour
{
    [Header("Player only")]
    public StarterAssets.StarterAssetsInputs inputs;
    public Collider shieldCollider;
    public float blockActiveTime = 2f;
    public bool canBlock = true;

    [Header("Player and Enemy")]
    public Collider swordCollider;

    public float attackActiveTime = 1f;

    public bool canAttack = true;

    CharacterAnimations characterAnimations;
    Sword sword;

    bool isEnemy;

    [Header("Enemy only")]
    public bool shouldAttack = false;

    private void Start()
    {
        characterAnimations = GetComponent<CharacterAnimations>();
        sword = swordCollider.gameObject.GetComponent<Sword>();

        isEnemy = gameObject.layer != LayerMask.NameToLayer("Player");
    }
    private void Update()
    {
        if(!isEnemy)
        {
            PlayerUpdate();
        }
        else
        {
            EnemyUpdate();
        }
    }

    private void PlayerUpdate()
    {
        if (inputs.attack && canAttack)
        {
            sword.ClearHitOpponents();
            Attack();
            characterAnimations.Attack();
            inputs.attack = false;
        }else if(inputs.block && !canAttack)//cancel atk with block
        {
            StopAttack();
            Block();
            characterAnimations.Block();
            inputs.block = false;
        }
        else if (inputs.block && canBlock)
        {
            Block();
            characterAnimations.Block();
            inputs.block = false;
        }
        else if (inputs.attack && !canAttack)
        {
            inputs.attack = false;
        }
        else if (inputs.block && !canBlock)
        {
            inputs.block = false;
        }
    }

    private void EnemyUpdate()
    {
        if(shouldAttack && canAttack)
        {
            sword.ClearHitOpponents();
            Attack();
            characterAnimations.Attack();
            shouldAttack = false;
        }
        else if (shouldAttack && !canAttack)
        {
            shouldAttack = false;
        }
    }

    private void Attack()
    {
        swordCollider.enabled = true;
        canAttack = false;
        Invoke(nameof(StopAttack), attackActiveTime);
    }

    private void StopAttack()
    {
        canAttack = true;
        swordCollider.enabled = false;
    }

    private void Block()
    {
        shieldCollider.enabled = true;
        canBlock = false;
        Invoke(nameof(StopBlock), blockActiveTime);
    }

    private void StopBlock()
    {
        canBlock = true;
        shieldCollider.enabled = false;
    }
}
