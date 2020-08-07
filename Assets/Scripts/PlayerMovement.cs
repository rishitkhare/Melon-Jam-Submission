using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GridMovement))]
public class PlayerMovement : MonoBehaviour {
    GridMovement gridMovement;


    public event EventHandler OnPlayerMove;

    // Start is called before the first frame update
    void Start() {
        gridMovement = gameObject.GetComponent<GridMovement>();
    }

    // Update is called once per frame
    void Update() {

        if (GetNumberOfDirectionKeysPressed() == 1) {
            Move();
            OnPlayerMove?.Invoke(this, EventArgs.Empty); //broadcasts 'turn' to all AI for their motions
        }
            
    }

    public void Die() {
        Destroy(gameObject);
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
        gridMovement.y++;    
    }

    private void MoveDown() {
        gridMovement.y--;
    }

    private void MoveLeft() {
        gridMovement.x--;
    }

    private void MoveRight() {
        gridMovement.x++;
    }

    private int GetNumberOfDirectionKeysPressed() {
        return Convert.ToInt32(Input.GetKeyDown("up")) + Convert.ToInt32(Input.GetKeyDown("down"))
             + Convert.ToInt32(Input.GetKeyDown("left")) + Convert.ToInt32(Input.GetKeyDown("right"));
    }
}
