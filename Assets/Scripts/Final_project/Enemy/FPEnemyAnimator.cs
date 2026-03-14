using UnityEngine;
using UnityEngine.AI;

public class FPEnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_animator == null || _agent == null) return;

        float speed = _agent.velocity.magnitude;
        _animator.SetFloat("Speed", speed, 0.05f, Time.deltaTime);
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.localPosition;
        pos.x = 0f;
        pos.z = 0f;
        transform.localPosition = pos;
    }
}