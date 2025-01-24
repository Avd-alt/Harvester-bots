using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerResources : MonoBehaviour
{
    [SerializeField] private Pool _poolResources;
    [SerializeField] private SpawnPoint[] _spawnPoints;

    private List<SpawnPoint> _freeSpawnPoints = new List<SpawnPoint>();
    private Dictionary<Resource, SpawnPoint> _resourceToSpawnPoint = new Dictionary<Resource, SpawnPoint>();
    private Coroutine _coroutine;

    private void Start()
    {
        _freeSpawnPoints.AddRange(_spawnPoints);

        _coroutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        int secondsDelay = 1;
        WaitForSeconds delay = new WaitForSeconds(secondsDelay);

        while (true)
        {
            yield return delay;

            if (_freeSpawnPoints.Count > 0)
            {
                SpawnResource();
            }
        }
    }

    private void SpawnResource()
    {
        SpawnPoint spawnPoint = GetRandomFreePoint();

        Resource resource = _poolResources.GetResource();
        resource.transform.position = spawnPoint.Position;

        _resourceToSpawnPoint[resource] = spawnPoint;

        _freeSpawnPoints.Remove(spawnPoint);

        resource.OnTaken += OnResourceTaken;
    }

    private void OnResourceTaken(Resource resource)
    {
        resource.OnTaken -= OnResourceTaken;

        if (_resourceToSpawnPoint.TryGetValue(resource, out SpawnPoint spawnPoint) == true)
        {
            _freeSpawnPoints.Add(spawnPoint);

            _resourceToSpawnPoint.Remove(resource);
        }
    }

    private SpawnPoint GetRandomFreePoint()
    {
        int index = Random.Range(0, _freeSpawnPoints.Count);
        return _freeSpawnPoints[index];
    }
}