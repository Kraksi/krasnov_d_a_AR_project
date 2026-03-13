using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform Player;
    public float UpdateRate = 0.2f;

    private NavMeshAgent _agent;
    private float _timer;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= UpdateRate)
        {
            _timer = 0f;

            if (Player != null)
                _agent.SetDestination(Player.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<PlayerClick>() != null)
        {
            GameManager.Instance.GameOver();
        }
    }

}