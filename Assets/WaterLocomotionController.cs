using System;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterLocomotionController : MonoBehaviour
{

    // adjust acceleration, turn, and max speed values as desired
    public float acceleration = 10f;
    public float turn = 5f;
    [FormerlySerializedAs("maxNormalSwimSpeed")] [FormerlySerializedAs("maxSpeed")] public float normalSwimMaxSpeed = 4f;
    private float _grappleTightMaxSpeed;
    private bool _isGrappleTight;

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

        ThrottleSpeed(movement);

        // rotate based on movement direction
        if (movement != Vector2.zero)
        {
            float grappleForce = grappleJoint.reactionForce.magnitude;
            if (grappleForce > 0)
            {
                _isGrappleTight = true;
                Vector3 crossProduct =
                    Vector3.Cross(movement, grappleJoint.transform.position - transform.position);
                float isClockwiseModifier = crossProduct.z < 0 ? 1 : -1;
                
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

                //set grapple max speed when we do grapple
                _grappleTightMaxSpeed = _rb.velocity.magnitude;
                Debug.Log($"Grapple max{_grappleTightMaxSpeed}");
                _isGrappleTight = false;
            }
        }
    }

    private void ThrottleSpeed(Vector2 movement)
    {
        float maxSpeed;
        if (_isGrappleTight)
        {
            maxSpeed = Math.Max(_grappleTightMaxSpeed, normalSwimMaxSpeed);
            if (_rb.velocity.magnitude <= maxSpeed)
            {
                _rb.AddForce(10f * acceleration * movement );
            }
        }
        else
        {
            maxSpeed = normalSwimMaxSpeed;
            if (_rb.velocity.magnitude <= maxSpeed)
            {
                _rb.AddForce(movement * acceleration);
            }
        }/*
        // limit max speed
        if (_rb.velocity.magnitude > maxSpeed)
        {
            //rb.velocity = rb.velocity.normalized * maxSpeed;
        } else
        {
            _rb.AddForce(movement * acceleration);
        }*/
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
