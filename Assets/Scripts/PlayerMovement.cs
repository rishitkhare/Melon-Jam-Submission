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

    //private Vector2 direction;

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
            if(manager.playerLivesLeft == 1 && Input.GetKeyDown("space")) {
                Sneeze();
            }
            else {
                Move();
            }

            //if you moved, host loses health
            if (!oldPosition.Equals(gridMovement.GetPositionVector2Int())) {
                manager.DecrementLivesLeft();
            }
        }
            
    }

    public void Die() {
        this.enabled = false;
        //Destroy(gameObject);
    }

    public void Sneeze() {
        Debug.Log("Achoo!");
        int intensity = 2;

        if (entityTag.HasTag("Spitter")) {
            intensity = 4;
        }

        Vector2Int sneezeOrigin = new Vector2Int(gridMovement.x, gridMovement.y);

        for(int i = 0; i < intensity; i ++) {
            MoveInDirection(gridMovement.direction);

            if(manager.PlayerTouchingWinTrigger()) {
                //move player back
                gridMovement.x = sneezeOrigin.x;
                gridMovement.y = sneezeOrigin.y;

                //end game
                manager.PlayerWin();
                return;
            }
        }

        gridMovement.x = sneezeOrigin.x;
        gridMovement.y = sneezeOrigin.y;

        //
        if (manager.playerLivesLeft == 1) {
            manager.PlayerLose();
        }
    }

    private void Move() {
        //sets direction
        gridMovement.direction = InputToDirection();

        //player moves differently depending on host
        if (entityTag.HasTag("Skateboarder")) {
            SlideWalk();
        }
        else if(entityTag.HasTag("Procrastinator")) {
            //TODO: add Procrastinator script
        }
        else {
            SimpleWalk();
        }
    }

    private void SlideWalk() {
        Vector2 slideDirection = InputToDirection();

        //uses the position before moving to determine whether or not object has hit a wall.
        //it will then stop sliding.
        Vector2Int oldPosition = new Vector2Int(gridMovement.x, gridMovement.y);
        MoveInDirection(slideDirection);
        int counter = 0;
        while (!oldPosition.Equals(new Vector2Int(gridMovement.x, gridMovement.y))) {
            MoveInDirection(slideDirection);

            //avoids infinite loop
            counter++;
            if(counter > 50) {
                return;
            }
        }
    }

    private Vector2Int ConvertToVectorInt(Vector2 vector2) {
        return (new Vector2Int((int) vector2.x, (int) vector2.y));
    }

    private Vector2 InputToDirection() {
        if (Input.GetKeyDown("up")) {
            return Vector2.up;
        }
        if (Input.GetKeyDown("down")) {
            return Vector2.down;
        }
        if (Input.GetKeyDown("left")) {
            return Vector2.left;
        }
        if (Input.GetKeyDown("right")) {
            return Vector2.right;
        }

        return Vector2.zero;
    }

    private void MoveInDirection(Vector2 direction) {
        if(direction.Equals(Vector2.up)) {
            gridMovement.MoveY(1);
        }
        else if (direction.Equals(Vector2.down)) {
            gridMovement.MoveY(-1);
        }
        else if (direction.Equals(Vector2.left)) {
            gridMovement.MoveX(-1);
        }
        else if (direction.Equals(Vector2.right)) {
            gridMovement.MoveX(1);
        }
        else {
            throw new System.ArgumentException("Non-cardinal direction:");
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
             + Convert.ToInt32(Input.GetKeyDown("left")) + Convert.ToInt32(Input.GetKeyDown("right"))
             + Convert.ToInt32(Input.GetKeyDown("space"));
    }
}
