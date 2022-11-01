using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCanvas : MonoBehaviour
{
    public GameObject realWorldObject;
    public GameObject infoText;
    
    private LineRenderer _lineRenderer;
    private Vector3 _pos;
    private Camera _mainCam;
    private RectTransform _rectTransform;


    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _mainCam = Camera.main;
        _rectTransform = infoText.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int layerMask = 1 << 6;
        var plane = new Plane(transform.forward, transform.position);
        var ray = new Ray(
            realWorldObject.transform.position 
                + _mainCam.transform.right.normalized * 0.2f
                + _mainCam.transform.up.normalized * 0.2f,
            _mainCam.transform.position - realWorldObject.transform.position);

        if (plane.Raycast(ray, out var enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
    
            _lineRenderer.SetPosition(0, hitPoint);
            var point2 = hitPoint + transform.TransformDirection(Vector3.up + Vector3.right) * 0.15f;
            _lineRenderer.SetPosition(1, point2);
            _lineRenderer.SetPosition(2, point2 + transform.TransformDirection(Vector3.right) * 0.55f);

        }
    }

    public void SetStartLinePos(Vector3 pos) {
        _pos = pos;
    }
}
