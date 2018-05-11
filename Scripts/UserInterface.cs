using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

public class UserInterface : MonoBehaviour {
    private IUserAction action;

    void Start () {
        action = SceneController.getInstance() as IUserAction;
    }
	
	void Update () {
        inputDirection();
    }

    void inputDirection() {
        if (Input.GetKey(KeyCode.W)) {
            action.heroMove(Diretion.UP);
        }
        if (Input.GetKey(KeyCode.S)) {
            action.heroMove(Diretion.DOWN);
        }
        if (Input.GetKey(KeyCode.A)) {
            action.heroMove(Diretion.LEFT);
        }
        if (Input.GetKey(KeyCode.D)) {
            action.heroMove(Diretion.RIGHT);
        }
    }
}
