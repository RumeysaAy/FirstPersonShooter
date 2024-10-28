using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType; // Bullets, Shells, Rockets

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindAnyObjectByType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}
