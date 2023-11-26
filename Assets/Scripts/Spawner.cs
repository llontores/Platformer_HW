using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _delay;
    [SerializeField] private float _minXPos;
    [SerializeField] private float _maxXPos;

    private void Start()
    {
        StartCoroutine(SpawnCoin());
    }

    private IEnumerator SpawnCoin()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (true)
        {
            Instantiate(_prefab,new Vector3(Random.Range(_minXPos, _maxXPos),transform.position.y,0),Quaternion.identity);

            yield return delay;
        }
    }

}
