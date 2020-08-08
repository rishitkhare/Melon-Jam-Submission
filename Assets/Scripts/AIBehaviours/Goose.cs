using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
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
                grid.MoveY(1);
            } else if (direction == 1) {
                grid.MoveY(-1);
            } else if (direction == 2) {
                grid.MoveX(1);
            } else { // direction == 3
                grid.MoveX(-1);
            }
        }
        turnNumber++;

    }

    override
    public void SubclassStart() {
        honk = gameObject.GetComponent<AudioSource>();
    }
}