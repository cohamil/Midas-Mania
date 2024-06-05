using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credit: https://www.youtube.com/watch?v=j1-OyLo77ss


public class FieldOfView : MonoBehaviour
{
    public float radius;    // how far the object can "see"
    [Range(0,360)] public float angle;     // how wide the object can "see"
    public GameObject playerRef;    // the "player" that this object is looking for
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine(){
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true){
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck(){
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask); // target mask is just a layer mask the player resides in
        if (rangeChecks.Length > 0){
            Transform target = rangeChecks[0].transform;    // only the player could be detected on the player layer so only first element is needed
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, directionToTarget) < angle/2){
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)){
                    canSeePlayer = true;
                }else{
                    canSeePlayer = false;
                }
            }else{
                canSeePlayer = false;
            }
        }else if (canSeePlayer){
            canSeePlayer = false;
        }
    }
}