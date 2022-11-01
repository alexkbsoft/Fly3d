using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public InfoTextPlacer infoText;

    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Terrain terrain;
    [SerializeField]
    private InfoCanvas infoCanvas;

    private NavMeshAgent _agent;
    private Camera _mainCam;

    private int _hitCount = 0;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target)
        {
            var pos = _target.transform.position;
            pos.y = terrain.SampleHeight(pos);

            _agent.SetDestination(pos);
            var calculatedPos = transform.position.y + _agent.height / 2.0f;

            if (Mathf.Abs(calculatedPos - _target.position.y) >= 0.001f)
            {
                var curPos = transform.position;
                curPos.y = calculatedPos;
                var desiredPos = curPos;
                desiredPos.y = _target.position.y;

                var newPos = Vector3.MoveTowards(curPos, desiredPos, 4.0f * Time.deltaTime);
                _agent.baseOffset += newPos.y - calculatedPos;
            }
        }

        // int layerMask = 1 << 6;
        // var plane = new Plane(infoCanvas.transform.forward, infoCanvas.transform.position);
        // var ray = new Ray(transform.position, _mainCam.transform.position - transform.position);

        // if (plane.Raycast(ray, out var enter))
        // {
        //     Debug.Log("HIT");
        //     Vector3 hitPoint = ray.GetPoint(enter);

        //     infoCanvas.SetStartLinePos(hitPoint);
        // }

        // if (Physics.Raycast(
        //     transform.position, 
        //     _mainCam.transform.position - transform.position, 
        //     out var hitPos,
        //     Mathf.Infinity, layerMask)) {
        //         Debug.Log("HIT!");
        //     infoCanvas.SetStartLinePos(hitPos.point);
        // }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {   
            _hitCount++;

            if (infoText != null)
            {
                infoText.SetHitCount(_hitCount);
            }
            
        }

    }
}
