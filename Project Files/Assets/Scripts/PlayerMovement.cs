using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GridMovement))]
[RequireComponent(typeof(EntityTag))]
public class PlayerMovement : MonoBehaviour {
    GridMovement gridMovement;
    AnimateGridMovement gridAnimator;
    GameManager manager;
    GameObject SoundSystem;
    BGMusic speaker;
    EntityTag entityTag;
    bool isDead;

    //only used if Player is procrastinator
    private Vector2Int procrastinatorStoredMove;

    // Start is called before the first frame update
    void Awake() {
        procrastinatorStoredMove = Vector2Int.zero;
        gridMovement = gameObject.GetComponent<GridMovement>();
        gridAnimator = gameObject.GetComponent<AnimateGridMovement>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        entityTag = gameObject.GetComponent<EntityTag>();
        SoundSystem = GameObject.FindGameObjectWithTag("SoundSystem");
        speaker = SoundSystem.GetComponent<BGMusic>();
    }


    // Update is called once per frame
    void Update() {

        //does not take input until previous motion is finished
        if (GetNumberOfDirectionKeysPressed() == 1 && !gridAnimator.GetIsAnimate()) {
            
            if(manager.playerLivesLeft == 1 && Input.GetKeyDown("space")) {
                Sneeze();
            }
            else {
                Move();
            }
        }
            
    }

    public bool GetIsDead() {
        return isDead;
    }

    public void SetIsDead(bool value) {
        isDead = value;
    }

    public void Die() {
        gridAnimator.DeathAnimation();
        this.isDead = true;
        this.enabled = false;
        //Destroy(gameObject);
    }

    public void Sneeze() {
        Debug.Log("Achoo!");
        int intensity = 2;

        if (entityTag.HasTag("Spitter")) {
            intensity = 4;
        }

        Vector2Int sneeze = new Vector2Int(gridMovement.x, gridMovement.y);

        for(int i = 0; i < intensity; i ++) {
            sneeze += ConvertToVectorInt(gridMovement.direction);
            gridMovement.SneezeOnBlock(sneeze);
        }

        //
        if (manager.playerLivesLeft == 1) {
            manager.PlayerLose();
        }
    }

    private void Move() {
        //player moves differently depending on host
        if (entityTag.HasTag("Skateboarder")) {
            SlideWalk();
            gridAnimator.AddToAnimateQueue(gridMovement.GetPositionVector2Int());
        }
        else if(entityTag.HasTag("Procrastinator")) {
            ProcrastinateWalk();
        }
        else {
            SimpleWalk();
            gridAnimator.AddToAnimateQueue(gridMovement.GetPositionVector2Int());
        }
    }

    private void SlideWalk() {
        Vector2Int beforeMove = new Vector2Int(gridMovement.x, gridMovement.y);

        Vector2 slideDirection = InputToDirection();
        Debug.Log(string.Format("X: {0} Y: {1}", beforeMove.x, beforeMove.y));

        //uses the position before moving to determine whether or not object has hit a wall.
        //it will then stop sliding.
        Vector2Int oldPosition = new Vector2Int(gridMovement.x, gridMovement.y);
        MoveInDirection(slideDirection);
        int counter = 0;
        bool hasCollided = false;
        while (! (oldPosition.Equals(new Vector2Int(gridMovement.x, gridMovement.y)))) {
            oldPosition = new Vector2Int(gridMovement.x, gridMovement.y);
            hasCollided = gridMovement.isEnemyHere(oldPosition + ConvertToVectorInt(slideDirection));
            MoveInDirection(slideDirection);

            

            //avoids infinite loop
            counter++;
            if(counter > 50) {
                Debug.Log("INFINITE!!!");
                return;
            }
        }

        //if you moved, host loses health
        Debug.Log(hasCollided);
        if ((!beforeMove.Equals(gridMovement.GetPositionVector2Int()))) {
            if(!(hasCollided)) {
                manager.DecrementLivesLeft();
                //sets direction
                gridMovement.direction = InputToDirection();

                gridAnimator.TriggerWalk();
            }
            else {
                if(!isDead) {
                    manager.DecrementLivesLeft();
                }
            }
        }
    }

    private void ProcrastinateWalk() {
        //if this is not a second move
        if(procrastinatorStoredMove.Equals(Vector2Int.zero)) {
            //set the move if valid
            procrastinatorStoredMove = ConvertToVectorInt(InputToDirection());
            Vector2Int nextPosition = gridMovement.GetPositionVector2Int() + procrastinatorStoredMove;
            if(!gridMovement.IsValidMovement(nextPosition.x, nextPosition.y, false)) {
                procrastinatorStoredMove = Vector2Int.zero;
            }
        }
        else {
            //check if the 2nd move is valid
            Vector2Int secondMove = ConvertToVectorInt(InputToDirection());
            Vector2Int nextPosition = gridMovement.GetPositionVector2Int() + procrastinatorStoredMove + secondMove;

            bool checkValid = gridMovement.IsValidMovement(nextPosition.x, nextPosition.y, manager.playerLivesLeft == 1);

            if (!checkValid) {
                procrastinatorStoredMove = Vector2Int.zero;
                return;
            }

            //sets direction
            gridMovement.direction = InputToDirection();
            gridAnimator.TriggerWalk();

            //execute previous move and next move
            MoveInDirection(procrastinatorStoredMove);
            gridAnimator.AddToAnimateQueue(gridMovement.GetPositionVector2Int());
            MoveInDirection(secondMove);
            gridAnimator.AddToAnimateQueue(gridMovement.GetPositionVector2Int());

            procrastinatorStoredMove = Vector2Int.zero;

            manager.DecrementLivesLeft();
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
        Vector2Int oldPosition = new Vector2Int(gridMovement.x, gridMovement.y);

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

        //if you moved, host loses health
        if (!oldPosition.Equals(gridMovement.GetPositionVector2Int())) {
            //sets direction
            gridMovement.direction = InputToDirection();
            gridAnimator.TriggerWalk();

            manager.DecrementLivesLeft();
        }
    }

    private int GetNumberOfDirectionKeysPressed() {
        return Convert.ToInt32(Input.GetKeyDown("up")) + Convert.ToInt32(Input.GetKeyDown("down"))
             + Convert.ToInt32(Input.GetKeyDown("left")) + Convert.ToInt32(Input.GetKeyDown("right"))
             + Convert.ToInt32(Input.GetKeyDown("space"));
    }
}
