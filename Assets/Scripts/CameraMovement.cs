using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;
    public float minX,maxX,minY,maxY;
        

    Vector3 currentPos, targetPos, desiredPos;
    Vector3 currentVelocity;
	
	// Update is called once per frame
	void LateUpdate () {
		if(target)
        {
            currentPos = transform.position;
            targetPos = target.position + offset;
            //desiredPos = Vector3.SmoothDamp(currentPos, targetPos, ref currentVelocity, smoothSpeed * Time.deltaTime);
            transform.position = Vector3.Slerp(currentPos, targetPos, smoothSpeed);
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x,minX,maxX),
                Mathf.Clamp(transform.position.y,minY,maxY),
                transform.position.z);
        }
	}
}
