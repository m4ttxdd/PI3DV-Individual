using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public string targetLayer = "Enemy";

    public float dmg = 20f;

    public Transform direction;

    public event System.Action OnBlock;

    private HashSet<Transform> hitOpponents = new HashSet<Transform>(); //to make sure an attack can only damage a target once per attack
    private bool blocked = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pushable"))//if hit physics object push it
        {
            if(other.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce((direction?.forward ?? transform.forward) * 1000);
            }
        }

        if (other.gameObject.TryGetComponent(out Shield shield))//if hit shield get stunned
        {
            if (shield.transform.root != transform.root)
            {
                //Debug.Log("BLOCKED");
                hitOpponents.Add(other.transform.root);
                if (!blocked)
                {
                    blocked = true;
                    OnBlock?.Invoke();
                }
                return;
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer(targetLayer))//if hit target deal damage
        {
            if(hitOpponents.Contains(other.transform.root))
            {
                return;
            }

            other.transform.root.GetComponent<Character>().TakeDamage(dmg);
            hitOpponents.Add(other.transform.root);
            //Debug.Log("Opponent hit!");
        }
    }

    public void ClearHitOpponents()
    {
        blocked = false;
        hitOpponents.Clear();
    }
}
