using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject pressEPrompt;
    [SerializeField] private GameObject lockedDoor; // Drag your LockedDoor object here

    private bool playerInRange = false;
    private SceneChanger sceneChanger;

    void Start()
    {
        if (pressEPrompt != null)
            pressEPrompt.SetActive(false);

        // Cache reference to SceneChanger
        sceneChanger = FindObjectOfType<SceneChanger>();
        if (sceneChanger == null)
            Debug.LogError("SceneChanger not found in scene.");
    }

    void Update()
    {
        if (playerInRange && lockedDoor == null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Unlocked. Going to next level...");
            sceneChanger.nextLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (lockedDoor == null && pressEPrompt != null)
                pressEPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (pressEPrompt != null)
                pressEPrompt.SetActive(false);
        }
    }
}
