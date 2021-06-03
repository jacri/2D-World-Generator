using UnityEngine;
using System.Collections;

public class Hotbar : MonoBehaviour
{
    // ===== Public Variables =====================================================================

    public int currentSlot;
    public HotbarSlot[] slots;

    // ===== Private Variables ====================================================================

    private Item currentItem;

    // ===== Start ================================================================================

    private void Start ()
    {
        UpdateItem();
        StartCoroutine(HandleScrolling());
    }

    // ===== Update ===============================================================================

    private void Update ()
    {
        if (currentItem != null)
        {
            if (Input.GetMouseButtonDown(0))
                currentItem.StartUse();

            else if (Input.GetMouseButtonDown(1))
                currentItem.StartAltUse();

            if (Input.GetMouseButtonUp(0))
                currentItem.StopUse();

            else if (Input.GetMouseButtonUp(1))
                currentItem.StopAltUse();
        }
    }

    // ===== Public Functions =====================================================================

    public void UpdateItem ()
    {
        currentItem = slots[currentSlot].Activate();
    }

    // ===== Private Funcitons ====================================================================

    private IEnumerator HandleScrolling ()
    {
        while (true)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                slots[currentSlot].Deactivate();
                currentSlot = currentSlot == 9 ? 0 : currentSlot + 1;
                UpdateItem();

                yield return new WaitForSeconds(0.05f);
            }

            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                slots[currentSlot].Deactivate();
                currentSlot = currentSlot == 0 ? 9 : currentSlot - 1;
                UpdateItem();

                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}