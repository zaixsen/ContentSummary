using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MapIndex
{
    public int x;
    public int y;

    public MapIndex(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class MapMgr : MonoBehaviour
{
    Transform player;

    int mapWidth = 100;
    int mapHeight = 100;

    MapIndex currentIndex;

    Dictionary<MapBlock, MapIndex> dic_mapBlock;

    private void Start()
    {
        dic_mapBlock = new Dictionary<MapBlock, MapIndex>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                MapBlock mapBlock = Instantiate(Resources.Load<MapBlock>("Prefab/MapBlock"));
                MapIndex mapIndex = new MapIndex(i, j);
                mapBlock.CreatMap(mapIndex);
                dic_mapBlock.Add(mapBlock, mapIndex);
            }
        }
        currentIndex = new MapIndex(0, 0);
    }

    private void Update()
    {
        int x = Mathf.FloorToInt((player.position.x + 50) / mapWidth);

        int y = Mathf.FloorToInt((player.position.z + 50) / mapHeight);

        MapIndex mapIndex = new MapIndex(x, y);

        if (mapIndex.x != currentIndex.x || mapIndex.y != currentIndex.y)
        {
            //计算新地图位置
            List<MapIndex> newMapIndex = new List<MapIndex>();
            for (int i = mapIndex.x - 1; i <= mapIndex.x + 1; i++)
            {
                for (int j = mapIndex.y - 1; j <= mapIndex.y + 1; j++)
                {
                    newMapIndex.Add(new MapIndex(i, j));
                }
            }

            //筛选新地图位置和废弃地图块
            List<MapBlock> oldMapBlocks = new List<MapBlock>();
            List<MapIndex> mapIndices = new List<MapIndex>();
            int index = 0;
            foreach (var mapblock in dic_mapBlock.Keys)
            {
                if (dic_mapBlock[mapblock].x != newMapIndex[index].x || dic_mapBlock[mapblock].y != newMapIndex[index].y)
                {
                    //添加不是重合的地图块
                    oldMapBlocks.Add(mapblock);
                    mapIndices.Add(newMapIndex[index]);
                }
                index++;
            }

            //刷新地图索引
            for (int i = 0; i < oldMapBlocks.Count; i++)
            {
                oldMapBlocks[i].UpdateMap(mapIndices[i]);
            }
            currentIndex = mapIndex;
        }
    }
}
