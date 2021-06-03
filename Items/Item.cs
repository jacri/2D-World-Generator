using UnityEngine;

public class Item : MonoBehaviour
{
    // ===== Public Variables =====================================================================

    public string itemName;
    public int quantity;

    // ===== Private Variables ====================================================================

    protected int status = 0;
    protected int layerMask = (1 << 6) | (1 << 7);

    // ===== Abstract Functions ===================================================================

    public virtual void StartUse ()
    {

    }

    public virtual void StopUse ()
    {

    }

    public virtual void StartAltUse ()
    {

    }

    public virtual void StopAltUse ()
    {

    }

    // ===== Public Functions =====================================================================

    public bool EqualsItem (Item other)
    {
        return itemName == other.itemName;
    }
}