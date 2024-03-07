using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody bulletRigidbody))
        {
            this.gameObject.SetActive(false);
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }
}
