using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GridMovement))]
[RequireComponent(typeof(EntityTag))]
public class PlayerMovement : MonoBehaviour {
    GridMovement gridMovement;
    GameManager manager;
    EntityTag entityTag;

    // Start is called before the first frame update
    void Start() {
        gridMovement = gameObject.GetComponent<GridMovement>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        entityTag = gameObject.GetComponent<EntityTag>();
    }

    // Update is called once per frame
    void Update() {

        if (GetNumberOfDirectionKeysPressed() == 1) {
            Vector2Int oldPosition = new Vector2Int(gridMovement.x, gridMovement.y);
            Move();

            //if you moved, host loses health
            if(!oldPosition.Equals(gridMovement.getPositionVector2Int())) {
                manager.DecrementLivesLeft();
            }
        }
            
    }

    public void Die() {
        this.enabled = false;
        //Destroy(gameObject);
    }


    private void Move() {
        if(entityTag.HasTag("Skateboarder")) {
            //TODO: add Skateboarder script
        }
        else if(entityTag.HasTag("Procrastinator")) {
            //TODO: add Procrastinator script
        }
        else {
            SimpleWalk();
        }
    }

    private void SimpleWalk() {
        if (Input.GetKeyDown("up")) {
            gridMovement.MoveY(1);
        }
        if (Input.GetKeyDown("down")) {
            gridMovement.MoveY(-1);
        }
        if (Input.GetKeyDown("left")) {
            gridMovement.MoveX(-1);
        }
        if (Input.GetKeyDown("right")) {
            gridMovement.MoveX(1);    
        }
    }

    private int GetNumberOfDirectionKeysPressed() {
        return Convert.ToInt32(Input.GetKeyDown("up")) + Convert.ToInt32(Input.GetKeyDown("down"))
             + Convert.ToInt32(Input.GetKeyDown("left")) + Convert.ToInt32(Input.GetKeyDown("right"));
    }
}
