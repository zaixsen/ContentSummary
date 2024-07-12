
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class PlayerDressClothing : MonoBehaviour
{
    public static PlayerDressClothing Instance;

    public GameObject[] dressObj;

    Dictionary<string, Transform> dic_bones;

    Texture2D texture2D;

    SkinnedMeshRenderer skinnedMeshRenderer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dic_bones = new Dictionary<string, Transform>();
        texture2D = new Texture2D(1024, 1024);
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        Transform[] bones = GetComponentsInChildren<Transform>();

        foreach (Transform trs in bones)
        {
            dic_bones.Add(trs.name, trs);
        }
        DressClothing(0, "Dress/Tou_0");
    }

    public void DressClothing(int paters, string bodyPath)
    {
        dressObj[paters] = Resources.Load<GameObject>(bodyPath);

        Mesh mesh = new Mesh();

        List<CombineInstance> combineInstances = new List<CombineInstance>();
        List<Transform> bones = new List<Transform>();
        List<Texture2D> textures = new List<Texture2D>();

        for (int i = 0; i < dressObj.Length; i++)
        {
            //网格
            SkinnedMeshRenderer skm = dressObj[i].GetComponentInChildren<SkinnedMeshRenderer>();
            CombineInstance combineInstance = new CombineInstance();
            combineInstance.mesh = skm.sharedMesh;
            combineInstances.Add(combineInstance);

            //骨骼
            Transform[] subBones = skm.bones;
            foreach (Transform bone in subBones)
            {
                if (dic_bones.ContainsKey(bone.name))
                {
                    bones.Add(dic_bones[bone.name]);
                }
            }

            textures.Add(skm.sharedMaterial.mainTexture as Texture2D);

        }

        Rect[] rects = texture2D.PackTextures(textures.ToArray(), 0);
        texture2D.Apply();

        List<Vector2> uvs = new List<Vector2>();

        //计算uv
        for (int i = 0; i < dressObj.Length; i++)
        {
            SkinnedMeshRenderer skm = dressObj[i].GetComponentInChildren<SkinnedMeshRenderer>();
            Vector2[] subUvs = skm.sharedMesh.uv;

            for (int j = 0; j < subUvs.Length; j++)
            {
                //                      图片开始位置  + 图片宽度 * 自身 uv
                Vector2 uv = new Vector2(rects[i].x + rects[i].width * subUvs[j].x, rects[i].y + rects[i].height * subUvs[j].y);
                uvs.Add(uv);
            }
        }

        mesh.CombineMeshes(combineInstances.ToArray(), true, false);
        mesh.uv = uvs.ToArray();
        Material material = new Material(Shader.Find("CarToonQ"));

        Texture faceture = dressObj[0].GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.GetTexture("_FaceTex");
        material.SetTexture("_FaceTex", faceture);

        material.SetFloat("_ScaleX", texture2D.width / faceture.width);
        material.SetFloat("_ScaleY", texture2D.height / faceture.height);

        material.mainTexture = texture2D;
        skinnedMeshRenderer.material = material;
        skinnedMeshRenderer.sharedMesh = mesh;
        skinnedMeshRenderer.bones = bones.ToArray();
    }

}
