//using System.Numerics;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum CopState
{
    Patrol,
    ChasePlayer,
    Investigate
}

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform playerTransform; // Reference to the player's transform
    public float chaseSpeed = 3f; // Speed at which the cop chases the player
    public float viewDistance = 10f; // Distance at which the cop can see the player
    public float viewAngle = 45f; // Angle of the flashlight cone

    private CopState currentState = CopState.Patrol; // Initial state
    private NavMeshAgent agent; // Reference to the NavMeshAgent component

    private float investigationDuration = 2f; // Duration of the investigation phase

    public float range = 10f; // Radius of the patrol area
    public Transform centrePoint; // Centre of the patrol area

    private int knownLives;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        knownLives = GameManager.Instance.playerLives;
    }

    void Update()
    {
        //Debug.Log(knownLives);
        switch (currentState)
        {
            case CopState.Patrol:
                UpdatePatrolState();
                break;
            case CopState.ChasePlayer:
                UpdateChasePlayerState();
                break;
            case CopState.Investigate:
                UpdateInvestigateState();
                break;
        }
    }

    public void ChangeState(CopState newState)
    {
        currentState = newState;
    }

    void UpdatePatrolState()
    {
        //Debug.Log("updatepatrolstate");

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }

        if (agent.velocity != Vector3.zero)
        {
            float angle = Mathf.Atan2(agent.velocity.y, agent.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        }
    }

    void UpdateChasePlayerState()
    {
        if (knownLives > GameManager.Instance.playerLives) {
            knownLives = GameManager.Instance.playerLives;
            currentState = CopState.Patrol;
            return;
        }
        
        //Debug.Log("updatechaseplayerstate");
        agent.SetDestination(playerTransform.position);


        lastKnownPlayerPosition = playerTransform.position;


        Vector2 dirToPlayer = playerTransform.position - transform.position;

        float angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);

    }

    private Vector2 lastKnownPlayerPosition;
    private bool currentlyInvestigating = false;

    void UpdateInvestigateState()
    {
        //Debug.Log("updateinvestigatestate");
        InvestigateSound(lastKnownPlayerPosition);
        StartCoroutine(InvestigationPhase(investigationDuration));
        
    }

    IEnumerator InvestigationPhase(float investigationDuration) {
        if (!currentlyInvestigating) {
            currentlyInvestigating = true;
            yield return new WaitForSeconds(investigationDuration);
            currentState = CopState.Patrol;
            currentlyInvestigating = false;
        }
    }
    
    public void InvestigateSound(Vector2 soundPos) {
        
        if (currentState != CopState.Investigate) currentState = CopState.Investigate;

        lastKnownPlayerPosition = soundPos;
        centrePoint.position = lastKnownPlayerPosition; // Set the center point position to the last known player position

        agent.SetDestination(lastKnownPlayerPosition);

        Vector2 dirToLastKnownPosition = lastKnownPlayerPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(dirToLastKnownPosition.y, dirToLastKnownPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.Rotate(Vector3.forward * Time.deltaTime * 30f);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}