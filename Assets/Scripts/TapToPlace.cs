using Unity.XR.CoreUtils;
using UnityEngine;

public class TapToPlace : MonoBehaviour
{
    private XROrigin xrOrigin;
    private Pose placementPose;

    void Start()
    {
        xrOrigin = Object.FindFirstObjectByType<XROrigin>();
    }

    void Update()
    {
        UpdatePlacementPose();
    }

    private void UpdatePlacementPose()
    {
    }
}
