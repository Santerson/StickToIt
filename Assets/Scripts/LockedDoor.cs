using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player != null && player.hasKey)
            {
                Debug.Log("Unlocked the door!");
                Destroy(gameObject); // Reveals the real exit
            }
            else
            {
                Debug.Log("The door is locked.");
            }
        }
    }
}
