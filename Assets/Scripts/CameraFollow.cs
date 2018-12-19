using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 2;
    public Vector2 movementOffset = new Vector2(5, 5);
    public bool freezeX, freezeY;
    public Vector2 movementLimit;
    public bool limitX, limitY;
    float minX, maxX, minY, maxY;
    Vector2 desiredPosition;

    void Awake () {
        SetMovementCenter(Vector2.zero);
    }

    void LateUpdate () {
        UpdateDesiredPosition();

        float step = smoothSpeed * Time.deltaTime;
        Vector2 v = Vector2.MoveTowards(transform.position, desiredPosition, step);
        transform.position = new Vector3(v.x, v.y, -1f); 
    }
    
    public void SetMovementCenter (Vector2 center)
    {
        if (movementLimit.x < 0)
            movementLimit.x *= -1;

        if (movementLimit.y < 0)
            movementLimit.y *= -1;

        minX = center.x - movementLimit.x;
        maxX = center.x + movementLimit.x;
        minY = center.y - movementLimit.y;
        maxY = center.y + movementLimit.y;
    }

    void UpdateDesiredPosition ()
    {
        desiredPosition = transform.position;

        if (!freezeX && Mathf.Abs(transform.position.x - target.position.x) > movementOffset.x)
            desiredPosition.x = target.position.x;

        if (!freezeY && Mathf.Abs(transform.position.y - target.position.y) > movementOffset.y)
            desiredPosition.y = target.position.y;

        if (limitX)
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

        if (limitY)
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
    }

    public void FlashPositionUpdate ()
    {
        UpdateDesiredPosition();
        transform.position = desiredPosition;
    }
}
