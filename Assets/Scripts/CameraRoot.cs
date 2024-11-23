using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    public Transform root;

    public void Update()
    {
        if (root != null)
        {
            transform.position = root.position;
            //transform.rotation = Quaternion.Euler(root.rotation.eulerAngles.x, 0 , root.rotation.eulerAngles.z);
            
        }
    }
}
