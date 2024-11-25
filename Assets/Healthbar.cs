using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Character character;
    Slider healthbar;

    public bool isPlayer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = transform.root.GetComponent<PlayerCharacter>();
        healthbar = GetComponentInChildren<Slider>();


        character.TakeDamageEvent += UpdateHealthbar;
        character.OnDeath += Death;


        healthbar.value = character.health / 100f;
    }

    private void UpdateHealthbar()
    {
        healthbar.value = character.health / 100f;
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(!isPlayer)
        transform.LookAt(Camera.main.transform);
    }
}
