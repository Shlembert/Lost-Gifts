using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDisplay : MonoBehaviour
{
    private float _startAspect = 1366f / 768f;

    private float _defaultHeight;
    private float _defaultWidth;

    private void Awake()
    {
        _defaultHeight = Camera.main.orthographicSize;
        _defaultWidth = Camera.main.orthographicSize * _startAspect;

        Camera.main.orthographicSize = _defaultWidth / Camera.main.aspect;
    }

    void Update()
    {
        Camera.main.orthographicSize = _defaultWidth / Camera.main.aspect;
    }
}

