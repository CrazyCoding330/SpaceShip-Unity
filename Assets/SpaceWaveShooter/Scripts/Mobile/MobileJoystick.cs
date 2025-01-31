﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Range(0f, 2f)] public float handleLimit = 1f;

    public Vector3 dir
    {
        get
        {
            return _dir;
        }
    }

    private Vector3 _dir;

    [Header("Components")]
    public RectTransform background;
    public RectTransform handle;

    private Vector2 joystickPosition;

    void Start ()
    {
        joystickPosition = background.position;
    }

    public void OnDrag (PointerEventData eventData)
    {
        Vector2 direction = eventData.position - joystickPosition;
        Vector2 d = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        handle.anchoredPosition = (d * background.sizeDelta.x / 2f) * handleLimit;

        _dir = new Vector3(d.x, 0, d.y);
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp (PointerEventData eventData)
    {
        _dir = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}