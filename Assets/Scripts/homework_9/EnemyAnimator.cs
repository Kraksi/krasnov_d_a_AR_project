using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponentInParent<NavMeshAgent>();
        
        Debug.Log($"Animator: {_animator}, Agent: {_agent}");
    }

    private void Update()
    {
        if (_animator == null || _agent == null) return;

        float speed = _agent.velocity.magnitude;
        Debug.Log($"Enemy speed: {speed}");
        _animator.SetFloat("Speed", speed, 0.05f, Time.deltaTime);
    }
}