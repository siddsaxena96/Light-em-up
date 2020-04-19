using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target = null;
    public Vector3 offset = Vector3.zero;
    public float smoothSpeed = 0f;
    public float minX, maxX, minY, maxY;

    private Vector3 currentPos = Vector3.zero;
    private Vector3 targetPos = Vector3.zero;
    private Vector3 desiredPos = Vector3.zero;
    private Vector3 currentVelocity = Vector3.zero;


    void LateUpdate()
    {
        if (target)
        {
            currentPos = transform.position;
            targetPos = target.position + offset;
            transform.position = Vector3.Slerp(currentPos, targetPos, smoothSpeed);
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minX, maxX),
                Mathf.Clamp(transform.position.y, minY, maxY),
                transform.position.z);
        }
    }
}
