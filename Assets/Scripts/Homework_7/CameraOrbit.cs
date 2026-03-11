using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [Header("Target")]
    public Transform Target;

    [Header("Settings")]
    public float Distance = 5f;
    public float MouseSensitivity = 3f;
    public float ScrollSpeed = 2f;
    public float MinDistance = 2f;
    public float MaxDistance = 10f;

    [Header("Vertical Limits")]
    public float MinY = -20f;
    public float MaxY = 80f;

    private float _currentX = 0f;
    private float _currentY = 30f;

    private void Update()
    {
        // Вращение по правой кнопке мыши
        if (Input.GetMouseButton(1))
        {
            _currentX += Input.GetAxis("Mouse X") * MouseSensitivity;
            _currentY -= Input.GetAxis("Mouse Y") * MouseSensitivity;
            _currentY = Mathf.Clamp(_currentY, MinY, MaxY);
        }

        // Зум колёсиком
        Distance -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);
    }

    private void LateUpdate()
    {
        if (Target == null) return;

        // Вычисляем позицию камеры
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0f);
        Vector3 offset = rotation * new Vector3(0f, 0f, -Distance);

        transform.position = Target.position + offset;
        transform.LookAt(Target.position + Vector3.up);
    }
}