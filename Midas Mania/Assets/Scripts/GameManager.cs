using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool goldCollected { get; private set; }
    public static bool playerWin { get; private set; }
    [SerializeField] private GameObject player;
    [SerializeField] public int playerLives = 3;
    [SerializeField] private GameObject Goal;
    [SerializeField] private GameObject Gold;

    private Vector2 playerStartPos;

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
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
        }
        playerStartPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerCollectedGold() {
        goldCollected = true;
        Goal.GetComponent<Goal>().AllowPlayerToLeave();
        Gold.SetActive(false);

    }

    public void PlayerLostLife() {
        playerLives -= 1;
        if (playerLives <= 0) {
            PlayerDied();
        }
        else
        {
            if (goldCollected) goldCollected = false;
            Gold.SetActive(true);
            player.transform.position = playerStartPos;
        }
    }

    private void PlayerDied() {
        Debug.Log("dead");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
