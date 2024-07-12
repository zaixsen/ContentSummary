using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiniteItem : MonoBehaviour
{
    [SerializeField]
    Text ImageIndex;
    [SerializeField]
    Text ItemIndex;

    public int ImageIndexNum;
    public int ItemIndexNum;

    void Start()
    {
        ImageIndex.text = ImageIndexNum.ToString();
    }
    private void Update()
    {
        //ItemIndex.text = ItemIndexNum.ToString();
    }
    internal void SetItemIndexNum(int index)
    {
        ItemIndexNum = index;
        ItemIndex.text = index.ToString();
    }
}




