using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool goldCollected { get; private set; }
    public static bool playerWin { get; private set; }
    [SerializeField] private GameObject Goal;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerWin = false;
        goldCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerCollectedGold() {
        goldCollected = true;
        Goal.GetComponent<Goal>().AllowPlayerToLeave();
        
    }
}
