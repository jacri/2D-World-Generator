using UnityEngine;
using dir = CameraBoundry.BoundryDirection;

public class CameraFollow : MonoBehaviour
{
    // ===== Private Variables ====================================================================

    private bool up = true;
    private bool down = true;
    private bool left = true;
    private bool right = true;

    private float z;
    private Vector3 pos;
    private Transform t;
    private Transform player;

    // ===== Start ================================================================================

    private void Start ()
    {
        t = transform;
        player = FindObjectOfType<PlayerMovement>().transform;

        if (player == null)
            enabled = false;

        z = t.position.z;
    }

    // ===== Update ===============================================================================

    private void Update ()
    {
        pos = player.position;
        pos.z = z;

        if ((!left && pos.x < t.position.x) || (!right && pos.x > t.position.x))
            pos.x = t.position.x;

        if ((!down && pos.y < t.position.y) || (!up && pos.y > t.position.y))
            pos.y = t.position.y;

        t.position = pos;
    }

    // ===== Private Funcitons ====================================================================

    private void OnTriggerEnter (Collider other)
    {
        other.GetComponent<CameraBoundry>()?.Enter(ref up, ref down, ref left, ref right);
    }

    private void OnTriggerExit (Collider other)
    {
        other.GetComponent<CameraBoundry>()?.Leave(ref up, ref down, ref left, ref right);
    }
}