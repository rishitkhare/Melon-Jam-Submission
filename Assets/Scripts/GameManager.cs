using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    GameObject[] entities;
    GameObject[] player;
    public GameObject wall;
    Collider2D wallCollider;

    // Start is called before first frame update
    void Awake() {
        wallCollider = wall.GetComponent<Collider2D>();
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
}