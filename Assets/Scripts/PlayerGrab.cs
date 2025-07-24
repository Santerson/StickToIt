using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] AudioSource keySound;
    [SerializeField] GameObject keyParticleSystem; // Particle system to play when the key is picked up
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
            Instantiate(keyParticleSystem, transform.position, Quaternion.identity);

            // Destroy the key object
            Destroy(gameObject);
        }
    }
}
