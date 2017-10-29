using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObserver : MonoBehaviour {

#region Singleton
    private static WorldObserver instance;

    public static WorldObserver Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WorldObserver>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("WorldObserver");
                    instance = obj.AddComponent<WorldObserver>();
                }
            }
            
            return instance;
        }
    }
#endregion

    private List<PlayerInfo> players;
    private List<EnemyInfo> enemies;

    public List<PlayerInfo> Players
    {
        get
        {
            if (players == null)
            {
                players = new List<PlayerInfo>();
            }

            return players;
        }
    }

    public List<EnemyInfo> Enemies
    {
        get
        {
            if (enemies == null)
            {
                enemies = new List<EnemyInfo>();
            }

            return enemies;
        }
    }

    private int scope;
    public void EnemyKilled()
    {
        scope++;
    }

    [SerializeField]
    private int maxEnemyCount;

    public int MaxEnemyCount
    {
        get
        {
            return maxEnemyCount;
        }
        private set
        {
            maxEnemyCount = value;
        }
    }
}
