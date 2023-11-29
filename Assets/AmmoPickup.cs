using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (other.transform.tag == "Player")
        {
            player.AddAmmo(player.maxAmmoSize);
            Destroy(gameObject);
            
        }
    }
}
