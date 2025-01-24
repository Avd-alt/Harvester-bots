using UnityEngine;

[RequireComponent(typeof(BotMovement))]
public class Bot : MonoBehaviour
{
    private BotMovement _botMovement;
    private Resource _currentResource;
    private bool _haveTarget;
    private bool _isAdding = false;
    private IResourceDeliveryAnnouncer _resourceDeliveryAnnouncer;
    private Vector3 _targetPoint;
    private Vector3 _basePosition;

    private void Awake()
    {
        _botMovement = GetComponent<BotMovement>();
    }

    private void OnEnable()
    {
        _botMovement.TargetAchieved += TryPickUpResource;
        _botMovement.ResourceDelivered += ResetTarget;
    }

    private void OnDisable()
    {
        _botMovement.TargetAchieved -= TryPickUpResource;
        _botMovement.ResourceDelivered -= ResetTarget;
    }

    private void Update()
    {
        if (_haveTarget == true)
        {
            _botMovement.MoveToTarget(_targetPoint, _isAdding);
        }
    }

    public void SetTarget(Vector3 targetPoint)
    {
        if (_haveTarget == false)
        {
            _targetPoint = targetPoint;
            _haveTarget = true;
        }
    }

    public bool HasTarget()
    {
        return _haveTarget;
    }

    public void SetBasePosition(Vector3 basePosition)
    {
        _basePosition = basePosition;
    }

    public void SetResourceManager(IResourceDeliveryAnnouncer resourceDeliveryAnnouncer)
    {
        _resourceDeliveryAnnouncer = resourceDeliveryAnnouncer;
    }

    private void ResetTarget()
    {
        if (_currentResource != null)
        {
            _resourceDeliveryAnnouncer.NotifyResourceDelivered(_currentResource);
            ResetState();
        }
    }

    private void ResetState()
    {
        _haveTarget = false;
        _targetPoint = Vector3.zero;
        _isAdding = false;
        _currentResource = null;
    }

    private void TryPickUpResource()
    {
        float pickupRadius = 2f;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRadius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.TryGetComponent(out Resource resource) == true)
            {
                _currentResource = resource;
                _currentResource.transform.SetParent(transform);
                _currentResource.transform.localPosition = new Vector3(0f, 0f, 1f);

                _isAdding = true;

                _currentResource.Take();
                _targetPoint = _basePosition;
            }
        }
    }
}