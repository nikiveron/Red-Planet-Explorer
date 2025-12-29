using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.attachedRigidbody.TryGetComponent<DroneDestructor>(out var destructor))
        {
            destructor.Detonate();
        }
    }
}
