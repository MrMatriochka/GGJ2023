using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeBar : MonoBehaviour
{
    public Slider slider;
    public int maxSize;
    public int actualSize;

    private void Start()
    {
        SetMaxSize(maxSize);
    }

    public void SetMaxSize(int size)
    {
        slider.maxValue = size;
        slider.value = actualSize;
    }

    private void SetSize(int size)
    {
        slider.value = size;
    }

    private void Update()
    {
        SetSize(actualSize);
    }
}
