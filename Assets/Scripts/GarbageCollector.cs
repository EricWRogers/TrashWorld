using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    [SerializeField] private List<GameObject> garbageObjects = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        garbageObjects.AddRange(GameObject.FindGameObjectsWithTag("Garbage"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
