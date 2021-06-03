using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
    // ===== Public Variables =====================================================================

    public GameObject item;
    public int pickupRange;
    public float speed;

    // ===== Private Variables ====================================================================

    private Transform t;
    private Collider col;
    private Transform player;
    private InventoryManager inv;
    private bool canPickup = false;

    // ===== Start ================================================================================

    private void Start ()
    {
        t = transform;
        col = GetComponent<Collider>();
        inv = FindObjectOfType<InventoryManager>();
        player = inv.transform;
    }

    // ===== Private Funcitons ====================================================================

    private void OnTriggerEnter (Collider other)
    {
        if (other.transform == player)
        {
            col.enabled = false;
            StartCoroutine(MoveTowardsPlayer());
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            col.enabled = false;
            StopAllCoroutines();
        }       
    }

    private IEnumerator AllowPickup ()
    {
        yield return new WaitForSeconds(1f);
        canPickup = true;
        StopCoroutine(AllowPickup());
    }

    private IEnumerator MoveTowardsPlayer ()
    {
        if (!canPickup)
            yield return new WaitForSeconds(1f);

        while (true)
        {
            if (Vector3.Distance(t.position, player.position) < pickupRange)
            {
                inv.AddItem(Instantiate(item));
                Destroy(gameObject);
            }

            t.position += (player.position - t.position) * speed;
            yield return new WaitForEndOfFrame();
        }
    }
}