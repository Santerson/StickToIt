using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillField : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Vector2 respawnLocation = new Vector2(-7.833f, -0.01f);
    [SerializeField] bool ResetsLevel = true;
    [SerializeField] GameObject shader;
    [SerializeField] GameObject deathPS;
    [SerializeField] AudioSource deathsfx;
    [SerializeField] AudioSource deathsfx2;
    PlayerScript[] objs;
    Vector2 pos = Vector2.zero;
    //GameObject reference = null;
    //GameObject referenceCamera = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            if (ResetsLevel)
            {
                // Reset the level by reloading the current scene
                try
                {
                    objs = FindObjectsOfType<PlayerScript>();
                    for (int i = 0; i < objs.Length; i++)
                    {
                        objs[i].haltPlayer = true;
                    }
                    FindObjectOfType<PlayerScript>().haltPlayer = true;
                }
                catch
                {
                    try
                    {
                        FindObjectOfType<DoorPlayer>().haltPlayer = true;
                        pos = FindObjectOfType<DoorPlayer>().transform.position;
                    }
                    catch
                    {
                        Debug.LogError("AN error occured while trying to halt the player");
                    }
                }
                //reference =Instantiate(shader, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                deathsfx.Play();
                StartCoroutine(deathAnim());
                return;
            }
            else
            {
                collision.gameObject.transform.position = respawnLocation;

            }
        }
    }

    IEnumerator deathAnim()
    {
        // Add any death animation logic here if needed
        yield return new WaitForSeconds(1f); // Wait for 1 second before respawning
        // Optionally, you can reset the player's state or perform other actions here
        try
        {
            if (objs.Length == 0)
            {
                throw new Exception("No PlayerScript instances found.");
            }
            for (int i = 0; i < objs.Length; i++)
            {
                Instantiate(deathPS, new Vector2(objs[i].transform.position.x, objs[i].transform.position.y), Quaternion.identity);
                Destroy(objs[i].gameObject);
            }
        }
        catch
        {
            try
            {
                Destroy(FindObjectOfType<DoorPlayer>().gameObject);
                Instantiate(deathPS, new Vector2(pos.x, pos.y), Quaternion.identity);
            }
            catch
            {
                Debug.LogError("AN error occured while trying to destroy the player");
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                yield break;
            }
        }
        //Destroy(reference);
        deathsfx2.Play();
        StartCoroutine(deathAnim2());
    }

    IEnumerator deathAnim2()
    {
        yield return new WaitForSeconds(2f); // Wait for 1 second before respawning
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }
}
