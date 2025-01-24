using UnityEngine;

public class SpawnerBots : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private Bot _prefabBot;

    public Bot SpawnBot()
    {
        SpawnPoint spawnPoint = GetRandomPoint();

        return Instantiate(_prefabBot, spawnPoint.transform.position, Quaternion.identity);
    }

    private SpawnPoint GetRandomPoint()
    {
        int minNumber = 0;
        int maxNumber = _spawnPoints.Length;

        return _spawnPoints[Random.Range(minNumber, maxNumber)];
    }
}