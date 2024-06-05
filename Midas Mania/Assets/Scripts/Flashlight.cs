using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;  // Enemy the flashlight is attached to
    private Enemy enemyScript;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = Enemy.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Enemy.transform.position, transform.position + transform.up);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            hit = Physics2D.Raycast(Enemy.transform.position, transform.up, enemyScript.viewDistance);
            if (hit.collider != null)
            {
                //Debug.Log("raycasted something: " + hit.collider.tag);
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("player detected");
                    enemyScript.ChangeState(CopState.ChasePlayer);
                }
            }
            else
            {
                Debug.Log("player detected2");
                enemyScript.ChangeState(CopState.ChasePlayer);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            enemyScript.ChangeState(CopState.Patrol); // should be investiage state, but patrol for now
        }
    }
}
