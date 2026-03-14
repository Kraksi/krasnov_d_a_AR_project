using UnityEngine;
using UnityEngine.AI;

public class FPPlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;

    [Header("Speed Thresholds")]
    public float WalkSpeed = 2f;
    public float RunSpeed = 4f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_animator == null || _agent == null) return;

        float targetSpeed = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            targetSpeed = _agent.speed;

        
        _animator.SetFloat("Speed", targetSpeed, 0.15f, Time.deltaTime);
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.localPosition;
        pos.x = 0f;
        pos.z = 0f;
        transform.localPosition = pos;
    }
}
