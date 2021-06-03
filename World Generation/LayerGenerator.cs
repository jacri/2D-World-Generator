using UnityEngine;

public abstract class LayerGenerator : MonoBehaviour
{
    // ===== Public Variables =====================================================================
    [Header("Layer Block Details")]
    public int block;

    [Space(10)]
    [Header("Layer Size")]
    public int xSize;
    public int ySize;

    [Space(10)]
    [Header("Layer Height Details")]
    public int yStart;
    public int yEnd;

    [Space(10)]
    [Header("Room Details")]
    public int numRooms;
    public int minRoomSize;
    public int maxRoomSize;

    // ===== Public Functions =====================================================================

    public abstract void InitLayer(ref int[,] map);
    public abstract void Generate(ref int[,] map, WorldGenerator worldGen);
}
