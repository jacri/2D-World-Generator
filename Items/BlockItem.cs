using UnityEngine;
using System.Collections;

public class BlockItem : Item
{
    // ===== Public Variables =====================================================================

    public float time;
    public GameObject block;
    public UnityEngine.UI.Text text;

    // ===== Private Variables ====================================================================

    private Ray ray;
    private Camera cam;
    private RaycastHit hit;

    // ===== Start ================================================================================

    private void Start ()
    {
        cam = Camera.main;
    }

    // ===== Update ===============================================================================

    private void Update() 
    {
        UpdateText();
    }

    // ===== Public Functions =====================================================================

    public override void StartUse ()
    {
        StartCoroutine(PlaceBlock());
    }

    public override void StopUse ()
    {
        StopAllCoroutines();
    }

    public void UpdateText ()
    {
        text.text = quantity.ToString();
    }

    // ===== Private Funcitons ====================================================================

    private IEnumerator PlaceBlock ()
    {
        while (true)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, 1 << 6) && !Physics.Raycast(ray, 100f, ~layerMask))
            {
                Vector3 pos = new Vector3(Mathf.Floor(hit.point.x / 0.25f + 0.5f) * 0.25f, Mathf.Floor(hit.point.y / 0.25f + 0.5f) * 0.25f, 0);
                Instantiate(block, pos, Quaternion.identity);
            }

            if (quantity <= 0)
            {
                Destroy(gameObject);
            }

            else
            {
                quantity--;
                //UpdateText();
            }
                

            yield return new WaitForSeconds(time);
        }
    }
}