using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GridMovement : MonoBehaviour
{
    public int x;
    public int y;
    GridLayout gridLayout;
    GameObject wallTilemap;
    Collider2D wallCollider;
    GameManager manager;

    void Start() {
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        wallTilemap = GameObject.FindGameObjectWithTag("Wall");
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        wallCollider = manager.GetWallCollider();
    }

    void Update() {
        //takes the grid position and updates based on the parent grid object
        transform.position = gridLayout.CellToWorld(new Vector3Int(x, y, 0));
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
        if (IsValidMovement(x, y + amount)) {
            y += amount;
        }
    }

    public void MoveX(int amount) {
        if (IsValidMovement(x + amount, y)) {
            x += amount;
        }
    }

    //checks if current space is not occupied. Is only called from the Move() methods
    private bool IsValidMovement(int newX, int newY) {
        
        //check enemy collisions
        GameObject[] allEntities = manager.GetEnemyList();
        bool collided = CheckArrayCollision(allEntities, newX, newY);

        //check player collisions
        allEntities = manager.GetPlayerList();

        collided = collided || CheckArrayCollision(allEntities, newX, newY);

        //check wall collisions
        collided = collided || CheckWallCollision(gridLayout.CellToWorld(new Vector3Int(newX, newY, 0)));

        //if collided is true motion is not valid (and vice versa)
        return !collided;
    }

    //returns true if the given objects are colliding
    private bool CheckArrayCollision(GameObject[] allEntities, float newX, float newY) {
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

    private bool CheckWallCollision(Vector2 point) {
        //check if the given point is inside a wall
        Debug.DrawLine(transform.position, point);
        bool answer = wallCollider.OverlapPoint(point);
        Debug.Log(answer);
        return answer;
    }

    
}
