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
        public float playerMoveSpeed;
        
        public GameData()
        {
            onBoarding = true;
            showItems = true;
            playerGold = 0;
            playerMoveSpeed = 5;
        }
    }
}