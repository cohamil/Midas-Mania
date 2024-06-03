using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floorboard : MonoBehaviour
{
    [SerializeField] private GameObject noiseObj;
    [SerializeField] private float noiseLengthInSeconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            StartCoroutine(GenerateNoise());
        }
    }

    IEnumerator GenerateNoise() {
        noiseObj.SetActive(true);
        yield return new WaitForSeconds(noiseLengthInSeconds);
        noiseObj.SetActive(false);
    }
}
