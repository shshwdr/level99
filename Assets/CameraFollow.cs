using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's transform
    public float smoothing = 5f; // How quickly the camera will move towards the player

    private Vector3 offset; // The initial offset between the camera and the player

    void Start()
    {
        offset = transform.position - target.position; // Calculate the initial offset
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset; // Calculate the target camera position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime); // Move the camera towards the target position
    }
}