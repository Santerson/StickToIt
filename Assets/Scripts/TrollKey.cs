using UnityEngine;

public class TrollKey : MonoBehaviour
{
    [SerializeField] private GameObject exitInScene;         // Reference to current Exit object in the scene
    [SerializeField] private GameObject exitWithDoorPrefab;  // Reference to the Exit With Door prefab
    [SerializeField] AudioSource keySfx;
    [SerializeField] GameObject keyPs;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the current exit still exists in the scene before replacing it
            if (exitInScene != null && exitWithDoorPrefab != null)
            {
                Vector3 position = exitInScene.transform.position;
                Quaternion rotation = exitInScene.transform.rotation;

                keySfx.Play();
                Instantiate(keyPs, transform.position, Quaternion.identity);

                Destroy(exitInScene);
                Instantiate(exitWithDoorPrefab, position, rotation);
            }

            // Always destroy the key that was touched
            Destroy(gameObject);
        }
    }
}

