using System;
using UnityEngine;

public class WaterLocomotionController : MonoBehaviour
{

    // adjust acceleration, turn, and max speed values as desired
    public float acceleration = 10f;
    public float turn = 5f;
    public float maxSpeed = 4f;

    private Rigidbody2D rb;
    public bool isMovementRestricted;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        AudioManager.Instance.SetSwimVelocity(rb.velocity.magnitude);

        if (isMovementRestricted) return;

        float moveHorizontal = 0;
        float moveVertical = 0;
        if (Input.GetKey(KeyCode.W))
        {
            moveVertical += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVertical -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal += 1;
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // limit car's max speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            //rb.velocity = rb.velocity.normalized * maxSpeed;
        } else
        {
            rb.AddForce(movement * acceleration);
        }

        // rotate car based on movement direction
        if (movement != Vector2.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(Vector3.forward, movement),
                                                  turn * Time.deltaTime);
            rb.velocity = transform.up * rb.velocity.magnitude;
        }
    }

    public void addBreakForce(float tripForceMagnitude)
    {
        rb.velocity /= tripForceMagnitude;
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
