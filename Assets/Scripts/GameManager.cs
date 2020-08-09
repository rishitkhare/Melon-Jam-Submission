using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    GameObject[] entities;
    GameObject[] player;
    public GameObject wall;
    public GameObject water;
    Collider2D wallCollider;
    Collider2D waterCollider;

    public int playerLivesLeft;

    //Event for each turn

    void Start() {
        playerLivesLeft = 5;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Start is called before first frame update
    void Awake() {
        wallCollider = wall.GetComponent<Collider2D>();
        waterCollider = water.GetComponent<Collider2D>();
    }

    public void DecrementLivesLeft() {
        playerLivesLeft--;
        if(playerLivesLeft < 1) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SetPlayerLives(int reset) {
        playerLivesLeft = reset;
    }

    public GameObject[] GetEnemyList() {
        entities = GameObject.FindGameObjectsWithTag("Enemy");

        return entities;
    }

    public GameObject[] GetPlayerList() {
        player = GameObject.FindGameObjectsWithTag("Player");

        return player;
    }

    public Collider2D GetWallCollider() {
        return wallCollider;
    }

    public Collider2D GetWaterCollider() {
        return waterCollider;
    }
}