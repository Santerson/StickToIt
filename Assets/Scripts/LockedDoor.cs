using UnityEngine;

public class Door : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player != null && player.hasKey)
            {
                Debug.Log("Door opens!");
                Destroy(gameObject); // or trigger an animation
            }
            else
            {
                Debug.Log("The door is locked.");
            }
        }
    }
}

