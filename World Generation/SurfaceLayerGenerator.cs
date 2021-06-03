using UnityEngine;

public class SurfaceLayerGenerator : LayerGenerator
{
    // ===== Public Variables =====================================================================

    [Space(10)]
    [Header("Surface Materials Details")]
    public int dirt;
    public int grass;

    [Space(10)]
    [Header("Surface Details")]

    public int smoothing;

    [Space(10)]
    [Header("Hill Details")]

    public int numHills;
    public int steepness;

    // ===== Public Functions =====================================================================

    public override void InitLayer (ref int[,] map)
    {

    }

    public override void Generate (ref int[,] map, WorldGenerator worldGen)
    {
        int[] heights = new int[xSize];
        int x = 0;

        while (x < xSize)
        {
            int height;
            int end = x + worldGen.Int(1, smoothing);

            if (x == 0)
                height = worldGen.Int(1, ySize);
                
            else
                height = worldGen.Int(Mathf.Max(heights[x - 1] - 2, 2), Mathf.Min(heights[x - 1] + 2, ySize));

            while (x < end)
            {
                if (x == xSize)
                    break;

                heights[x] = height;

                for (int y = 0; y < heights[x]; y++)
                    map[x, yStart + y] = dirt;

                x++;
            }
        }

        // Place hills
        for (int hills = 0; hills < numHills; hills++)
        {
            int startPos = worldGen.Int(0, xSize);
            int maxHeight = worldGen.Int(3, steepness);

            x = startPos;
            int height = heights[x];

            while (height < maxHeight)
            {
                int len = x + worldGen.Int(0, smoothing);

                while (x < len)
                {
                    if (x == xSize)
                        break;

                    heights[x] = height;

                    for (int y = 0; y < heights[x]; y++)
                        map[x, yStart + y] = dirt;

                    x++;
                }

                height++;
            }

            int endPos = x + worldGen.Int(smoothing / 2, smoothing * 2);

            if (endPos >= xSize)
                endPos = xSize - 1;

            int endHieght = heights[endPos];
            while (height > endHieght)
            {
                int len = x + worldGen.Int(1, smoothing);

                while (x < len)
                {
                    if (x == xSize)
                        break;

                    heights[x] = height;

                    for (int y = 0; y < heights[x]; y++)
                        map[x, yStart + y] = dirt;

                    x++;
                }

                height--;
            }
        }

        // Place grass
        for (x = 0; x < xSize; x ++)
            map[x, yStart + heights[x]] = grass;
    }
}
