using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Texture2D level_map;
    public ColorToPrefab[] colorMappings;
    public Vector2 offset;

	// Use this for initialization
	void Start () {
        GenerateLevel();
	}

    void GenerateLevel()
    {
        for (int i=0; i<level_map.width; i++)
        {
            for (int j=0; j<level_map.height; j++)
            {
                GenerateTile(i, j);
            }
        }
    }

    void GenerateTile(int i, int j)
    {
        Color pixelColor = level_map.GetPixel(i, j);

        if (pixelColor.a == 0)
        {
            return;
        }

        foreach(ColorToPrefab colorMapping in colorMappings)
        {
            if(colorMapping.color.Equals(pixelColor))
            {
                Vector2 pos = new Vector2(i, j);
                Instantiate(colorMapping.prefab, pos+offset, Quaternion.identity, transform);
            }
        }
    }
}
