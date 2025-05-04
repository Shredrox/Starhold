using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject towerPrefab;
    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
#endif
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Platform"))
                {
                    Vector3 placementPosition = hit.point + Vector3.up * 0.005f;

                    GameObject tower = Instantiate(towerPrefab, placementPosition, Quaternion.identity);

                    tower.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                }
            }
        }
    }
}
