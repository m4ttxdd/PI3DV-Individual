using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float visionRange;
    public float visionAngle;

    public float attackRange;

    NavMeshAgent agent;

    public LayerMask playerMask;
    public LayerMask environment;

    public Transform player;
    public bool playerInSight;

    private CharacterAttacks characterAttacks;
    private CharacterAnimations characterAnimations;

    private float timeSinceLastSeen;
    public const float sightTimeout = 3.0f;

    private bool stunned = false;
    public float stunTime = 3f;
    private void Start()
    {
        characterAnimations = GetComponent<CharacterAnimations>();
        agent = GetComponent<NavMeshAgent>();
        characterAttacks = GetComponent<CharacterAttacks>();

        characterAttacks.swordCollider.gameObject.GetComponent<Sword>().OnBlock += Stunned; //idc

        StartCoroutine(StateChecker());
        StartCoroutine(CheckVision());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator StateChecker()
    {
        while (true)
        {
            if(stunned)
            {
                characterAttacks.shouldAttack = false;
                yield return new WaitForSeconds(stunTime);
                stunned = false;

            }

            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                characterAttacks.shouldAttack = true;
                agent.SetDestination(transform.position);
            }
            else if (playerInSight)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.SetDestination(transform.position);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator CheckVision()
    {
        while (true)
        {
            VisionCone();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void VisionCone()
    {
        bool playerCurrentlyInSight = false;

        if (Physics.CheckSphere(transform.position, visionRange, playerMask))
        {
            Vector3 dirToTarget = player.position - transform.position;
            if (Vector3.Angle(transform.forward, dirToTarget) < visionAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distance, environment))
                {
                    playerCurrentlyInSight = true;
                }
            }
        }

        if (playerCurrentlyInSight)
        {
            playerInSight = true;
            timeSinceLastSeen = 0f;
        }
        else
        {
            timeSinceLastSeen += 0.1f; //increment by the wait time in CheckVision cba to do it better :):):)
            if (timeSinceLastSeen >= sightTimeout)
            {
                playerInSight = false;
            }
        }
    }

    private void Stunned()
    {
        stunned = true;
        characterAnimations.GotBlocked();
        agent.SetDestination(transform.position);
    }
}
