using UnityEngine;

public class CameraBoundry : MonoBehaviour 
{
    // ===== Enums ================================================================================

    public enum BoundryDirection
    {
        Up,
        Down,
        Left,
        Right,
    };

    // ===== Public Variables =====================================================================

    public BoundryDirection direction;

    // ===== Public Functions =====================================================================

    public void Enter (ref bool up, ref bool down, ref bool left, ref bool right)
    {
        switch (direction)
        {
            case BoundryDirection.Up:
                up = false;
                break;

            case BoundryDirection.Down:
                down = false;
                break;

            case BoundryDirection.Left:
                left = false;
                break;

            case BoundryDirection.Right:
                right = false;
                break;
        }
    }

    public void Leave (ref bool up, ref bool down, ref bool left, ref bool right)
    {
        switch (direction)
        {
            case BoundryDirection.Up:
                up = true;
                break;

            case BoundryDirection.Down:
                down = true;
                break;

            case BoundryDirection.Left:
                left = true;
                break;

            case BoundryDirection.Right:
                right = true;
                break;
        }
    }
}