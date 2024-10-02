using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform plateVisual;
    private List<GameObject> spawnedPlates;
    private float plateOffsetY = .1f;

    void Awake()
    {
        spawnedPlates = new List<GameObject>();
    }
    void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateTransform = Instantiate(plateVisual, platesCounter.GetKitchenObjectFollowTransform());
        plateTransform.localPosition = new Vector3(0, spawnedPlates.Count * plateOffsetY, 0);
        spawnedPlates.Add(plateTransform.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject toBeRemovedGameObject = spawnedPlates[spawnedPlates.Count - 1];
        Destroy(toBeRemovedGameObject);
        spawnedPlates.RemoveAt(spawnedPlates.Count - 1);        
    }
}
