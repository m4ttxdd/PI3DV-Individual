using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    public Transform root;

    public bool isRagdoll = false;
    public void Update()
    {
        transform.position = root.position;

        if (isRagdoll)
        {
            transform.rotation = root.rotation;
        }
    }
}
