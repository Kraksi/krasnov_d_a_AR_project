using UnityEngine;
using UnityEngine.AI;

public class FPPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed = 5f;
    public float RotateSpeed = 90f;

    [Header("Acceleration")]
    public float AccelerationSpeed = 10f;  
    public float DecelerationSpeed = 20f; 

    private NavMeshAgent _agent;
    private float _currentSpeed = 0f;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = MoveSpeed;
        _agent.angularSpeed = 0f;
        _agent.acceleration = 999f;
        _agent.autoBraking = false;
    }

    private void Update()
    {
        
        float rotateInput = 0f;
        if (Input.GetKey(KeyCode.A)) rotateInput = -1f;
        if (Input.GetKey(KeyCode.D)) rotateInput = 1f;
        transform.Rotate(Vector3.up, rotateInput * RotateSpeed * Time.deltaTime);

        
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.W)) moveInput = 1f;
        if (Input.GetKey(KeyCode.S)) moveInput = -1f;

        float targetSpeed = moveInput * MoveSpeed;

        
        float rate = Mathf.Abs(targetSpeed) > Mathf.Abs(_currentSpeed)
            ? AccelerationSpeed  
            : DecelerationSpeed; 

        _currentSpeed = Mathf.MoveTowards(
            _currentSpeed, targetSpeed, rate * Time.deltaTime);

        if (Mathf.Abs(_currentSpeed) > 0.01f)
            _agent.Move(transform.forward * _currentSpeed * Time.deltaTime);
        else
            _agent.velocity = Vector3.zero;
    }
}