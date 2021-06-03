using UnityEngine;
using System.Collections.Generic;

public class CavernLayerGenerator : LayerGenerator
{
    // ===== Public Functions =====================================================================

    public override void InitLayer (ref int[,] map)
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = yStart; y < yEnd; ++y)
            {
                map[x, y] = block;
            }
        }
    }

    public override void Generate (ref int[,] map, WorldGenerator worldGen)
    {
        List<(int xCoord, int yCoord)> rooms = new List<(int xCoord, int yCoord)>();

        // Pick rooms
        int numRoomsRand = Mathf.RoundToInt(worldGen.Float(numRooms * 0.75f, numRooms * 1.25f));
        for (int i = 0; i < numRoomsRand; ++i)
            rooms.Add((worldGen.Int(0, xSize), worldGen.Int(yStart, yEnd)));

        // Generate rooms
        foreach (var c in rooms)
        {
            if (map[c.xCoord, c.yCoord] == 4)
                continue;

            map[c.xCoord, c.yCoord] = 0;
            int roomSize = worldGen.Int(minRoomSize, maxRoomSize);
            List<(int adjX, int adjY)> adj = new List<(int, int)> { c };

            AddAdjTiles(ref map, c.xCoord, c.yCoord, adj);

            for (int i = 0; i < roomSize; ++i)
            {
                if (adj.Count == 0)
                    break;

                (int x, int y) = adj[worldGen.Int(0, adj.Count)];

                map[x, y] = 0;
                AddAdjTiles(ref map, x, y, adj);
            }
        }
    }

    // ===== Protected Functions ==================================================================

    protected void AddAdjTiles (ref int[,] map, int x, int y, List<(int, int)> adj)
    {
        if (x > 0 && map[x - 1, y] != 0 && map[x - 1, y] != 4)
            adj.Add((x - 1, y));

        if (x < xSize - 1 && map[x + 1, y] != 0 && map[x + 1, y] != 4)
            adj.Add((x + 1, y));

        if (y > 0 && map[x, y - 1] != 0 && map[x, y - 1] != 4)
            adj.Add((x, y - 1));

        if (y < ySize - 1 && map[x, y + 1] != 0 && map[x, y + 1] != 4)
            adj.Add((x, y + 1));
    }
}