using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Floorboard : MonoBehaviour
{
    [SerializeField] private GameObject noiseObj;
    [SerializeField] private float noiseLengthInSeconds;
    [SerializeField] private float growFactor;
    [SerializeField] private float holdDelay;
    private bool isActive = false;

    private Vector3 origNoiseScale;
    // Start is called before the first frame update
    void Start()
    {
        origNoiseScale = noiseObj.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !isActive) {
            StartCoroutine(GenerateNoise());
            StartCoroutine(StartDelay());
        }
    }

    IEnumerator GenerateNoise() {
        noiseObj.SetActive(true);
        isActive = true;
        for (int i = 0; i < 20; i++) {
            noiseObj.transform.localScale += new Vector3(1, 1, 0) * growFactor;
            yield return new WaitForSeconds(noiseLengthInSeconds / 75);
        }
        
        yield return new WaitForSeconds(noiseLengthInSeconds);
        noiseObj.SetActive(false);
        noiseObj.transform.localScale = origNoiseScale;
    }
    IEnumerator StartDelay() {
        yield return new WaitForSeconds(holdDelay);
        isActive = false;
    }
}
