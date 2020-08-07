using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goose : AIMovement {

    int turnNumber = 0;
    int direction;

    override
    public void AIMovement_OnPlayerMove(object sender, EventArgs e) {
        System.Random randomGenerator = new System.Random();
        if (turnNumber % 2 == 0) {
            //telegraph
            direction = randomGenerator.Next(4);
            Debug.Log(direction);
            honk.Play();
        }
        else {
            if (direction == 0) {
                MoveUp();
            } else if (direction == 1) {
                MoveDown();
            } else if (direction == 2) {
                MoveRight();
            } else { // direction == 3
                MoveLeft();
            }
        }
        turnNumber++;

    }
}