using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // ===== Public Variables =====================================================================

    public float speed;
    public float jumpForce;
    public float maxJumpButtonHold;

    // ===== Private Variables ====================================================================

    private Transform t;
    private Rigidbody rb;

    private bool canJump = true;

    // ===== Start ================================================================================

    private void Start ()
    {
        t = transform;
        rb = GetComponent<Rigidbody>();
    }

        // ===== Update ===============================================================================

    private void Update ()
    {
        if (Input.GetKey(KeyCode.A))
            t.position += Vector3.left * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            t.position += Vector3.right * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
            StartCoroutine(Jump());
    }

    // ===== Private Funcitons ====================================================================

    private IEnumerator Jump ()
    {
        canJump = false;
        float timer = 0f;

        while (Input.GetKey(KeyCode.Space) && timer < maxJumpButtonHold)
        {
            timer += Time.deltaTime;
            rb.AddForce(new Vector3(0f, jumpForce * (maxJumpButtonHold - timer) / maxJumpButtonHold, 0f));

            yield return new WaitForFixedUpdate();
        }

        for (int frame = 0; frame < 15; frame++)
        {
            rb.AddForce(new Vector3(0f, jumpForce * (frame / 15), 0f));
            yield return new WaitForEndOfFrame();
        }

        StopAllCoroutines();
    }

    private void OnCollisionEnter (Collision collision)
    {
        Vector3 point = collision.contacts[0].point;

        if (point.y < t.position.y && point.x > t.position.x - 0.5f && point.x < t.position.x + 0.5f)
            canJump = true;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (point.y > t.position.y - 0.4f && point.y < t.position.y)
                t.position += new Vector3(0f, 0.25f, 0f);
        }
    }
}