using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Planetascript : MonoBehaviour
{

    private Destovac _destovac;
    private List<GameObject> _trees = new List<GameObject>();

    public struct LevelConfig
    {
        public int treeCount;
        public int capacity;
        public float enemySpeed;

        public LevelConfig(int treeCount, int capacity, float enemySpeed)
        {
            this.treeCount = treeCount;
            this.capacity = capacity;
            this.enemySpeed = enemySpeed;
        }
    }

    void Start()
    {
        List<LevelConfig> levels = new List<LevelConfig> {
            new LevelConfig(3, 2, 0.005f),
            new LevelConfig(5, 3, 0.005f),
            new LevelConfig(2, 25, 0.005f),
            new LevelConfig(20, 1, 0.008f),
            new LevelConfig(4, 4, 0.01f),
        };

        // Prseni
        _destovac = Camera.main.GetComponent<Destovac>();
        _destovac.ZacniMoznaPrset();

        LevelConfig config = levels[GameState.Instance.Level];

        GameState.Instance.GetComponent<Generator>().generateTrees(config.treeCount);
        setCapacity(config.capacity);
        setEnemySpeed(config.enemySpeed);
    }


    void setCapacity(int c)
    {
        _trees = GameObject.FindGameObjectsWithTag("Tree").ToList();
        _trees.ForEach(x => x.GetComponent<Poctoscript>().Kapacita = c);
    }

    void setEnemySpeed(float s)
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemy.GetComponent<EnemyController>().speed = s;
    }
}