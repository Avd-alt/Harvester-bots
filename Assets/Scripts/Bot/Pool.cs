using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private Resource _resource;

    private int _deafaultCapacity = 15;
    private int _maxCapacity = 40;
    private ObjectPool<Resource> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Resource>(
            createFunc: () => CreateObject(),
            actionOnGet: resource => resource.gameObject.SetActive(true),
            actionOnRelease: resource => resource.gameObject.SetActive(false),
            actionOnDestroy: resource => Destroy(resource.gameObject),
            collectionCheck: true,
            defaultCapacity: _deafaultCapacity,
            maxSize: _maxCapacity);
    }

    public Resource GetResource()
    {
        Resource resource = _pool.Get();
        return resource;
    }

    public void RealeseResource(Resource resource)
    {
        resource.transform.SetParent(null);
        _pool.Release(resource);
    }

    private Resource CreateObject() => Instantiate(_resource);
}
