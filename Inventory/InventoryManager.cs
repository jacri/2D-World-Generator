using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    // ===== Public Variables =====================================================================

    public GameObject UI;
    public Item selectedItem;
    public InventorySlot[] slots;

    // ===== Private Variables ====================================================================

    private Ray ray;
    private RaycastHit hit;

    private Camera cam;

    // ===== Start ================================================================================

    private void Start ()
    {
        cam = Camera.main;
    }

    // ===== Update ===============================================================================

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            UI.SetActive(!UI.activeSelf);

        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (UI.activeSelf)
        {
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<InventorySlot>())
                {
                    InventorySlot slot = hit.collider.GetComponent<InventorySlot>();

                    // No item is currently held - pickup new item
                    if (selectedItem == null)
                        selectedItem = slot.UpdateItem();

                    // Replace current item with new one
                    else
                        selectedItem = slot.UpdateItem(selectedItem);

                    // Make sure new item is moved
                    if (selectedItem != null)
                        StartCoroutine(MoveItem());

                    // No item is held
                    else
                    {
                        StopAllCoroutines();
                        GetComponentInChildren<Hotbar>().UpdateItem();
                    }
                }

                else if (hit.collider.GetComponent<PauseMenu>())
                    hit.collider.GetComponent<PauseMenu>().Quit();
            }
        }

        if (Input.GetMouseButtonDown(0) && selectedItem != null && Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.GetComponent<InventorySlot>())
                selectedItem.GetComponent<Item>().StartUse();
        }

        if (Input.GetMouseButtonUp(0) && selectedItem != null && Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.GetComponent<InventorySlot>())
                selectedItem.GetComponent<Item>().StopUse();
        }
    }

    // ===== Private Funcitons ====================================================================

    private IEnumerator MoveItem ()
    {
        RaycastHit posHit;

        while (selectedItem != null)
        {
            if (Physics.Raycast(ray, out posHit))
                selectedItem.transform.position = new Vector3(posHit.point.x, posHit.point.y, 0);

            yield return new WaitForEndOfFrame();
        }
    }

    public void AddItem (GameObject itemObj)
    {
        Item item = itemObj.GetComponent<Item>();
        int newSlot = Contains(item);

        if (newSlot == -1)
        {
            newSlot = NextOpenSlot();

            if (newSlot == -1)
                return;

            slots[newSlot].AddItem(itemObj);
        }

        else
            slots[newSlot].item.quantity++;
    } 

    // ===== Helper Functions =====================================================================

    private int Contains (Item item)
    {
        for (int i = 0; i < 40; ++i)
        {
            if (slots[i].item == null)
                continue;

            else if (slots[i].item.EqualsItem(item))
                return i;
        }

        return -1;
    }

    private int NextOpenSlot ()
    {
        for (int i = 0; i < 40; i++)
        {
            if (slots[i].item == null)
                return i;
        }

        return -1;
    }
}