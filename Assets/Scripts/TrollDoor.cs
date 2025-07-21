using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollDoor : MonoBehaviour
{
    [SerializeField] GameObject doorPrefab;

    void Start()
    {
        Instantiate(doorPrefab, new Vector3(10f,10f,10f) , Quaternion.identity);
    }
}
