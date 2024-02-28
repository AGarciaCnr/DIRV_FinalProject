using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class Drink : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] public float _fill;
    private float _initialFill;
    [SerializeField] private float _emptyingTime = 5;
    [SerializeField] private AnimationCurve _dumpAngle;

    private float _time = 0;
    private Transform _transform;
    private ParticleSystem.EmissionModule _emission;

    private void Awake()
    {
        _emission = GetComponentInChildren<ParticleSystem>().emission;
        _transform = this.transform;
        _initialFill = _fill;
    }

    private void Update()
    {
        if (!IsEmpty() && IsDumped())
        {
            _emission.enabled = true;
            Empty();

            _fill = (_fill <= 0.25f) ? 0.0f : _fill;
        }
        else
        {
            _emission.enabled = false;
        }
    }

    private void Empty()
    {
        _time += Time.deltaTime;
        _fill = Mathf.Lerp(_initialFill, 0, _time / _emptyingTime);
    }

    public bool IsEmpty()
    {
        return _fill <= 0;
    }

    public bool IsDumped()
    {
        Vector3 drinkAngles = new Vector3(_transform.eulerAngles.x, 0.0f, _transform.eulerAngles.z);
        float currentAngle = Quaternion.Angle(Quaternion.Euler(drinkAngles), Quaternion.identity);
        float dumpAngle = _dumpAngle.Evaluate(_fill);
        return currentAngle > dumpAngle;
    }
}
