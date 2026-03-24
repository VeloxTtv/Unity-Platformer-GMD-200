using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2IntVariable _playerGridPos;
    [SerializeField] private GameObject _tilePrefab1;
    [SerializeField] private GameObject _tilePrefab2;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int numRows = 6;
    [SerializeField] private int numColumns = 6;
    [SerializeField] private Vector2 tileSize = Vector2.one;
    [SerializeField] private Vector2 padding = new Vector2(0.4f, 0.4f);

    private List<GameObject> _tiles = new List<GameObject>();

    void Start() 
    {
        System.Random random = new System.Random();
        int randomTile = random.Next(1, 3);
        int randomCoinSpace = random.Next(0, numRows * numColumns);
        int randomEnemySpace = random.Next(numRows * 2, numRows * numColumns);
        while (randomEnemySpace == randomCoinSpace) randomEnemySpace = random.Next(0, numRows * numColumns);
        int currentSpace = 0;

        Debug.Log($"Coin at space {randomCoinSpace}, Enemy at space {randomEnemySpace}");

        _tiles.Capacity = numRows * numColumns;

        for (int row = 0; row < numRows; row++)
        {
            for(int col = 0; col < numColumns; col++)
            {
                Vector2 pos = new Vector2(col * (tileSize.x + padding.x) - numColumns/2f, row * (tileSize.y + padding.y) - numRows / 2f);

                if (randomCoinSpace == currentSpace)
                {
                    Instantiate(_coinPrefab, pos, Quaternion.identity, transform);
                }

                if (randomEnemySpace == currentSpace)
                {
                    GameObject enemy = Instantiate(_enemyPrefab, pos, Quaternion.identity, transform);
                    enemy.GetComponent<enemyController>()._playerGridPos = _playerGridPos;
                }

                if (randomTile == 1)
                {
                    GameObject tile = Instantiate(_tilePrefab1, pos, Quaternion.identity, transform);
                    _tiles.Add(tile);
                }
                if (randomTile == 2)
                {
                    GameObject tile = Instantiate(_tilePrefab2, pos, Quaternion.identity, transform);
                    _tiles.Add(tile);
                }
                currentSpace++;
            }
        }
    }

    public GameObject GetTile(int column, int row)
    {
        //Check if coordinate is valid
        if (column < 0 || row < 0 || column >= numColumns || row >= numRows)
        {
            return null;
        }
        return _tiles[row * numColumns + column];
    }

    public bool moveValid(int column, int row)
    {
        if (column < 0 || row < 0 || column >= numColumns || row >= numRows)
        {
            return false;
        }
        else return true;
    }
}
