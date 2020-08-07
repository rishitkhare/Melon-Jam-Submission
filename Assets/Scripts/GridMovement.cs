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

    public void MoveUp() {
        y++;
    }

    public void MoveDown() {
        y--;
    }

    public void MoveRight() {
        x++;
    }

    public void MoveLeft() {
        x--;
    }

}
