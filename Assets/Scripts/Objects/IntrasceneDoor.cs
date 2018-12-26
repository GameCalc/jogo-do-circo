using UnityEngine;

public class IntrasceneDoor : Interactable
{
    public enum Direction { North, East, South, West };

    public Direction wall;
    public bool locked = false;
    public Transform destinyGrid;

    Vector2 playerDisplacement;

    void Start()
    {
        switch (wall)
        {
            case Direction.North:
                playerDisplacement = new Vector2(transform.position.x, transform.position.y + 4);
                break;

            case Direction.East:
                playerDisplacement = new Vector2(transform.position.x + 4, transform.position.y);
                break;

            case Direction.South:
                playerDisplacement = new Vector2(transform.position.x, transform.position.y - 4);
                break;

            case Direction.West:
                playerDisplacement = new Vector2(transform.position.x - 4, transform.position.y);
                break;
        }
    }

    protected override void OnInteract()
    {
        if (!locked)
        {
            Player.instance.transform.position = playerDisplacement;
            IntrasceneCamera.ChangeGrid(destinyGrid.position);
        }
    }
}
