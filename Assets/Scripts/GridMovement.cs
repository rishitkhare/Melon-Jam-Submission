using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityTag))]
public class GridMovement : MonoBehaviour
{
    public int x;
    public int y;
    GridLayout gridLayout;

    Collider2D wallCollider;
    Collider2D waterCollider;
    

    GameManager manager;
    EntityTag entityTag;

    void Start() {
        //position is set to nearest gridspace
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //initializing components
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        wallCollider = manager.GetWallCollider();
        waterCollider = manager.GetWaterCollider();
        entityTag = gameObject.GetComponent<EntityTag>();
    }

    void Update() {
        //takes the grid position and updates based on the parent grid object
        transform.position = gridLayout.CellToWorld(new Vector3Int(x, y, 0));
    }

    public Vector2Int getPositionVector2Int() {
        return new Vector2Int(this.x, this.y);
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

    //checks if a grid space is not occupied. Ignores the gameObject it is called from
    //checks three things: enemy collision, player collision, and wall collision.
    public bool IsValidMovement(int newX, int newY) {
        
        //check enemy collisions
        GameObject[] allEntities = manager.GetEnemyList();
        GameObject collider = CheckArrayCollision(allEntities, newX, newY);
        bool collided = (collider != null);

        //if collides with Enemy, shift hosts
        if(collider != null) {
            if(manager.playerLivesLeft == 1) {
                collider.GetComponent<EntityTag>().GivePlayerControl();
            }
            return false;
        }

        //check player collisions
        allEntities = manager.GetPlayerList();

        if(CheckArrayCollision(allEntities, newX, newY) != null) {
            return false;
        }

        //this is the point that the object is trying to move to
        Vector2 newWorldPoint = MidpointOfCell(gridLayout.CellToWorld(new Vector3Int(newX, newY, 0)));

        //check wall collisions
        if(CheckWallCollision(newWorldPoint, wallCollider)) {
            return false;
        }

        //check water collisions (if not a goose)
        if((!entityTag.HasTag("Goose")) && CheckWallCollision(newWorldPoint, waterCollider)) {
            return false;
        }

        //nothing has collided, movement is vaild
        return true;
    }

    //returns true if the given objects are colliding
    private GameObject CheckArrayCollision(GameObject[] allEntities, float newX, float newY) {
        foreach (GameObject entity in allEntities) {
            if(entity != gameObject) {
                GridMovement other = entity.GetComponent<GridMovement>();

                if (other.x == newX && other.y == newY) {
                    return entity;
                }
            }
        }

        return null;
    }

    private bool CheckWallCollision(Vector2 point, Collider2D collidable) {
        //check if the given point is inside a wall
        bool answer = collidable.OverlapPoint(point);
        return answer;
    }

    private Vector3 MidpointOfCell(Vector3 gridSpace) {
        return new Vector3(gridSpace.x + (gridLayout.cellSize.x/2f), gridSpace.y + (gridLayout.cellSize.y/2f), 0);
    }
}
