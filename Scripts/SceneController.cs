using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Patrols {
    public class Diretion {
        public const int UP = 0;
        public const int DOWN = 2;
        public const int LEFT = -1;
        public const int RIGHT = 1;
    }

    public class FenchLocation {
        public const float FenchHori = 12.42f;
        public const float FenchVertLeft = -3.0f;
        public const float FenchVertRight = 3.0f;
    }

    public interface IUserAction {
        void heroMove(int dir);
    }

    public interface IAddAction {
        void addRandomMovement(GameObject other, bool isActive);
        void addDirectMovement(GameObject other);
    }

    public interface IGameStatusOp {
        int getHeroStandOnArea();
        void heroEscapeAndScore();
        void patrolHitHeroAndGameover();
    }

    public class SceneController : System.Object, IUserAction, IAddAction, IGameStatusOp {
        private static SceneController _instance;
        private GameModel gameModel;
        private GameEventManager manager;

        public static SceneController getInstance() {
            if (_instance == null)
                _instance = new SceneController();
            return _instance;
        }

        internal void setGameModel(GameModel other) {
            if (gameModel == null) {
                gameModel = other;
            }
        }

        internal void setGameEventManager(GameEventManager _myGameEventManager) {
            if (manager == null) {
                manager = _myGameEventManager;
            }
        }

        /*********************实现IUserAction接口*********************/
        public void heroMove(int dir) {
            gameModel.heroMove(dir);
        }

        /*********************实现IAddAction接口*********************/
        public void addRandomMovement(GameObject sourceObj, bool isActive) {
            gameModel.addRandomMovement(sourceObj, isActive);
        }

        public void addDirectMovement(GameObject sourceObj) {
            gameModel.addDirectMovement(sourceObj);
        }

        /*********************实现IGameStatusOp接口*********************/
        public int getHeroStandOnArea() {
            return gameModel.getHeroStandOnArea();
        }

        public void heroEscapeAndScore() {
            manager.heroEscapeAndScore();
        }

        public void patrolHitHeroAndGameover() {
            manager.patrolHitHeroAndGameover();
        }
    }
}

