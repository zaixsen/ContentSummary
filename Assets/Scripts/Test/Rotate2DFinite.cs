using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class Rotate2DFinite : MonoBehaviour, IEndDragHandler, IDragHandler
{
    [SerializeField]
    Image img;

    float r = 0;
    int n = 5;
    int space = 200;
    float rad = 0;

    [SerializeField]
    float move = 0;

    List<FiniteItem> list = new List<FiniteItem>();
    List<FiniteItem> sort = new List<FiniteItem>();

    int nowIndex = 0;
    int currentIndex = 0;
    int itemCount = 5;

    int dataCount;



    bool OkDrag = true;

    private void Start()
    {
        Init();
        dataCount = allNum.Count;
        r = (img.rectTransform.sizeDelta.x + space) * n / (2 * Mathf.PI);
        rad = 2 * Mathf.PI / n;

        Move();
        img.gameObject.SetActive(false);

    }

    List<string> allNum = new List<string>();

    public void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            allNum.Add(i.ToString());
        }
    }

    public void Move()
    {
        for (int i = 0; i < n; i++)
        {
            float x = Mathf.Sin(i * rad + move) * r;
            float y = Mathf.Cos(i * rad + move) * r;
            if (i >= list.Count)
            {
                GameObject go = Instantiate(img.gameObject, transform);
                list.Add(go.GetComponent<FiniteItem>());
                sort.Add(go.GetComponent<FiniteItem>());
                go.name = i.ToString();

            }
            list[i].transform.localPosition = new Vector3(x, 0, y);

            list[i].gameObject.SetActive(y > 0);

            float scale = (r + y) / (2 * r);
            list[i].transform.localScale = Vector3.one * scale;
        }

        sort.Sort((a, b) => { return (int)(a.transform.localScale.x * 100 - b.transform.localScale.x * 100); });

        for (int i = 0; i < sort.Count; i++)
        {
            sort[i].transform.SetSiblingIndex(i);
            if (list[i] == sort[sort.Count - 1])
            {
                //��ǰ��ǰ����ͼƬ��ԭ�����±�
                nowIndex = i;
            }
        }

        //������ǰ�����������
        currentIndex = -Mathf.RoundToInt(move / rad);
        //���ⳬ������
        if (Mathf.Abs(currentIndex) > dataCount) move %= rad;

        //�������������� ��������������� + ��ǰ���� ��С����ģ�
        currentIndex = currentIndex < 0 ? dataCount + currentIndex : currentIndex;

        //��������
        for (int i = -2; i <= 2; i++) SetItemAndDataIndex(i);

    }

    public void SetItemAndDataIndex(int offset)
    {
        // ������Ч��������ȷ����Խ��
        int effectiveIndex = (nowIndex + offset + itemCount) % itemCount;
        int effectiveDataIndex = (currentIndex + offset + dataCount) % dataCount;

        // �����������
        list[effectiveIndex].SetItemIndexNum(effectiveDataIndex);
    }

    public void OnDrag(PointerEventData eventData)
    {
        move += eventData.delta.x / r;
        Move();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float offset = Mathf.Abs(move % rad);
        float endPos = offset > rad * 0.5f ? rad - offset : -offset;
        if (move < 0) endPos = -endPos;
        DOTween.To((x) =>
        {
            move = x;
            Move();
        }, move, move + endPos, 0.5f);
    }
}
