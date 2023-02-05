using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;

    [Header("Spawn Plates Settings"), Space()]
    [SerializeField] private Transform plateVisualPrefab;

    [SerializeField] private List<GameObject> plateVisualGameObjects;
    [SerializeField] private float plateOffcetY = 0.1f;

    private void Start()
    {
        if (platesCounter != null)
        {
            platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
            platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        }
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        plateVisualTransform.localPosition = new Vector3(0, plateOffcetY * plateVisualGameObjects.Count, 0);
        plateVisualGameObjects.Add(plateVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        if (plateVisualGameObjects.Count == 0)
        {
            Debug.LogError("Plates Visual Count - Null | " + gameObject.name);
            return;
        }

        GameObject plateGameObject = plateVisualGameObjects[plateVisualGameObjects.Count - 1];
        plateVisualGameObjects.RemoveAt(plateVisualGameObjects.Count - 1);

        if (plateGameObject != null)
            Destroy(plateGameObject);
    }
}