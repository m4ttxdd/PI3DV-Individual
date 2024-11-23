using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimations : MonoBehaviour
{
    public Animator animator;
    public FirstPersonController FPSController;
    private NavMeshAgent agent;

    public float speedChangeRate = 0.05f;

    private float normalisedSpeed;

    public bool isEnemy = false;

    public void Start()
    {
        if (isEnemy)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        normalisedSpeed = SetSpeed(isEnemy);

        var speed = Mathf.MoveTowards(animator.GetFloat("speed"), normalisedSpeed, speedChangeRate);

        animator.SetFloat("speed", speed);
    }

    public float SetSpeed(bool isEnemy)
    {
        if (!isEnemy)
        {
            return FPSController._speed / FPSController.SprintSpeed;
        }
        else
        {
            return agent.velocity.magnitude / agent.speed;
        }
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void Block()
    {
        animator.SetTrigger("block");
    }

    public void GotBlocked()
    {
        animator.SetTrigger("gotBlocked");
    }
}
