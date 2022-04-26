using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] GameObject dirt, grass;
    // Start is called before the first frame update
    void Start()
    {
        Generation();
    }

    private void Generation()
    {
        for (int x = 0; x < width; x++)
        {
            int minHeight = height - 1;
            int maxHeight = height + 2;

            height = Random.Range(minHeight, maxHeight);

            for (int y = 0; y < height; y++)
            {
                SpawnObj(dirt, x, y);
            }

            SpawnObj(grass, x, height);
        }
    }

    private void SpawnObj(GameObject obj, int width, int height)
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }
}
