using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GridMovement))]
public class AIMovement : MonoBehaviour
{

    protected GridMovement grid;

    protected AudioSource honk;

    // Start is called before the first frame update
    void Start() {
        honk = GetComponent<AudioSource>();
        grid = gameObject.GetComponent<GridMovement>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.OnPlayerMove += AIMovement_OnPlayerMove;

        SubclassStart(); //calls the 
    }

    public virtual void AIMovement_OnPlayerMove(object sender, EventArgs e) { }

    //if the subclass needs to do something on Start(), put it here in the override
    public virtual void SubclassStart() {
        //is overidden in subclass
    }

    //turns the current gameObject into the player by adding some components and disabling AI
    public void GivePlayerControl() {
        //Give player control over the object
        gameObject.AddComponent<PlayerMovement>();

        //kill the old object
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Die();

        //Tag new object with Player
        gameObject.tag = "Player";

        //disable current AI script
        this.enabled = false;
    }

    public bool PlayerInProximity(float proximity) {
        //get Player's location
        GridMovement playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<GridMovement>();

        //get distance from player
        float difference = this.grid.DistanceFromOtherEntity(playerLocation);


        return difference < proximity;
    }
}
