using UnityEngine;

[RequireComponent(typeof(CameraFollow))]
public class IntrasceneCamera : MonoBehaviour {
    public Transform defaultGrid;
    static CameraFollow cameraFollow;

	// Use this for initialization
	void Start () {
        cameraFollow = transform.GetComponent<CameraFollow>();
        ChangeGrid(defaultGrid.position);
	}

    public static void ChangeGrid (Vector3 destinyGrid)
    {
        cameraFollow.SetMovementCenter(destinyGrid);
        cameraFollow.FlashPositionUpdate();
    }
}
