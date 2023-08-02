using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowCamera : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Camera horizontal offset to target
    /// </summary>
    [SerializeField] private float _horizontalOffset = 1.0f;
    /// <summary>
    /// Camera vertica offset to target
    /// </summary>
    [SerializeField] private float _verticalOffset = 1.0f;
    /// <summary>
    /// Look at point vertical offset to target
    /// </summary>
    [SerializeField] private float _lookatVerticalOffset = 1.0f;
    /// <summary>
    /// Follow factor to regulate camera responsiveness
    /// </summary>
    [SerializeField] private float _followFactor = 1.0f;
    #endregion
    #region references
    /// <summary>
    /// Reference to target's transform
    /// </summary>
    [SerializeField] private Transform _targetTransform;
    /// <summary>
    /// Reference to own transform
    /// </summary>
    private Transform _myTransform;
    #endregion
    /// <summary>
    /// Initialiation of desired position and lookat point
    /// </summary>
    void Start()
    {
        _myTransform = transform;
    }
    /// <summary>
    /// Updates camera position
    /// </summary>
    void LateUpdate()
    {
        _myTransform.LookAt(_targetTransform.position + Vector3.up * _lookatVerticalOffset);
        _myTransform.position = Vector3.Lerp(_myTransform.position, _targetTransform.position + Vector3.back * _horizontalOffset + Vector3.up * _verticalOffset, _followFactor / 100);
    }
}