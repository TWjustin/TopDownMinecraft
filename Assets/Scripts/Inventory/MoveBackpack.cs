using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBackpack : MonoBehaviour, IDragHandler
{
    public Canvas canvas;
    RectTransform currentRect;

    private void Awake()
    {
        currentRect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentRect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
