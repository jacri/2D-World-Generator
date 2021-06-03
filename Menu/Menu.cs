using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // ===== Public Variables =====================================================================

    public UnityEngine.UI.Text resumeText;
    public UnityEngine.UI.InputField inputField;

    // ===== Public Functions =====================================================================

    public void NewWorld ()
    {
        try
        {
            int seed = System.Int32.Parse(inputField.text);
            PlayerPrefs.SetInt("Seed", seed);
            PlayerPrefs.Save();
        }
        
        catch (System.Exception)
        {
            
        }

        SceneManager.LoadScene("NewWorld");
    }

    public void ResumeWorld (string name)
    {
        string fileName = string.Format("{0}/Worlds/{1}.world", Application.dataPath, name);

        if (File.Exists(fileName))
            SceneManager.LoadScene("ResumeWorld");

        else
        {
            resumeText.text = "Error - no world is saved.";
            resumeText.color = Color.red;
        }
    }
}