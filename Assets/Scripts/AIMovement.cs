using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIMovement : MonoBehaviour
{

    public int GridX = 2;
    public int GridY = 1;

    public AudioSource honk;

    // Start is called before the first frame update
    void Start() {
        honk = GetComponent<AudioSource>();
        GridX = (int)transform.position.x;
        GridY = (int)transform.position.y;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.OnPlayerMove += AIMovement_OnPlayerMove;
    }

    private void Update() {
        transform.position = new Vector2(GridX, GridY);
    }

    public virtual void AIMovement_OnPlayerMove(object sender, EventArgs e) {
    }

    public void MoveUp() {
        GridY++;
        }

    public void MoveDown() {
        GridY--;
        }

    public void MoveLeft() {
        GridX--;
        }

    public void MoveRight() {
        GridX++;
        }
    }
