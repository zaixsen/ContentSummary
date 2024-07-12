using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class Radar : MaskableGraphic
{
    float radius;

    float rad;

    int propertyCount;

    List<float> offset;

    public Texture texture;

    protected override void Awake()
    {
        offset = new List<float>() { 1, 0.6f, 1, 1, 1 };

        radius = rectTransform.sizeDelta.x / 2;
        propertyCount = 5;
        rad = 2 * Mathf.PI / propertyCount;
    }

    public void SetRadarData(RoleData roleData)
    {
        RoleProperty roleProperty = roleData.property;
        offset[0] = roleProperty.atk / 10f;
        offset[1] = roleProperty.defence / 10f;
        offset[2] = roleProperty.hp / 10f;
        offset[3] = roleProperty.magic / 10f;
        offset[4] = roleProperty.speed / 10f;
        texture = Resources.Load<Texture>(roleData.icon);
        SetAllDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        vh.AddVert(Vector3.zero, color, Vector2.one * .5f);
        for (int i = 0; i < propertyCount; i++)
        {
            float x = Mathf.Sin(rad * i) * radius * offset[i];
            float y = Mathf.Cos(rad * i) * radius * offset[i];

            float uvX = (Mathf.Sin(rad * i) * radius * offset[i] + radius) / rectTransform.sizeDelta.x;
            float uvY = (Mathf.Cos(rad * i) * radius * offset[i] + radius) / rectTransform.sizeDelta.y;

            vh.AddVert(new Vector3(x, y, 0), color, new Vector2(uvX, uvY));

            if (i == 0)
                vh.AddTriangle(0, 1, propertyCount);
            else
                vh.AddTriangle(0, i, i + 1);
        }
    }

    public override Texture mainTexture
    {
        get
        {
            if (texture != null)
            {
                return texture;
            }
            return base.mainTexture;
        }
    }

}
