using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Intro : MonoBehaviour
{
    [SerializeField] int start = 0;
    [SerializeField] float times = 5;
    [SerializeField] float timeBetweenPrints = 0.5f;
    [SerializeField] GameObject prefab;
    [SerializeField] bool rotate = false;
    [SerializeField] Vector3 speedRotation;
    List<GameObject> instantiatedObjects = new List<GameObject>();
    Coroutine coroutine = null;

    void Start()
    {
        StartCoroutine(MainLoop());
        StartCoroutine(Rotation());
    }
    IEnumerator MainLoop()
    {
        while (true)
        {
            coroutine = StartCoroutine(InstanciateValuesCoroutine());
            while (coroutine != null)
                yield return null;
            coroutine = StartCoroutine(DestroyObjects());
            while (coroutine != null)
                yield return null;
        }
    }

    IEnumerator InstanciateValuesCoroutine()
    {
        for (int k = start; k < start + times; k++)
        {
            for (int j = start; j < start + times; j++)
            {
                for (int i = start; i < start + times; i++)
                {
                    if ((j == start && i == start) || 
                        (j == start && k == start) || 
                        (k == start && i == start) || 
                        (j == start + times - 1 && i == start + times - 1) || 
                        (j == start + times - 1 && k == start + times - 1) || 
                        (k == start + times - 1 && i == start + times - 1) ||
                        (j == start && i == start + times - 1) ||
                        (j == start && k == start + times - 1) ||
                        (k == start && i == start + times - 1) ||
                        (i == start && j == start + times - 1) ||
                        (k == start && j == start + times - 1) ||
                        (i == start && k == start + times - 1))
                    {
                        Vector3 position = new Vector3(i, j, k);
                        InstantiateAt(position);
                        yield return new WaitForSeconds(timeBetweenPrints);
                    }
                }
            }
        }
        coroutine = null;
    }

    IEnumerator Rotation()
    {
        while (true)
        {
            if (rotate == true)
            {
                transform.Rotate(speedRotation * Time.deltaTime);
            }
            yield return null;
        }
    }

    IEnumerator DestroyObjects()
    {
        while (instantiatedObjects.Count > 0)
        {
            Destroy(instantiatedObjects[0]);
            instantiatedObjects.RemoveAt(0);
            yield return new WaitForSeconds(timeBetweenPrints);
        }
        coroutine = null;
    }

    public void InstantiateAt(Vector3 location)
    {
        GameObject instantiated = Instantiate(prefab, transform);

        instantiated.transform.localPosition = location;

        instantiated.name = prefab.name + " " + instantiatedObjects.Count;

        instantiatedObjects.Add(instantiated);
        
        print("Placing Sphere " + instantiated.name + " at " + location);
        
        instantiated.GetComponent<MeshRenderer>().material.color = new Color(
            Normalize(location.x, -5, 5),
            Normalize(location.y, -5, 5),
            Normalize(location.z, -5, 5)
        );
    }
    float Normalize(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }
}