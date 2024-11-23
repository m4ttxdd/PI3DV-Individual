using UnityEngine;

public class Sword : MonoBehaviour
{
    public string targetLayer = "Enemy";

    public float dmg = 20f;

    public Transform direction;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pushable"))
        {
            other.GetComponent<Rigidbody>().AddForce((direction?.forward ?? transform.forward) * 1000);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<Character>().TakeDamage(dmg);
            Debug.Log("Enemy hit!");
        }

    }
}
