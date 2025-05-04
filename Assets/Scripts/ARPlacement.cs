using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlacement : MonoBehaviour
{
    public GameObject gameFieldPrefab;
    public ARPlaneManager planeManager;
    private static GameObject spawnedField;
    public static GameObject SpawnedField => spawnedField;
    public static bool IsFieldPlaced { get; set; }

    public ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new();
    public Transform[] platformPositions;

    void Start()
    {
        raycastManager = Object.FindFirstObjectByType<ARRaycastManager>();
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
        foreach (var plane in planeManager.trackables)
        {
            if (plane.TryGetComponent<ARPlaneMeshVisualizer>(out var visualizer))
            {
                visualizer.enabled = false;
            }
        }
    }
}
