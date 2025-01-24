using System.Collections;
using UnityEngine;

public class ScannerResources : MonoBehaviour
{
    [SerializeField] private LayerMask _maskResource;
    [SerializeField] private ResourceDistributor _resourceDistributor;

    private float _scanRadius;
    private float _startRadius = 0;
    private float _endRadius = 70f;
    private Coroutine _coroutine;

    private void Start()
    {
        _scanRadius = _startRadius;

        _coroutine = StartCoroutine(ScanPlane());
    }

    private IEnumerator ScanPlane()
    {
       float stepIncreaseRadius = 15f;
       float increaseInterval = 1f;

       WaitForSeconds delay = new WaitForSeconds(increaseInterval);

        while (true)
        {
            GetResourcesWithPlane();

            if(_scanRadius < _endRadius)
            {
                _scanRadius += stepIncreaseRadius;

                if (_scanRadius >= _endRadius)
                    _scanRadius = _startRadius;
            }

            yield return delay;
        }
    }

    private void GetResourcesWithPlane()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanRadius, _maskResource);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Resource resource) == true)
            {
                int degree = 2;
                float scanRadiusSqr = Mathf.Pow(_scanRadius, degree);
                float distance = (transform.position - resource.transform.position).sqrMagnitude;

                if (distance <= scanRadiusSqr)
                {
                    _resourceDistributor.RegisterResource(resource);
                }
            }
        }
    }
}