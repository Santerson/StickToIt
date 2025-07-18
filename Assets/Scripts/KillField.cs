using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillField : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Vector2 respawnLocation = new Vector2(-7.833f, -0.01f);
    [SerializeField] bool ResetsLevel = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            if (ResetsLevel)
            {
                // Reset the level by reloading the current scene
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                return;
            }
            else
            {
                collision.gameObject.transform.position = respawnLocation;

            }
        }
    }
}
