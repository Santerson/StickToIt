using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayerGrab : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set player's hasKey to true
            other.GetComponent<DoorPlayer>().hasKey = true;

            // Destroy the key object
            Destroy(gameObject);
        }
    }
}
