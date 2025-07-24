using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] AudioSource doorLockedSound; // Sound to play when the door is locked
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player != null && player.hasKey)
            {
                Debug.Log("Unlocked the door!");
                doorLockedSound.Play();
                Destroy(gameObject); // Reveals the real exit
            }
            else
            {
                Debug.Log("The door is locked.");
            }
        }
    }
}
