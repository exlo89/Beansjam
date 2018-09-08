using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public float distance = 10;

    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y, distance * -1);
    }
}