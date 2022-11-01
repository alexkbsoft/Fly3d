using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _force;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private Transform gun;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float maxVelocity = 5;
    
    
    private Vector3 _currentVelocity = Vector3.zero;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var (horizontal, vertical, up) = movementCoord();

        if (gun != null && projectilePrefab != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(projectilePrefab, gun.position, transform.rotation);
        }

        _rb.drag = horizontal == 0 && vertical == 0 && up == 0 ? 3 : 0;
    }

     void FixedUpdate () {
        var (horizontal, vertical, up) = movementCoord();
        float mouseR = Input.GetAxis("Mouse X");
        // float joyR = Input.GetAxis("RightJoystik");
        // float rotation = (mouseR != 0 ? mouseR : joyR);
        float rotation = mouseR;

        Vector3 force = new Vector3(horizontal, up, vertical) * _force;
        _rb.AddRelativeForce(force);

        if (_rb.velocity.magnitude >= maxVelocity)
        {    
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxVelocity);
        }

        _rb.angularVelocity = new Vector3(0.0f, rotation * _rotationSpeed, 0.0f);
     }

     (float, float, float) movementCoord() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool space = Input.GetKey(KeyCode.Space);
        bool ctrl = Input.GetKey(KeyCode.LeftControl);
        float up = space ? 1 : ctrl ? -1 : 0;

        return (horizontal, vertical, up);
     }

    ////  Variant with manula acceleration:
    
    //  Vector3 getNewVelocity() {
    //     float horizontal = Input.GetAxis("Horizontal");
    //     float vertical = Input.GetAxis("Vertical");
    //     float hAccel = horizontal == 0 ? dragAcceleration : acceleration;
    //     float fAccel = vertical == 0 ? dragAcceleration : acceleration;

    //     float newHVel = _currentVelocity.x + 
    //         horizontal * transform.forward * hAccel * Time.fixedDeltaTime;
    //     newHVel = newHVel < 0 ? 0 : newHVel;

    //     float newForwardVel = _currentVelocity.z + 
    //         vertical * transform.right * fAccel * Time.fixedDeltaTime;
    //     newForwardVel = newForwardVel < 0 ? 0 : newForwardVel;

    //     return new Vector3(newHVel, 0, newForwardVel);
    //  }
}
