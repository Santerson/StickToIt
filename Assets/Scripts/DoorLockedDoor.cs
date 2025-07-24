using UnityEngine;

public class DoorLockedDoor : MonoBehaviour
{
    [SerializeField] private AudioSource doorLockedSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorPlayer player = other.GetComponent <DoorPlayer>();
            if (player != null && player.hasKey)
            {
                Debug.Log("Unlocked the door!");
                if (doorLockedSound != null)
                {
                    doorLockedSound.Play();
                }
                Destroy(gameObject); // Reveals the real exit
            }
            else
            {
                Debug.Log("The door is locked.");
            }
        }
    }
}
