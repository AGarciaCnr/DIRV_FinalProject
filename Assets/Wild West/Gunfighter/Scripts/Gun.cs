using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _bulletSpeed = 100f;

    public void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

        if (bullet.TryGetComponent(out Rigidbody bulletRigidbody))
        {
            ApplyForce(bulletRigidbody);
        }
    }

    private void ApplyForce(Rigidbody bulletRigidbody)
    {
        Vector3 force = _bulletSpawnPoint.forward * _bulletSpeed;
        bulletRigidbody.AddForce(force);
    }
}