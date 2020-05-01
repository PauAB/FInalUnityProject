using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour, IPooledObject
{
    public float Speed = 1f;
    private Vector3 mDirection;

    void Update()
    {
        transform.position += mDirection * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Message m = new DamageMessage(transform, other.transform, typeof(DamageMessage), 5.0f);
        MessageManager.GetInstance().SendMessage(m);
    }

    public void OnObjectSpawn()
    {
        
    }

    public void OnObjectSpawn(Vector3 direction)
    {
        mDirection = direction;
    }
}
