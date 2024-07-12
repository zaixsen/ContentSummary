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

    private int dataStartIndex;

    private int dataEndIndex;

    List<string> datas = new List<string>();

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            datas.Add(i.ToString());
        }

        itemHeight = items[0].GetComponent<RectTransform>().rect.size.y;
        //图片位置
        topItemYPos = items[0].transform.localPosition.y;

        for (int i = 0; i < items.Count; i++)
        {
            items[i].name = datas[i];
            items[i].GetComponentInChildren<Text>().text = datas[i];
        }
        dataStartIndex = 0;
        dataEndIndex = items.Count - 1;
    }

    public void OnDrag(PointerEventData eventData)
    {
        offset = eventData.delta.y;
        //拖拽 item跟着动
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.localPosition += Vector3.up * offset;
        }


        if (offset > 0)
        {
            //向上滑动

            //提前填充避免空位置
            if (items[0].transform.localPosition.y >= topItemYPos)
            {
                items[items.Count - 1].transform.localPosition = items[items.Count - 2].transform.localPosition - Vector3.up * itemHeight;
            }

            //重置索引
            if (items[0].transform.localPosition.y >= topItemYPos + itemHeight)
            {
                FiniteItem temp = items[0];

                temp.transform.localPosition = items[items.Count - 1].transform.localPosition - Vector3.up * itemHeight;
                for (int i = 1; i < items.Count; i++)
                {
                    items[i - 1] = items[i];
                }
                items[items.Count - 1] = temp;

                dataEndIndex++;
                dataEndIndex %= datas.Count;
                items[items.Count - 1].name = datas[dataEndIndex];
                items[items.Count - 1].GetComponentInChildren<Text>().text = datas[dataEndIndex];
                //第一个元素的数据索引
                dataStartIndex = int.Parse(items[0].name);
            }
        }
        else if (offset < 0)
        {
            //向下滑动
            //提前填充避免空位置
            if (items[0].transform.localPosition.y <= topItemYPos)
            {
                items[items.Count - 1].transform.localPosition = items[0].transform.localPosition + Vector3.up * itemHeight;
            }

            //重置索引
            if (items[0].transform.localPosition.y <= topItemYPos - itemHeight / 2)
            {
                dataStartIndex--;

                if (dataStartIndex < 0)
                {
                    dataStartIndex = datas.Count - 1;
                }

                items[items.Count - 1].name = datas[dataStartIndex];
                items[items.Count - 1].GetComponentInChildren<Text>().text = datas[dataStartIndex];

                FiniteItem temp = items[items.Count - 1];

                temp.transform.localPosition = items[0].transform.localPosition + Vector3.up * itemHeight;
                for (int i = items.Count - 1; i >= 1; i--)
                {
                    items[i] = items[i - 1];
                }
                items[0] = temp;

                //最后一个元素的数据索引
                dataEndIndex = int.Parse(items[items.Count - 1].name);
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
