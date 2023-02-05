using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTest : MonoBehaviour
{
    public Slider prefabSlider;
    private Slider useSlider;
    [SerializeField]private Vector3 offset;

    private void Start()
    {
        useSlider = Instantiate(prefabSlider, FindObjectOfType<Canvas>().transform).GetComponent<Slider>();
        useSlider.value = 0;

    }
    
    private void Update()
    {
        useSlider.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + offset);
    }


    // public Transform player;
    // //public RectTransform healthBarRect;
    // private Camera mainCamera;
    // public Vector3 offset;
    // // private Vector2 viewportPosition;
    // // private Vector2 worldObject_ScreenPosition;
    //
    // void Start()
    // {
    //     mainCamera = Camera.main;
    // }
    //
    // private void Update()
    // {
    //     Vector3 pos = mainCamera.WorldToScreenPoint(player.position + offset);
    //
    //     if (transform.position != pos)
    //         transform.position = pos;
    // }
    //
    //
    // // void LateUpdate()
    // // {
    // //     viewportPosition = mainCamera.WorldToViewportPoint(player.position);
    // //     worldObject_ScreenPosition = new Vector2(
    // //         ((viewportPosition.x * healthBarRect.sizeDelta.x) - (healthBarRect.sizeDelta.x * 0.5f)),
    // //         ((viewportPosition.y * healthBarRect.sizeDelta.y) - (healthBarRect.sizeDelta.y * 0.5f)));
    // //     healthBarRect.anchoredPosition = worldObject_ScreenPosition;
    // // }
}
