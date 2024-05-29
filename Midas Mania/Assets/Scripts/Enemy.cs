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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
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

    void UpdatePatrolState()
    {
        // Implement patrol behavior here
        // ...

        // Check if the player is within the flashlight cone
        Vector3 dirToPlayer = playerTransform.position - transform.position;
        if (Vector3.Angle(dirToPlayer, transform.up) < viewAngle / 2 && dirToPlayer.magnitude < viewDistance)
        {
            currentState = CopState.ChasePlayer;
        }
    }

    void UpdateChasePlayerState()
    {
        // Set the player's position as the destination for the NavMeshAgent
        agent.SetDestination(playerTransform.position);

        // Calculate the direction to the player
        Vector3 dirToPlayer = playerTransform.position - transform.position;

        // Rotate the enemy to face the player (2D)
        float angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

        // Check if the player is out of the flashlight cone
        if (Vector3.Angle(dirToPlayer, transform.up) >= viewAngle / 2 || dirToPlayer.magnitude >= viewDistance)
        {
            // Don't transition to Investigate state if already chasing
            if (currentState != CopState.ChasePlayer)
            {
                currentState = CopState.Investigate;
            }
        }
        else
        {
            // If the player re-enters the flashlight cone during the chase, reset the state to ChasePlayer
            if (currentState == CopState.Investigate)
            {
                currentState = CopState.ChasePlayer;
            }
        }
    }
    void UpdateInvestigateState()
    {
        // Implement investigate behavior here
        // ...

        // Check if the player is back within the flashlight cone
        Vector3 dirToPlayer = playerTransform.position - transform.position;
        if (Vector3.Angle(dirToPlayer, transform.up) < viewAngle / 2 && dirToPlayer.magnitude < viewDistance)
        {
            currentState = CopState.ChasePlayer;
        }
    }
}