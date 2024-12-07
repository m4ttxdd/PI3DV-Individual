using NUnit.Framework;
using UnityEngine;
using UnityEngine.XR;

public class RagdollEnabler : MonoBehaviour
{
    public Transform ragdollRoot;
    public bool startEnabled = false;
    public Component[] componentsToDisable;
    public CameraRoot cameraRoot;

    private Animator animator;
    private Collider[] colliders;
    private Rigidbody[] rigidbodies;
    private CharacterJoint[] characterJoints;

    private PhysicsMaterial physicsMaterial;

    private int originalLayer;
    private int pushLayer;

    private void Start()
    {
        originalLayer = gameObject.layer;
        pushLayer = LayerMask.NameToLayer("Pushable");

        //cache all components we need to disable and enable
        rigidbodies = ragdollRoot.GetComponentsInChildren<Rigidbody>();
        colliders = ragdollRoot.GetComponentsInChildren<Collider>();
        characterJoints = ragdollRoot.GetComponentsInChildren<CharacterJoint>();
        animator = ragdollRoot.GetComponent<Animator>();

        //create a phys mat for the ragdoll so it looks better
        physicsMaterial = new PhysicsMaterial
        {
            dynamicFriction = 0.5f,
            staticFriction = 0.9f,
            bounciness = 0.1f,
            frictionCombine = PhysicsMaterialCombine.Maximum,
            bounceCombine = PhysicsMaterialCombine.Maximum
        };


        if (startEnabled)
        {
            EnableRagdoll();
        }
        else
        {
            DisableRagdoll();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && transform.root == Camera.main.transform.root)
        {
            ToggleRagdoll();
        }
    }

    public void EnableRagdoll()//enables and disables relevant components
    {
        animator.enabled = false;

        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
        }

        foreach (var joint in characterJoints)
        {
            joint.enableCollision = true;
        }

        foreach (var collider in colliders)
        {
            collider.material = physicsMaterial;
            collider.isTrigger = false;
            collider.gameObject.layer = pushLayer;
        }

        foreach (var component in componentsToDisable)
        {
            if (component is Collider collider)
            {
                collider.enabled = false;
            }
            else if (component is Behaviour behaviour)
            {
                behaviour.enabled = false;
            }
            else
            {
                Debug.LogWarning("Component type not supported: " + component.GetType());
            }
        }

        if(cameraRoot)
        {
            cameraRoot.isRagdoll = true;//will make camera fall as seen from first person
        }
    }

    public void DisableRagdoll()//enables and disables relevant components
    {
        animator.enabled = true;

        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

        foreach (var joint in characterJoints)
        {
            joint.enableCollision = false;
        }

        foreach (var collider in colliders)
        {
            collider.material = new PhysicsMaterial();
            collider.isTrigger = true;
            collider.gameObject.layer = originalLayer;
        }

        foreach (var component in componentsToDisable)
        {
            if (component is Collider collider)
            {
                collider.enabled = true;
            }
            else if (component is Behaviour behaviour)
            {
                behaviour.enabled = true;
            }
            else
            {
                Debug.LogWarning("Component type not supported: " + component.GetType());
            }
        }

        if(cameraRoot)
        {
            cameraRoot.isRagdoll = false;
        }
    }

    public bool ToggleRagdoll()
    {
        if (animator.enabled)
        {
            EnableRagdoll();
            return true;
        }
        else
        {
            DisableRagdoll();
            return false;
        }
    }
}
