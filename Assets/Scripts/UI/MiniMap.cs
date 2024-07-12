using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{

    public Transform player;

    public GameObject[] enemyPos;

    public Image miniMapImage;

    public Image enemyPointerTemp;

    Texture2D miniMapTexture;

    List<Transform> enemiesPointer;

    private void Start()
    {
        miniMapTexture = new Texture2D(400, 400);
        enemiesPointer = new List<Transform>();
        enemyPos = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i <= miniMapTexture.width; i++)
        {
            for (int j = 0; j <= miniMapTexture.height; j++)
            {
                //                  这个值在地图上是0.04f 这里是0.01f的原因是 像素大小的问题图片像素为 400*400 所以要缩放4倍
                float y = Mathf.PerlinNoise(i * 0.01f, j * 0.01f) * 8;
                if (y > 2.6f)
                {
                    miniMapTexture.SetPixel(i, j, Color.green);
                }
                else
                {
                    miniMapTexture.SetPixel(i, j, Color.blue);
                }
            }
        }
        miniMapTexture.Apply();
        miniMapImage.sprite = Sprite.Create(miniMapTexture,
            new Rect(0, 0, miniMapTexture.width, miniMapTexture.height), Vector2.one * .5f);


        for (int i = 0; i < enemyPos.Length; i++)
        {
            var enemyPointer = Instantiate(enemyPointerTemp, enemyPointerTemp.transform.parent);
            enemiesPointer.Add(enemyPointer.transform);
        }
        enemyPointerTemp.gameObject.SetActive(false);
    }

    private void Update()
    {
        float x = player.transform.localPosition.x / 100;
        float y = player.transform.localPosition.z / 100;

        miniMapImage.rectTransform.pivot = new Vector2(x + .5f, y + .5f);
        miniMapImage.rectTransform.anchoredPosition = Vector2.zero;

        for (int i = 0; i < enemiesPointer.Count; i++)
        {
            //敌人位置 - 玩家位置 = 方向 通过方向映射到小地图上
            Vector3 dir = enemyPos[i].transform.position - player.position;
            enemiesPointer[i].localPosition = new Vector3(dir.x, dir.z, 0) * 4;
        }
    }
}
