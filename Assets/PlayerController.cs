using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
    
        Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;
        Vector3 movement = direction * moveSpeed * Time.deltaTime;
    
        transform.position += movement;
    }
}
