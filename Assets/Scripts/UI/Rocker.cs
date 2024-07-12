using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rocker : MonoBehaviour, IDragHandler, IEndDragHandler
{
    float radius;

    Vector2 startPos;
    RectTransform rectTransform;
    public Camera UICamera;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        radius = 150;
        startPos = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, UICamera, out Vector2 pos);
        rectTransform.anchoredPosition = Vector2.ClampMagnitude(pos - startPos, radius);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = startPos;
    }

    public float GetAxis(string axisName)
    {
        switch (axisName)
        {
            case "H":
                return rectTransform.anchoredPosition.x / radius;
            case "V":
                return rectTransform.anchoredPosition.y / radius;
            default:
                break;
        }
        return 0f;
    }
}
