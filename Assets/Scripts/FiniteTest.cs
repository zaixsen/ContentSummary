using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FiniteTest : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform content;

    public List<FiniteItem> items;

    private float offset;

    private float itemHeight;

    private float topItemYPos;

    List<string> datas = new List<string>();

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            datas.Add(i.ToString());
        }

        itemHeight = items[0].GetComponent<RectTransform>().rect.size.y;
        //ͼƬλ��
        topItemYPos = items[0].transform.localPosition.y;

        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponentInChildren<Text>().text = datas[i];
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        offset = eventData.delta.y;
        //��ק item���Ŷ�
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.localPosition += Vector3.up * offset;
        }

        if (offset > 0)
        {
            //���ϻ���

            //��ǰ�������λ��
            if (items[0].transform.localPosition.y >= topItemYPos)
            {
                items[items.Count - 1].transform.localPosition = items[items.Count - 2].transform.localPosition - Vector3.up * itemHeight;
                //items[items.Count - 1].GetComponentInChildren<Text>().text = datas[endIndex];
            }
            //��������
            if (items[0].transform.localPosition.y >= topItemYPos + itemHeight)
            {
                FiniteItem temp = items[0];
                temp.transform.localPosition = items[items.Count - 1].transform.localPosition - Vector3.up * itemHeight;
                for (int i = 1; i < items.Count; i++)
                {
                    items[i - 1] = items[i];
                }
                items[items.Count - 1] = temp;
            }

        }
        else if (offset < 0)
        {
            //���»���
            //��ǰ�������λ��
            if (items[0].transform.localPosition.y <= topItemYPos)
            {
                items[items.Count - 1].transform.localPosition = items[0].transform.localPosition + Vector3.up * itemHeight;

                //items[items.Count - 1].GetComponentInChildren<Text>().text = datas[startIndex];
            }
            //��������
            if (items[0].transform.localPosition.y <= topItemYPos - itemHeight)
            {
                FiniteItem temp = items[items.Count - 1];
                temp.transform.localPosition = items[0].transform.localPosition + Vector3.up * itemHeight;
                for (int i = items.Count - 1; i >= 1; i--)
                {
                    items[i] = items[i - 1];
                }
                items[0] = temp;

            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
