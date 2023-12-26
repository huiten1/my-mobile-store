using System;
using UnityEngine.Serialization;

namespace _Main._Scripts
{
    [Serializable]
    public class GameData
    {
        public bool onBoarding;
        public bool showItems;
        public int playerGold;
        public int level;
        public float playerMoveSpeed;
        public float botSpawnTime;
        public float scooterBonusSpeed;
        public GameData()
        {
            level = 1;
            onBoarding = true;
            showItems = false;
            playerGold = 0;
            playerMoveSpeed = 5;
            botSpawnTime = 4;
            scooterBonusSpeed = 2;
        }
    }
}