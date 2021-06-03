using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    // ===== Public Variables =====================================================================

    [Header("World Info")]

    public int seed;
    public int xSize;
    public int ySize;

    [Space(10)]
    [Header("Save Info")]

    public bool saveWorld;
    public bool loadWorld;
    public string worldName;

    [Space(10)]
    [Header("Block and Layer Info")]

    public int[,] map;
    public GameObject[] blocks;
    public LayerGenerator[] layers;

    // ===== Private Variables ====================================================================

    private SaveManager save;

    // ===== Awake ================================================================================

    private void Awake ()
    {
        map = new int[xSize, ySize];
        save = GetComponent<SaveManager>();

        seed = PlayerPrefs.GetInt("Seed", 0);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        if (seed == 0)
            seed = System.Guid.NewGuid().GetHashCode();

        Cursor.visible = true;
        Random.InitState(seed);

        bool gen = !loadWorld || !(save.Load(worldName, ref map));

        if (gen)
            InitWorld();
           
        BuildWorld();

        if (saveWorld)
            save.Save(worldName, ref map);
    }

    // ===== Private Funcitons ====================================================================

    private void InitWorld ()
    {
        foreach (LayerGenerator layer in layers)
            layer.InitLayer(ref map);

        foreach (LayerGenerator layer in layers)
            layer.Generate(ref map, this);
    }

    private void BuildWorld ()
    {
        for (int x = 0; x < xSize; ++x)
        {
            for (int y = 0; y < ySize; ++y)
            {
                if (map[x, y] == 0)
                    continue;

                Instantiate(blocks[map[x,y]], new Vector3((float)x / 4f, (float)y / 4f), Quaternion.identity, transform.GetChild(transform.childCount - 1));
            }
        }
    }

    // ===== Public Functions =====================================================================

    public int Int (int a, int b)
    {
        return Random.Range(a, b);
    }

    public float Float (float a, float b)
    {
        return Random.Range(a, b);
    }
}