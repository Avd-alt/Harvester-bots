using System.Collections.Generic;
using UnityEngine;

public class ResourceDistributor : MonoBehaviour
{
    private Queue<Resource> _availableResources = new Queue<Resource>();
    private HashSet<Resource> _assignedResources = new HashSet<Resource>();

    public void RegisterResource(Resource resource)
    {
        if (_availableResources.Contains(resource) == false && _assignedResources.Contains(resource) == false)
        {
            _availableResources.Enqueue(resource);
        }
    }
 
    public Resource TryGetAvailableResource()
    {
        if(_availableResources.Count != 0)
        {
            Resource resource = _availableResources.Dequeue();

            _assignedResources.Add(resource);
            return resource;
        }

        return null;
    }

    public void ReleaseResource(Resource resource)
    {
        if (_assignedResources.Contains(resource) == true)
        {
            _assignedResources.Remove(resource);
        }
    }
}