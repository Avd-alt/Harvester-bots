using System;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    private float _speedMove = 50f;
    private float _rangeDetection = 2f;

    public event Action TargetAchieved;
    public event Action ResourceDelivered;

    public void MoveToTarget(Vector3 target, bool isDeliveringResource)
    {
        RotateBot(target);
        MoveTo(target);

        if (IsWithinRange(target) == true)
        {
            if (isDeliveringResource == true)
            {
                ResourceDelivered?.Invoke();
            }
            else
            {
                TargetAchieved?.Invoke();
            }
        }
    }

    private void MoveTo(Vector3 target)
    {
        float currentY = transform.position.y;
        Vector3 targetPosition = new Vector3(target.x, currentY, target.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speedMove * Time.deltaTime);
    }

    private void RotateBot(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private bool IsWithinRange(Vector3 target)
    {
        int degree = 2;
        float rangeDetectionSqrt = Mathf.Pow(_rangeDetection, degree);
        float distance = (transform.position - target).sqrMagnitude;

        return distance <= rangeDetectionSqrt;
    }
}