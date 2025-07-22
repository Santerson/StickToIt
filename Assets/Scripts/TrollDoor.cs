using UnityEngine;

public class TrollKey : MonoBehaviour
{
    [SerializeField] private GameObject exitDoor;       // Drag your "Exit with Door" GameObject here
    [SerializeField] private Transform exitSpawnPoint;  // Place this where you want the door to appear

    private bool doorMoved = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!doorMoved && other.CompareTag("Player"))
        {
            // Troll move: door appears to block them
            exitDoor.transform.position = exitSpawnPoint.position;

            // Optional: if the door had a collider disabled, enable it here
            // exitDoor.GetComponent<Collider2D>().enabled = true;

            doorMoved = true;
            Destroy(gameObject); // Remove the key from the scene
        }
    }
}
