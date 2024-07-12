using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotate3D : MonoBehaviour, IDragHandler, IEndDragHandler
{
    int roleCount;

    int currentIndex;

    float radius;

    float rad;

    float moveIncrement;

    float speed;

    List<GameObject> roles;

    List<RoleData> roleDatas;

    private void Start()
    {
        roles = new List<GameObject>();
        roleDatas = DataConfig.Instance.myData.roleDatas;
        roleCount = roleDatas.Count;
        radius = 1.5f;
        speed = 1;
        rad = 2 * Mathf.PI / roleCount;

        RotateRole();
    }

    private void RotateRole()
    {
        roleDatas = DataConfig.Instance.myData.roleDatas;
        for (int i = 0; i < roleCount; i++)
        {
            float x = Mathf.Sin(rad * i + moveIncrement + Mathf.PI) * radius;
            float z = Mathf.Cos(rad * i + moveIncrement + Mathf.PI) * radius;

            if (i >= roles.Count)
            {
                GameObject role = Instantiate(Resources.Load<GameObject>(roleDatas[i].model), transform);
                role.transform.eulerAngles = new Vector3(0, 180, 0);
                roles.Add(role);
            }
            roles[i].transform.localPosition = new Vector3(x, 0, z);
        }
        //寻找最前面的
        float minZ = roles.Min((x) => { return x.transform.position.z; });
        GameObject go = roles.Find((x) => minZ == x.transform.position.z);
        currentIndex = roles.IndexOf(go);
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveIncrement -= eventData.delta.x / radius * Time.deltaTime;
        RotateRole();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float offset = Mathf.Abs(moveIncrement % rad);

        float endPos = offset > rad / 2 ? rad - offset : -offset;

        if (moveIncrement < 0) endPos = -endPos;

        float timer = Mathf.Abs(offset * radius / speed);

        DOTween.To((x) =>
        {
            moveIncrement = x;
            RotateRole();
        }, moveIncrement, moveIncrement + endPos, timer).OnComplete(() =>
        {
            //Debug.Log(currentIndex);
        });
    }


}
