using UnityEngine;

public class Block : MonoBehaviour
{
    // ===== Public Variables =====================================================================

    public float strength;
    public GameObject item;

    // ===== Public Functions =====================================================================

    public virtual void Mine (float amnt)
    {
        strength -= amnt;

        if (strength <= 0)
            Destroy();
    }

    // ===== Protected Functions ==================================================================

    protected virtual void Destroy()
    {
        Instantiate(item, transform.position, Quaternion.identity);
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 0.25f);
    }
}