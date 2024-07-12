using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
public class RoleData
{
    public int id;
    public string icon;
    public string model;
    public RoleProperty property;
}
public class RoleProperty
{
    public int atk;
    public int defence;
    public int speed;
    public int hp;
    public int magic;
}

public class MyData
{
    public string nickname;
    public List<RoleData> roleDatas;

    public MyData()
    {
        roleDatas = new List<RoleData>();
    }
}
public class DressData
{
    public int id;
    public int boneType;
    public string icon;
    public string prefab;
}

public class DataConfig
{
    private static DataConfig instance;
    public static DataConfig Instance
    {
        get
        {
            if (instance == null)
                instance = new DataConfig();
            return instance;
        }
    }

    public List<RoleData> RoleDatas;
    public MyData myData;
    public List<DressData> dressDatas;

    public DataConfig()
    {
        RoleDatas = JsonConvert.DeserializeObject<List<RoleData>>(Resources.Load<TextAsset>("Data/RoleRadarData").text);
        myData = JsonConvert.DeserializeObject<MyData>(Resources.Load<TextAsset>("Data/MyData").text);
        dressDatas = JsonConvert.DeserializeObject<List<DressData>>(Resources.Load<TextAsset>("Data/ChaneDressData").text);
        if (myData == null)
            myData = new MyData();
    }

    public void SetRole(RoleData roleData)
    {
        myData.roleDatas.Add(roleData);
        string mydata = JsonConvert.SerializeObject(myData);
        File.WriteAllText("Assets/Resources/Data/MyData.json", mydata);
    }

}
