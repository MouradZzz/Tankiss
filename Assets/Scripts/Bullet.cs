using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Private Refrence

    private Rigidbody _rigidbody;

    #endregion

    [Header("Bullet Reference")] 
    [Space] 
    public float bulletSpeed;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Shoot();
    }

    void Shoot()
    {
        _rigidbody.AddForce(transform.up * bulletSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var t = collision.collider.GetComponent<Damageable>();

        if (t)
        {
            t.InflictDamage(1);
            Destroy(gameObject);
        }
    }
}
