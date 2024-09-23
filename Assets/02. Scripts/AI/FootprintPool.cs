using System.Collections.Generic;
using UnityEngine;

public class FootprintPool : MonoBehaviour
{
    public static FootprintPool Instance;
    public GameObject footprintPrefab;
    public int poolSize = 20;

    private List<GameObject> footprints;

    private void Awake()
    {
        Instance = this;
        footprints = new List<GameObject>();
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(footprintPrefab);
            obj.SetActive(false);
            footprints.Add(obj);
        }
    }

    public GameObject GetFootprint()
    {
        foreach (GameObject footprint in footprints)
        {
            if (!footprint.activeInHierarchy)
            {
                return footprint;
            }
        }

        GameObject newFootprint = Instantiate(footprintPrefab);
        newFootprint.SetActive(false);
        footprints.Add(newFootprint);
        return newFootprint;
    }
}