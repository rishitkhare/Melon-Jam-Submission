using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject[] entities;
    public GameObject[] player;

    // Start is called before first frame update
    public GameObject[] GetEnemyList() {
        entities = GameObject.FindGameObjectsWithTag("Enemy");

        return entities;
    }

    public GameObject[] GetPlayerList() {
        player = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(player[0].name);

        return player;
    }
}