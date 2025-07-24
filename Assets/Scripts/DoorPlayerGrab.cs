using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayerGrab : MonoBehaviour
{
    [SerializeField] AudioSource keySound;
    [SerializeField] GameObject keyPS;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set player's hasKey to true
            other.GetComponent<DoorPlayer>().hasKey = true;

            Instantiate(keyPS, transform.position, Quaternion.identity);

            if (keySound != null)
            {
                keySound.Play();
            }

            // Destroy the key object
            Destroy(gameObject);
        }
    }
}
