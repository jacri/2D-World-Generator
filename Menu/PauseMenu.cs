using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // ===== Public Functions =====================================================================

    public void Quit ()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}