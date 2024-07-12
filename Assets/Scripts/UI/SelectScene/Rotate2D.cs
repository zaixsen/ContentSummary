using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Rotate2D : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject iconTemp;

    public Button randomButton;

    public Button continueButton;

    public Button backButton;

    public Transform roleParent;

    public Radar radar;

    int roleIconCount;

    int currentIndex;

    float radius;

    float rad;

    float moveIncrement;

    float speed;

    float space;

    bool isClickRandom;

    RectTransform iconRectTransform;

    List<GameObject> icons;

    List<GameObject> sorts;

    List<GameObject> roles;

    List<RoleData> roleDatas;

    private void Start()
    {
        iconRectTransform = iconTemp.transform as RectTransform;
        icons = new List<GameObject>();
        sorts = new List<GameObject>();
        roles = new List<GameObject>();
        roleDatas = DataConfig.Instance.RoleDatas;
        roleIconCount = DataConfig.Instance.RoleDatas.Count;
        isClickRandom = true;
        space = 200;
        currentIndex = 0;
        radius = (iconRectTransform.rect.width + space) * roleIconCount / (2 * Mathf.PI);
        rad = 2 * Mathf.PI / roleIconCount;
        speed = 400;
        randomButton.onClick.AddListener(RandomRole);
        continueButton.onClick.AddListener(CreateRole);
        backButton.onClick.AddListener(CloseUI);
        iconTemp.SetActive(false);
        RotateIcon();
        SelectedRole();
    }

    private void CreateRole()
    {
        DataConfig.Instance.SetRole(roleDatas[currentIndex]);
        CloseUI();
    }

    private void CloseUI()
    {
        UIManager.Instance.SetCreateRoleUI(false);
        roleParent.gameObject.SetActive(false);
    }

    private void RandomRole()
    {
        if (!isClickRandom) return;

        isClickRandom = false;

        int randomIndex = Random.Range(0, roleIconCount);

        int positive = currentIndex - randomIndex;

        int negative = roleIconCount - Mathf.Abs(positive);

        negative = positive < 0 ? negative : -negative;

        int minMove = Mathf.Abs(positive) > Mathf.Abs(negative) ? negative : positive;

        float minRad = minMove * rad;

        float timer = Mathf.Abs(minRad * radius / speed);

        DOTween.To((x) =>
        {
            moveIncrement = x;
            RotateIcon();
        }, moveIncrement, moveIncrement + minRad, timer).
        OnComplete(() =>
        {
            isClickRandom = true;
            SelectedRole();
        });
    }

    private void SelectedRole()
    {
        for (int i = 0; i < roleIconCount; i++)
            roles[i].SetActive(i == currentIndex);

        radar.SetRadarData(roleDatas[currentIndex]);
    }

    private void RotateIcon()
    {
        for (int i = 0; i < roleIconCount; i++)
        {
            float x = Mathf.Sin(rad * i + moveIncrement + Mathf.PI) * radius;
            float y = Mathf.Cos(rad * i + moveIncrement + Mathf.PI) * radius;

            if (i >= icons.Count)
            {
                GameObject icon = Instantiate(iconTemp, transform);
                GameObject role = Instantiate(Resources.Load<GameObject>(roleDatas[i].model), roleParent);
                icon.SetActive(true);
                icons.Add(icon);
                icons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(roleDatas[i].icon);
                sorts.Add(icon);
                roles.Add(role);
            }
            icons[i].transform.localPosition = new Vector3(x, 0, y);
            float scale = (radius - y) / (2 * radius);
            icons[i].transform.localScale = Vector3.one * scale;
        }

        sorts.Sort((x, y) => { return x.transform.localScale.x > y.transform.localScale.x ? 1 : -1; });

        for (int i = 0; i < roleIconCount; i++)
            sorts[i].transform.SetSiblingIndex(i);

        currentIndex = icons.IndexOf(sorts[roleIconCount - 1]);
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveIncrement -= eventData.delta.x / radius;
        RotateIcon();
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
            RotateIcon();
        }, moveIncrement, moveIncrement + endPos, timer).
        OnComplete(() =>
        {
            SelectedRole();
        });
    }
}
