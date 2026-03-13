using UnityEngine;

/// <summary>
/// Smooth top-down follow camera. Attach to Main Camera.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 12f, -6f);
    [SerializeField] private float _smoothSpeed = 8f;

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 desiredPos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, _smoothSpeed * Time.deltaTime);
        transform.LookAt(_target);
    }
}