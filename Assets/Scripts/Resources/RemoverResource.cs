using System;
using UnityEngine;

public class RemoverResource : MonoBehaviour
{
    [SerializeField] private Pool _poolResources;

    public event Action<Resource> ResourceAdding;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) == true)
        {
            ResourceAdding?.Invoke(resource);
            _poolResources.RealeseResource(resource);
        }
    }
}