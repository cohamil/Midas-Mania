using UnityEngine;

public enum CopState
{
    Patrol,
    ChasePlayer,
    Investigate
}

public class CopController : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float chaseSpeed = 3f; // Speed at which the cop chases the player
    public float viewDistance = 10f; // Distance at which the cop can see the player
    public float viewAngle = 45f; // Angle of the flashlight cone

    private CopState currentState = CopState.Patrol; // Initial state
    private UnityEngine.AI.NavMeshAgent agent; // Reference to the NavMeshAgent component

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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

        // Check if the player is out of the flashlight cone
        Vector3 dirToPlayer = playerTransform.position - transform.position;
        if (Vector3.Angle(dirToPlayer, transform.up) >= viewAngle / 2 || dirToPlayer.magnitude >= viewDistance)
        {
            currentState = CopState.Investigate;
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