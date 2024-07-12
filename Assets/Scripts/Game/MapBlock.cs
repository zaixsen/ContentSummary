using Newtonsoft.Json.Linq;
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
        CreatMap(0, 0);
    }
    private void CreatMap(int raw, int col)
    {
        transform.localPosition = new Vector3(raw * mapWidth - mapWidth / 2,0, col * mapHeight - mapWidth / 2);
        Mesh mesh = new Mesh();

        VertexHelper vh = new VertexHelper();

        for (int i = 0; i <= mapWidth; i++)
        {
            for (int j = 0; j <= mapHeight; j++)
            {
                float y = Mathf.PerlinNoise((i * raw + i) * 0.04f, (i * col + j) * 0.04f) * 8;

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
}
