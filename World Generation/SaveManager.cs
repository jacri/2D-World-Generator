using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // ===== Private Variables ====================================================================

    private WorldGenerator worldGen;

    // ===== Public Functions =====================================================================

    public void Save (string name, ref int[,] map)
    {
        worldGen = GetComponent<WorldGenerator>();
        string fileName = string.Format("{0}/Worlds/{1}.world", Application.dataPath, name);

        string dir = string.Format("{0}/Worlds", Application.dataPath);

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        
        using (FileStream fs = File.OpenWrite(fileName))
        {
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(worldGen.seed.ToString());

            for (int y = 0; y < worldGen.ySize; y++)
            {
                for (int x = 0; x < worldGen.xSize; x++)
                {
                    sw.Write(map[x, y].ToString());
                }

                sw.WriteLine();
            }

            sw.Close();
            fs.Close();
        }
    }

    public bool Load (string name, ref int[,] map)
    {
        worldGen = GetComponent<WorldGenerator>();
        string fileName = string.Format("{0}/Worlds/{1}.world", Application.dataPath, name);

        if (!File.Exists(fileName))
            return false;

        using (FileStream fs = File.OpenRead(fileName))
        {
            StreamReader sr = new StreamReader(fs);
            worldGen.seed = int.Parse(sr.ReadLine());
            sr.ReadLine();

            for (int y = 0; y < worldGen.ySize; y++)
            {
                char[] row = sr.ReadLine().ToCharArray();

                for (int x = 0; x < row.Length; x++)
                    map[x, y] = row[x] - '0';
            }

            sr.Close();
            fs.Close();
        }

        return true;
    }
}