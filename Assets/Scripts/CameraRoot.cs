using UnityEngine;
/// <summary>
/// used for making the camera position be the right place but not the rotation since the pivot point it attached to the head and caused bobbing.
/// </summary>
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
