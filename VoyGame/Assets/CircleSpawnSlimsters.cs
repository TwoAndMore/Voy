using UnityEngine;

public class CircleSpawnSlimsters : MonoBehaviour
{
    private const float _angle = 360f;
    
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _enemyAmount = 20;
    [SerializeField] private float _radius  = 5f;

    private void Start()
    {
        for (int i = 0; i < _enemyAmount; i++)
        {
            float anglePlace = (_angle / _enemyAmount) * i;

            float x = transform.position.x + Mathf.Sin(anglePlace) * _radius;
            float z = transform.position.z + Mathf.Cos(anglePlace) * _radius;
            Vector3 spawnPosition = new Vector3(x, 1, z);
            
            GameObject enemy = Instantiate(_enemyPrefab, transform);
            enemy.transform.position = spawnPosition;
        }
    }
}
