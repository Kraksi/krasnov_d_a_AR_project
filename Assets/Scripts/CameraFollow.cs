using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform Target;

    [Header("Settings")]
    public Vector3 Offset = new Vector3(0f, 3f, -6f);
    public float SmoothSpeed = 5f;

    private void LateUpdate()
    {
        if (Target == null)
        {
            Debug.LogWarning("CameraFollow: Target is not assigned");
            return;
        }

        Vector3 desiredPosition = Target.position + Target.rotation * Offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);

        transform.LookAt(Target);
    }
}