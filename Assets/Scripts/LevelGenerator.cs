using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] LevelBlocks;
    public int MaxGeneratedBlocks = 100;
    public float LevelSpeed = 2;
    [Range(1, 9)] public int SpawnLevelSize = 5;
    [Range(-5, 5)] public int Offset;

    public int BlockHeight = 10;

    private GameObject[] _currentBlocks;
    private int _borderBelow;
    private int _topOffset;
    
    // Use this for initialization
    void Start()
    {
        _borderBelow = (int) Mathf.Round((SpawnLevelSize - Offset) * 0.5F) * BlockHeight;
        _topOffset = SpawnLevelSize % 2 == 0 ? 1 : 0;

        Debug.Log(_borderBelow);
        _currentBlocks = new GameObject[SpawnLevelSize];
        for (int i = 0; i < SpawnLevelSize; i++)
        {
            _currentBlocks[i] = Instantiate(LevelBlocks[i%LevelBlocks.Length],
                transform.position + Vector3.down * _borderBelow +
                Vector3.up * BlockHeight * i,
                transform.rotation);
        }
    }

    private void Update()
    {
        float step = LevelSpeed * Time.deltaTime;

        for (int i = 0; i < _currentBlocks.Length; i++)
        {
            if (_currentBlocks[i].transform.position.y < -_borderBelow - BlockHeight)
            {
                Destroy(_currentBlocks[i]);
                int radommNumber = Random.Range(0, LevelBlocks.Length);
                
                _currentBlocks[i] = Instantiate(LevelBlocks[radommNumber],
                    transform.position + Vector3.up * _borderBelow + Vector3.up * _topOffset * -BlockHeight,
                    transform.rotation);
            }
            else
            {
                _currentBlocks[i].transform.position = Vector3.MoveTowards(_currentBlocks[i].transform.position,
                    _currentBlocks[i].transform.position + Vector3.down, step);
            }
        }
    }
}