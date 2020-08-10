using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    GameObject[] entities;
    GameObject[] dead;
    GameObject player;
    GridMovement playerLocation;
    GameObject winTrigger;
    public GameObject wall;
    public GameObject water;
    Collider2D wallCollider;
    Collider2D waterCollider;
    Collider2D winCollider;

    public int playerLivesLeft;

    //Event for each turn

    void Start() {
        playerLivesLeft = 5;
        player = GameObject.FindGameObjectWithTag("Player");
        winTrigger = GameObject.FindGameObjectWithTag("WinTrigger");
        winCollider = winTrigger.GetComponent<Collider2D>();
        playerLocation = player.GetComponent<GridMovement>();
    }

    // Start is called before first frame update
    void Awake() {
        wallCollider = wall.GetComponent<Collider2D>();
        waterCollider = water.GetComponent<Collider2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown("r")) {
            this.PlayerLose();
        }
        if(Input.GetKeyDown("s"))
        {
            this.PlayerWin();
        }
    }

    //TODO: Add screen transitions
    public void PlayerLose() {
        //reload current scene
        Debug.Log("Aw... Lose!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerWin() {
        //load next scene
        Debug.Log("That's a win!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DecrementLivesLeft() {
        GetPlayer();
        playerLivesLeft--;
        if(playerLivesLeft < 1) {
            if(PlayerTouchingWinTrigger()) {
                PlayerWin();
            }
            else {
                PlayerLose();
            }
        }
    }

    public bool PlayerTouchingWinTrigger() {
        return winCollider.OverlapPoint(playerLocation.GetWorldMidPointTransform());
    }

    public void SetPlayerLives(int reset) {
        playerLivesLeft = reset;
    }

    public GameObject[] GetEnemyList() {
        entities = GameObject.FindGameObjectsWithTag("Enemy");

        return entities;
    }

    public GameObject[] GetDeadList()
    {
        dead = GameObject.FindGameObjectsWithTag("Dead");

        return dead;
    }

    public GameObject GetPlayer() {


        return player;
    }

    public void UpdatePlayerReference() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLocation = player.GetComponent<GridMovement>();
    }

    public Collider2D GetWallCollider() {
        return wallCollider;
    }

    public Collider2D GetWaterCollider() {
        return waterCollider;
    }
}