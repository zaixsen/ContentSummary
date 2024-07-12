using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DressItem : MonoBehaviour, IPointerClickHandler
{
    DressData dressData;
    public void SetData(DressData data)
    {
        dressData = data;
        GetComponent<Image>().sprite = Resources.Load<Sprite>(data.icon);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerDressClothing.Instance.DressClothing(dressData.boneType - 1, dressData.prefab);
    }
}
