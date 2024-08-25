using UnityEngine;

public class CannonTilt : MonoBehaviour
{
    float rotationSpeed = 90f; // 1초당 회전각도
    float timePassed = 0f;
    float tiltAngle = 90f;

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed <= tiltAngle/rotationSpeed)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
        else if (timePassed <= tiltAngle/rotationSpeed*2)
        {
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
        else
        {
            timePassed = 0f;
        }
    }


}
