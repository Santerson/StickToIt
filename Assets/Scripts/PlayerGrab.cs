using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] AudioSource keySound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set player's hasKey to true
            other.GetComponent<PlayerScript>().hasKey = true;

            if (keySound != null)
            {
                keySound.Play();

            }

            // Destroy the key object
            Destroy(gameObject);
        }
    }
}
