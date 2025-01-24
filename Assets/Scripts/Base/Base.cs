using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IResourceDeliveryAnnouncer
{
    [SerializeField] private SpawnerBots _spawnerBots;
    [SerializeField] private ResourceDistributor _resourceDistributor;

    private int _spawnStartCount = 3;
    private List<Bot> _bots = new List<Bot>();

    private void Start()
    {
        for (int i = 0; i < _spawnStartCount; i++)
        {
            Bot bot = _spawnerBots.SpawnBot();
            bot.SetResourceManager(this);
            bot.SetBasePosition(this.transform.position);
            _bots.Add(bot);
        }
    }

    private void Update()
    {
        AssignResourcesToBots();
    }

    public void NotifyResourceDelivered(Resource resource)
    {
        _resourceDistributor.ReleaseResource(resource);
    }

    private void AssignResourcesToBots()
    {
        foreach (var bot in _bots)
        {
            if (bot.HasTarget() == false)
            {
                Resource resource = _resourceDistributor.TryGetAvailableResource();

                if (resource != null)
                {
                    bot.SetTarget(resource.transform.position);
                }
            }
        }
    }
}
