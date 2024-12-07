using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// enemy controller
/// </summary>

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

        if(player == null)//make sure player is set
        {
            player = Camera.main.transform.Find("PlayerMove");
        }

        characterAttacks.swordCollider.gameObject.GetComponent<Sword>().OnBlock += Stunned; //not clean but w/e subscribe to stunned when getting blocked

        StartCoroutine(StateChecker());//start the coroutines responsible for vision and action
        StartCoroutine(CheckVision());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator StateChecker()
    {
        while (true)//every .1 second if not stunned go through the if statement and see what it should do
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
                FaceTarget();//make the agent face the player slowly
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

    void FaceTarget()

    {//not the best implementation since it only runs every .1 second but it works fine
        Vector3 direction = (player.transform.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

    }

    private IEnumerator CheckVision()
    {
        while (true)//check vision ever .1 sec
        {
            VisionCone();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void VisionCone()
    {
        bool playerCurrentlyInSight = false;

        if (Physics.CheckSphere(transform.position, visionRange, playerMask))//check if player in vision range
        {
            Vector3 dirToTarget = player.position - transform.position;//find direction
            if (Vector3.Angle(transform.forward, dirToTarget) < visionAngle / 2)//find if angle between forward and direction to player is less than vision angle / 2
            {
                float distance = Vector3.Distance(transform.position, player.position);//get distance

                if (!Physics.Raycast(transform.position + Vector3.up * .7f, dirToTarget, distance, environment))//check if environment layers are in the way so it wont look through objects
                {
                    playerCurrentlyInSight = true;
                }
            }
        }

        if (playerCurrentlyInSight)//make the enemy keep chasing the player until is has not seen the player for 3 secons, so it will not come to a halt whenever a small object is between them
        {
            playerInSight = true;
            timeSinceLastSeen = 0f;
        }
        else
        {
            timeSinceLastSeen += 0.1f; //increment by the wait time in CheckVision :):):)
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
