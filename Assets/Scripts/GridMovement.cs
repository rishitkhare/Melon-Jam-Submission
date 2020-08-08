using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    GameObject gridObject;
    public int x;
    public int y;

    void Start() {
        gridObject = GameObject.FindGameObjectWithTag("Grid");
        x = (int)transform.position.x;
        y = (int)transform.position.y;
    }

    void Update() {
        transform.position = new Vector2(x, y);
    }

    //implements Pythagorean theorem to calculate positive distance between two grid entities
    public float DistanceFromOtherEntity(GridMovement other) {
        float Xdiff = Mathf.Abs(other.x - this.x);
        float Ydiff = Mathf.Abs(other.y - this.y);

        //a^2 + b^2 = c^2
        //c = Sqrt(a^2 + b^2)
        return Mathf.Sqrt(Mathf.Pow(Xdiff, 2) + Mathf.Pow(Ydiff, 2));
    }

    //overload of above method but takes GameObject parameter and gets the component for user
    public float DistanceFromOtherEntity(GameObject other) {
        if(other.GetComponent<GridMovement>() != null) {
            return DistanceFromOtherEntity(other.GetComponent<GridMovement>());
        }
        else {
            throw new System.NullReferenceException(other.name + " does not contain GridMovement component");
        }
    }

    public void MoveY(int amount) {
        //Debug.Log(isValidMovement(x, y + amount));
        if (isValidMovement(x, y + amount)) {
            y += amount;
        }
    }

    public void MoveX(int amount) {
        if (isValidMovement(x + amount, y)) {
            x += amount;
        }
    }

    //checks if current space is not occupied. Is only called from the Move() methods
    private bool isValidMovement(int newX, int newY) {

        //check enemy collisions
        GameObject[] allEntities = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GetEnemyList();
        bool collided = checkArrayCollision(allEntities, newX, newY);
        //DebugGOList(allEntities);
        if(gameObject.tag.Equals("Enemy")){
            Debug.Log(string.Format("{0} EnemyCollisionCheck: {1}", gameObject.name, collided));
        }

        //check player collisions
        allEntities = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GetPlayerList();

        if (gameObject.tag.Equals("Enemy")) {
            Debug.Log(string.Format("{0} PlayerCollisionCheck {1}", gameObject.name, checkArrayCollision(allEntities, newX, newY)));
        }
        collided = collided || checkArrayCollision(allEntities, newX, newY);


        //if collided is true, return false, if 
        return !collided;
    }

    //returns true if the given objects are colliding
    private bool checkArrayCollision(GameObject[] allEntities, float newX, float newY) {
        foreach (GameObject entity in allEntities) {
            if(entity != gameObject) {
                GridMovement other = entity.GetComponent<GridMovement>();

                if (other.x == newX && other.y == newY) {
                    return true;
                }
            }
        }

        return false;
    }

    private void DebugGOList(GameObject[] debugList){
        Debug.Log("Debug List:");
        foreach(GameObject thing in debugList) {
            Debug.Log(string.Format("{0} scans {1} at X:{2} Y:{3}", gameObject.name, thing.name, thing.GetComponent<GridMovement>().x, thing.GetComponent<GridMovement>().y));
;        }
    }

    
}
