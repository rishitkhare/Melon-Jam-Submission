using System;
using UnityEngine;

public class Generic : AIMovement
{
    /* 
     * -1 = none
     * 0 = up
     * 1 = down
     * 2 = right
     * 3 = left
     */
    public int moveSetting;

    void Update() {
        if(Input.GetKeyDown("space")) {
            Debug.Log(PlayerInProximity(2.0f));
        }
        if(Input.GetKeyDown("w")) {
            GivePlayerControl();
        }
    }

    override
    public void AIMovement_OnPlayerMove(object sender, EventArgs e) {
        if (moveSetting == 0) {
            grid.MoveUp();
        }
        else if (moveSetting == 1) {
            grid.MoveDown();
        }
        else if (moveSetting == 2) {
            grid.MoveRight();
        }
        else if (moveSetting == 3) {
            grid.MoveLeft();
        }
    }
}
