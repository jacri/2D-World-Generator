using UnityEngine;
using System.Collections;

public class Pickaxe : Item
{
    // ===== Public Variables =====================================================================

    public float power;
    public float range;

    // ===== Private Variables ====================================================================

    private Ray ray;
    private Camera cam;
    private Transform player;
    private RaycastHit hit;

    // ===== Start ================================================================================

    private void Start ()
    {
        cam = Camera.main;
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // ===== Public Functions =====================================================================

    public override void StartUse ()
    {
        StartCoroutine(Mine());
    }

    public override void StopUse ()
    {
        StopAllCoroutines();
    }

    // ===== Protected Funcitons ==================================================================

    protected IEnumerator Mine ()
    {
        while (true)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, ~layerMask) && hit.collider.GetComponent<Block>() && Vector3.Distance(player.position, hit.point) <= range)
                hit.collider.GetComponent<Block>().Mine(power);
               
            yield return new WaitForEndOfFrame();
        }
    }
}