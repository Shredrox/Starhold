using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    //public GameObject terrainPrefab;
    //public GameObject towerPrefab;
    //public GameObject platformPrefab;
    public GameObject gameFieldPrefab;

    private static GameObject spawnedField;
    public static GameObject SpawnedField => spawnedField;
    public static bool IsFieldPlaced { get; set; }

    public ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new();
    public Transform[] platformPositions;

    void Start()
    {
        raycastManager = Object.FindFirstObjectByType<ARRaycastManager>();

        //foreach (var pos in platformPositions)
        //{
        //    Instantiate(platformPrefab, pos.position, pos.rotation);
        //}
    }

    void Update()
    {
        if (IsFieldPlaced) return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && !IsFieldPlaced)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                PlaceGameField(hit.point, Quaternion.identity);
            }
        }

        //foreach (var platformPosition in platformPositions)
        //{
        //    if (Vector3.Distance(hitPose.position, platformPosition.position) < 0.1f)
        //    {
        //        if (towerPrefab != null)
        //        {
        //            Instantiate(towerPrefab, hitPose.position, Quaternion.identity);
        //        }
        //    }
        //}
#else
        if (Input.touchCount == 0)
        {
            return;
        }
        if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            PlaceGameField(hitPose.position, hitPose.rotation);
        }
#endif
    }

    void PlaceGameField(Vector3 position, Quaternion rotation)
    {
        spawnedField = Instantiate(gameFieldPrefab, position, rotation);
        IsFieldPlaced = true;
    }
}
