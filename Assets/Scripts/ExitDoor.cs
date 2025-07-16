using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObject pressEPrompt; // drag your Canvas/Text object here

    private bool playerInRange = false;

    void Start()
    {
        if (pressEPrompt != null)
            pressEPrompt.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Entering next scene...");
            FindObjectOfType<SceneChanger>().nextLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pressEPrompt != null)
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