using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {
    GameObject Grid;
    int GridX;
    int GridY;

    public event EventHandler OnPlayerMove;

    // Start is called before the first frame update
    void Start() {
        Grid = GameObject.FindGameObjectWithTag("Grid");
    }

    // Update is called once per frame
    void Update() {

        if (GetNumberOfDirectionKeysPressed() == 1) {
            Move();
            OnPlayerMove?.Invoke(this, EventArgs.Empty); //broadcasts 'turn' to all AI for their motions
        }
        transform.position = new Vector3(GridX, GridY);
            
    }

    private void Move() {
        if (Input.GetKeyDown("up")) {
            MoveUp();
        }
        if (Input.GetKeyDown("down")) {
            MoveDown();
        }
        if (Input.GetKeyDown("left")) {
            MoveLeft();
        }
        if (Input.GetKeyDown("right")) {
            MoveRight();    
        }
    }

    private void MoveUp() {
        GridY++;    
    }

    private void MoveDown() {
        GridY--;
    }

    private void MoveLeft() {
        GridX--;
    }

    private void MoveRight() {
        GridX++;
    }

    private int GetNumberOfDirectionKeysPressed() {
        return Convert.ToInt32(Input.GetKeyDown("up")) + Convert.ToInt32(Input.GetKeyDown("down"))
             + Convert.ToInt32(Input.GetKeyDown("left")) + Convert.ToInt32(Input.GetKeyDown("right"));
    }
}
