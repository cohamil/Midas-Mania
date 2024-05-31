using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Player") {
            Debug.Log("gold collected");
            GameManager.Instance.PlayerCollectedGold();
            gameObject.SetActive(false);
        }
    }
}
