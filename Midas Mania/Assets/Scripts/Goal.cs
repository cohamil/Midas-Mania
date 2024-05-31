using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Material initialMaterial;
    [SerializeField] private Material successMaterial;
    private Renderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = initialMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AllowPlayerToLeave() {
        rend.material = successMaterial;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (GameManager.goldCollected && other.transform.tag == "Player") {
            Debug.Log("win");
        }
    }
}
