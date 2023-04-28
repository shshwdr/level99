using System;
using UnityEngine;

public class WaterLocomotionController : MonoBehaviour
{

    // adjust acceleration, turn, and max speed values as desired
    public float acceleration = 10f;
    public float turn = 5f;
    public float maxSpeed = 4f;

    private Rigidbody2D _rb;
    public bool isMovementRestricted;
    [SerializeField] private DistanceJoint2D grappleJoint;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        AudioManager.Instance.SetSwimVelocity(_rb.velocity.magnitude);

        if (isMovementRestricted) return;

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // limit car's max speed
        if (_rb.velocity.magnitude > maxSpeed)
        {
            //rb.velocity = rb.velocity.normalized * maxSpeed;
        } else
        {
            _rb.AddForce(movement * acceleration);
        }

        
        
        // rotate based on movement direction
        if (movement != Vector2.zero)
        {
            if (grappleJoint.reactionForce.magnitude > 1)
            {
                //Debug.Log($"GRAAAAPPPLLLLEE - {grappleJoint.reactionForce.magnitude}");
                Vector3 crossProduct =
                    Vector3.Cross(transform.forward, grappleJoint.transform.position - transform.position);
                bool isClockwise = crossProduct.y < 0;
                float isClockwiseModifier = 1;
                if (!isClockwise) isClockwiseModifier = -1;
                Debug.Log($"{crossProduct}");
                Debug.Log($"Clockwise: {isClockwise}");
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(Vector3.forward,  Vector2.Perpendicular(grappleJoint.transform.position - transform.position) * isClockwiseModifier),
                    turn * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(Vector3.forward, movement),
                    turn * Time.deltaTime);
            
            
                _rb.velocity = transform.up * _rb.velocity.magnitude;
            }
               
            
        }
    }

    public void addBreakForce(float tripForceMagnitude)
    {
        _rb.velocity /= tripForceMagnitude;
    }

    private void OnDisable()
    {
        AudioManager.Instance.SetSwimVelocity(0);

    }
    internal void setMovementRestriction(bool isRestricted)
    {
        isMovementRestricted = isRestricted;
    }
}
