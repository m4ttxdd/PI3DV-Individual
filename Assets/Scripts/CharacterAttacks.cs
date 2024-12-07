using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// script responsible for attacks and timings, used for both enemy and player again, should probably be in different scripts but doesnt matter for this scale ig
/// </summary>
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
    public float attackDelayTime = 0f;

    public bool canAttack = true;

    CharacterAnimations characterAnimations;
    Sword sword;

    bool isEnemy;

    [Header("Enemy only")]
    public bool shouldAttack = false;

    public float attackCooldown = .4f;
    
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
        //inputs.attack becomes true when attack button is clicked and will only be disabled after this method has run
        //logic is happening through a huge if statement, but it works
        
        if (inputs.attack && canAttack)//if attack clicked and we can attack meaning no other attack is ongoing we attack
        {
            sword.ClearHitOpponents();//clears the hashset used to make sure we dont hit the same opponent twice
            Attack();
            characterAnimations.Attack();
            inputs.attack = false;
        }else if(inputs.block && !canAttack)//cancel atk with block, for more reactive gameplay, the !canAttack is only true when attack is ongoing so yea
        {
            StopAttack();
            Block();
            characterAnimations.Block();
            inputs.block = false;
        }
        else if (inputs.block && canBlock)// block is clicked and we can block meaning no other block is ongoing we block
        {
            Block();
            characterAnimations.Block();
            inputs.block = false;
        }
        else if (inputs.attack && !canAttack)//you could remove these last two if statements and then the attack and blocks would have a semi queued system but i didnt like it
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
        if(shouldAttack && canAttack)//just like the player attacks but shouldattack is set from EnemyController when player in attackrange
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

    private void Attack()//enable the collider for weapon and call a method to stop it and disable it after a time
    {
        if (attackDelayTime <= 0)
        {
            swordCollider.enabled = true;
        }
        else
        {
            Invoke(nameof(EnableSwordCollider), attackDelayTime);
        }
        
        canAttack = false;
        Invoke(nameof(StopAttack), attackActiveTime);
    }

    private void EnableSwordCollider()
    {
        swordCollider.enabled = true;
    }

    private void CanAttackCooldown()
    {
        canAttack = true;
    }

    private void StopAttack()
    {
        swordCollider.enabled = false;

        if (isEnemy)
        {
            Invoke(nameof(CanAttackCooldown), attackCooldown);
        }
        else//play no cooldown on attack
        {
            canAttack = true;
        }
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
