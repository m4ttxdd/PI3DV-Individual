using StarterAssets;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    public Animator animator;
    public FirstPersonController FPSController;

    public float speedChangeRate = 0.05f;

    public StarterAssets.StarterAssetsInputs inputs;

    private void Update()
    {
        if (inputs.attack)
        {
            animator.SetTrigger("attack");
            inputs.attack = false;
        }
        else if (inputs.block)
        {
            animator.SetBool("block", true);
            inputs.block = false;
        }

            var targetSpeed = FPSController._speed / FPSController.SprintSpeed;
        var speed = Mathf.MoveTowards(animator.GetFloat("speed"), targetSpeed, speedChangeRate);

        animator.SetFloat("speed", speed);
    }

}
