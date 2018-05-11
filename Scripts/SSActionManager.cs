using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//并发顺序
public class SSActionManager : MonoBehaviour {

    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> add = new List<SSAction>();
    private List<int> delete = new List<int>();

    protected void Start () {
	
	}

    protected void Update() {
	    foreach (SSAction ac in add) {
            actions[ac.GetInstanceID()] = ac;
        }
        add.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions) {
            SSAction ac = kv.Value;
            if (ac.destroy)
                delete.Add(kv.Key);
            else if (ac.enable)
                ac.Update();
        }

        foreach (int key in delete) {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        delete.Clear();
    }

    public void runAction(GameObject gameObj, SSAction action, ISSActionCallback manager) {
        //先把该对象现有的动作销毁（与原来不同部分）
        for (int i = 0; i < add.Count; i++) {
            if (add[i].gameObject.Equals(gameObj)) {
                SSAction temp = add[i];
                add.RemoveAt(i);
                i--;
                DestroyObject(temp);
            }
        }
        foreach (KeyValuePair<int, SSAction> kv in actions) {
            SSAction ac = kv.Value;
            if (ac.gameObject.Equals(gameObj)) {
                ac.destroy = true;
            }
        }

        action.gameObject = gameObj;
        action.transform = gameObj.transform;
        action.callBack = manager;
        add.Add(action);
        action.Start();
    }
}
