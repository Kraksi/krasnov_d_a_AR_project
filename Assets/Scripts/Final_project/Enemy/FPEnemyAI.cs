using UnityEngine;
using UnityEngine.AI;

public class FPEnemyAI : MonoBehaviour
{
    public Transform Player;
    public float UpdateRate = 0.2f;
    public float BaseSpeed = 3.5f;

    [Header("Smart AI")]
    public float FlankAngle = 90f;
    public float PersonalSpace = 3f;
    public float PredictionTime = 0.5f;

    private NavMeshAgent _agent;
    private float _timer;
    private float _flankOffset;
    private int _enemyIndex = 0;

    private static int _enemyCount = 0;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = BaseSpeed;

        _enemyIndex = _enemyCount;
        _enemyCount++;

        
        _flankOffset = (_enemyIndex * 360f / Mathf.Max(1, _enemyCount)) +
                       Random.Range(-20f, 20f);
    }

    private void OnDestroy()
    {
        _enemyCount--;
    }

    public void SetSpeed(float speed)
    {
        if (_agent != null)
            _agent.speed = speed;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= UpdateRate)
        {
            _timer = 0f;
            if (Player != null)
                UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        NavMeshAgent playerAgent = Player.GetComponent<NavMeshAgent>();
        Vector3 predictedPos = Player.position;

        if (playerAgent != null && playerAgent.velocity.magnitude > 0.1f)
            predictedPos += playerAgent.velocity * PredictionTime;

        float distToPlayer = Vector3.Distance(transform.position, Player.position);

        if (distToPlayer > PersonalSpace * 1.5f)
        {
            
            Vector3 dirToPlayer = (predictedPos - transform.position).normalized;
            Vector3 flankDir = Quaternion.Euler(0, _flankOffset, 0) * dirToPlayer;
            Vector3 targetPos = predictedPos - flankDir * 0.5f;
            targetPos = AvoidOtherEnemies(targetPos);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPos, out hit, 2f, NavMesh.AllAreas))
                _agent.SetDestination(hit.position);
            else
                _agent.SetDestination(predictedPos);
        }
        else
        {
            
            _agent.SetDestination(Player.position);
        }
    }

    private Vector3 GetUniqueAttackPosition()
    {
        FPEnemyAI[] allEnemies = FindObjectsOfType<FPEnemyAI>();
        float angle = (_enemyIndex * 360f / Mathf.Max(1, allEnemies.Length)) + 180f;

        Vector3 offset = Quaternion.Euler(0, angle, 0) * Vector3.forward * PersonalSpace;
        Vector3 attackPos = Player.position + offset;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(attackPos, out hit, 3f, NavMesh.AllAreas))
            return hit.position;

        return Player.position;
    }

    private Vector3 AvoidOtherEnemies(Vector3 targetPos)
    {
        FPEnemyAI[] enemies = FindObjectsOfType<FPEnemyAI>();
        Vector3 avoidance = Vector3.zero;
        int count = 0;

        foreach (FPEnemyAI other in enemies)
        {
            if (other == this) continue;

            float dist = Vector3.Distance(transform.position, other.transform.position);
            if (dist < PersonalSpace * 2f)
            {
                Vector3 away = transform.position - other.transform.position;
                float strength = (PersonalSpace * 2f - dist) / (PersonalSpace * 2f);
                avoidance += away.normalized * strength * 3f;
                count++;
            }
        }

        if (count > 0)
            avoidance /= count;

        return targetPos + avoidance;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Enemy столкнулся с: {other.gameObject.name}");
        if (other.GetComponent<FPPlayerController>() != null)
            FPGameManager.Instance.GameOver();
    }
}