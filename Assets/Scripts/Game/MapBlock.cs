using Newtonsoft.Json.Linq;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MapBlock : MonoBehaviour
{
    int mapWidth = 100;
    int mapHeight = 100;

    public Material material;
    private void Awake()
    {
        //CreatMap(0, 0);
    }

    public void CreatMap(MapIndex mapIndex)
    {
        transform.localPosition = new Vector3(mapIndex.x * mapWidth - mapWidth / 2, 0, mapIndex.y * mapHeight - mapWidth / 2);
        Mesh mesh = new Mesh();

        VertexHelper vh = new VertexHelper();

        for (int i = 0; i <= mapWidth; i++)
        {
            for (int j = 0; j <= mapHeight; j++)
            {
                float y = Mathf.PerlinNoise((mapWidth * mapIndex.x + i) * 0.04f, (mapHeight * mapIndex.y + j) * 0.04f) * 8;

                vh.AddVert(new Vector3(i, y, j), Color.red, Vector2.zero);

                if (i < mapWidth && j < mapHeight)
                {
                    int fristCol = i * (mapHeight + 1) + j;
                    int seconedCol = (i + 1) * (mapHeight + 1) + j;

                    vh.AddTriangle(fristCol, fristCol + 1, seconedCol + 1);
                    vh.AddTriangle(fristCol, seconedCol + 1, seconedCol);
                }
            }
        }
        vh.FillMesh(mesh);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = material;
        gameObject.AddComponent<MeshCollider>();
    }

    public void UpdateMap(MapIndex mapIndex)
    {
        transform.localPosition = new Vector3(mapIndex.x * mapWidth - mapWidth / 2, 0, mapIndex.y * mapHeight - mapWidth / 2);
        Mesh mesh = new Mesh();

        VertexHelper vh = new VertexHelper();

        for (int i = 0; i <= mapWidth; i++)
        {
            for (int j = 0; j <= mapHeight; j++)
            {
                float y = Mathf.PerlinNoise((mapWidth * mapIndex.x + i) * 0.04f, (mapHeight * mapIndex.y + j) * 0.04f) * 8;

                vh.AddVert(new Vector3(i, y, j), Color.red, Vector2.zero);

                if (i < mapWidth && j < mapHeight)
                {
                    int fristCol = i * (mapHeight + 1) + j;
                    int seconedCol = (i + 1) * (mapHeight + 1) + j;

                    vh.AddTriangle(fristCol, fristCol + 1, seconedCol + 1);
                    vh.AddTriangle(fristCol, seconedCol + 1, seconedCol);
                }
            }
        }
        vh.FillMesh(mesh);
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
