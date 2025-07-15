using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransferScript : MonoBehaviour
{
    [SerializeField] int Level = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LevelLoad()
    {
        Level++;
        SceneManager.LoadScene("lvl" + Level);
    }
}
