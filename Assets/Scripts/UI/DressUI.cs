using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DressUI : MonoBehaviour
{
    public List<Toggle> dressTypes;
    public GameObject dressItem;
    public GameObject content;
    public Button closeButton;
    public List<DressData> dressDatas;


    private void Start()
    {
        dressDatas = JsonConvert.DeserializeObject<List<DressData>>(Resources.Load<TextAsset>("Data/ChaneDressData").text);

        //dressDatas = DataConfig.Instance.dressDatas;
        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.mainUI.gameObject.SetActive(true);

        });
        for (int i = 0; i < dressTypes.Count; i++)
        {
            int type = i;
            dressTypes[i].onValueChanged.AddListener((flag) =>
            {
                if (flag)
                {
                    ChangeClothing(type + 1);
                }
            });
        }
        ChangeClothing(1);
        dressItem.SetActive(false);
    }

    private void ChangeClothing(int type)
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            if (dressItem != content.transform.GetChild(i).gameObject)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < dressDatas.Count; i++)
        {
            if (dressDatas[i].boneType == type)
            {
                DressItem dress = Instantiate(dressItem, content.transform).gameObject.AddComponent<DressItem>();
                dress.gameObject.SetActive(true);
                dress.SetData(dressDatas[i]);
            }
        }
    }

}
