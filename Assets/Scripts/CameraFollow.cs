using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 10f;
    public Vector2 movementOffset;
    public bool freezeX, freezeY;
    public Vector2 movementLimit;
    public bool limitX, limitY;
    float minX, maxX, minY, maxY;

    void Start () {
        if (movementLimit.x < 0)
            movementLimit.x *= -1;

        if (movementLimit.y < 0)
            movementLimit.y *= -1;

        minX = -1 * movementLimit.x;
        maxX = movementLimit.x;
        minY = -1 * movementLimit.y;
        maxY = movementLimit.y;
    }

    void LateUpdate () {
        Vector3 desiredPosition = transform.position;

        if (!freezeX && Mathf.Abs(transform.position.x - target.position.x) > movementOffset.x)
            desiredPosition.x = target.position.x;

        if (!freezeY && Mathf.Abs(transform.position.y - target.position.y) > movementOffset.y)
            desiredPosition.y = target.position.y;

        if (limitX)
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

        if (limitY)
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }    
}
