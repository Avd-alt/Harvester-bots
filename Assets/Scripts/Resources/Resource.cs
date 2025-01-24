using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public event Action<Resource> OnTaken;

    public void Take()
    {
        OnTaken?.Invoke(this);
    }
}