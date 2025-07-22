using UnityEngine;

public class TrollKey : MonoBehaviour
{
    [SerializeField] private GameObject exitInScene;         // Reference to current Exit object in the scene
    [SerializeField] private GameObject exitWithDoorPrefab;  // Reference to the Exit With Door prefab
    private static bool exitReplaced = false;                // Static to track if the exit was already replaced

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!exitReplaced && exitInScene != null && exitWithDoorPrefab != null)
            {
                // Replace the exit
                Vector3 position = exitInScene.transform.position;
                Quaternion rotation = exitInScene.transform.rotation;

                Destroy(exitInScene);
                Instantiate(exitWithDoorPrefab, position, rotation);

                exitReplaced = true; // Prevent further replacement
            }

            // Always destroy this key when touched
            Destroy(gameObject);
        }
    }
}
