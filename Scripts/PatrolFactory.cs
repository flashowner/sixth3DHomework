using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

namespace Com.Patrols {
    public class PatrolFactory : System.Object {
        private static PatrolFactory _instance;
        private GameObject player;

        private Vector3[] position = new Vector3[] { new Vector3(-8, 0, 14), new Vector3(-2, 0, 14),
            new Vector3(4, 0, 14), new Vector3(5, 0, 5), new Vector3(-2, 0, 5), new Vector3(-8, 0, 5)};

        public static PatrolFactory getInstance() {
            if (_instance == null)
                _instance = new PatrolFactory();
            return _instance;
        }

        public void initItem(GameObject other) {
            player= other;
        }

        public GameObject getPatrol() {
            GameObject newPatrol = Camera.Instantiate(player);
            return newPatrol;
        }

        public Vector3[] getPosSet() {
            return position;
        }
    }
}

