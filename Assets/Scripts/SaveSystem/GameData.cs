using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameProgramming2D
{
    [Serializable]
    public class GameData
    {
        public int Score;
        public PlayerData PlayerData;
        public List<EnemyData> EnemyDatas;

    }
}
