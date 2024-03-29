﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private RectTransform _initialRectTransformposition;

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("Main Canvas").GetComponent<Canvas>();
        }

        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = .5f;
        _canvasGroup.blocksRaycasts = false;
        _initialRectTransformposition = _rectTransform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        //_rectTransform.anchoredPosition = _initialRectTransformposition.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
