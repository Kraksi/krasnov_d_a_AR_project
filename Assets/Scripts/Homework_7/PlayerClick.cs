using UnityEngine;
using UnityEngine.AI;

public class PlayerClick : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log($"Кликнули на: {hit.point} объект: {hit.collider.name}");
            
            NavMeshHit navHit;
            // Проверяем есть ли NavMesh рядом с точкой клика
            if (NavMesh.SamplePosition(hit.point, out navHit, 2f, NavMesh.AllAreas))
            {
                _agent.SetDestination(navHit.position);
                Debug.Log($"Идём к: {navHit.position}");
            }
            else
            {
                Debug.Log("NavMesh не найден в этой точке!");
            }
        }
    }
}
}