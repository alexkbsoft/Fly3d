using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoTextPlacer : MonoBehaviour
{
    public GameObject realWorldObject;
    public TextMeshProUGUI _textmeshPro;
    
    private Vector3 _viewportPos;
    private RectTransform _rectTransform;

    private string _text = "0";

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _textmeshPro = GetComponent<TextMeshProUGUI>();
    }
 
    public void PlaceYourself()
    {
        if (realWorldObject == null)
        {
            return;
        }
        _viewportPos = Camera.main.WorldToViewportPoint(realWorldObject.transform.position);
        _rectTransform.anchoredPosition = new Vector2(1920 * _viewportPos.x + 120, 1080 * _viewportPos.y - 1080 + 150);
    }

    void Update()
    {
        PlaceYourself();
    }

    public void SetHitCount(int hitCount) {
        _text = hitCount.ToString();
        _textmeshPro.SetText("hits: " + _text);
    }
}
