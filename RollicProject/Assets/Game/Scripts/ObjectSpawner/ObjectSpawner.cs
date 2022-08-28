using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 firsObjectPos;
    [SerializeField] private Quaternion objectsRotation;
    [SerializeField] private float objectCount;
    [SerializeField] private CollectObject spawnObject;
    void Start()
    {
        SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < objectCount; i++)
        {
            var newObject=Instantiate(spawnObject, Vector3.zero, objectsRotation,transform);
            newObject.transform.localPosition = new Vector3(firsObjectPos.x + i, firsObjectPos.y, firsObjectPos.z);
        }
    }
}
