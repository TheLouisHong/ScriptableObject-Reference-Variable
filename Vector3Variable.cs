using System;
using UnityEngine;

/// <summary>
/// Vector3Reference Class.
/// </summary>
[Serializable]
public class Vector3Reference : Reference<Vector3, Vector3Variable>
{
    public Vector3Reference(Vector3 Value) : base(Value) { }
    public Vector3Reference() { }
}

/// <summary>
/// Vector3Variable Class.
/// </summary>
[CreateAssetMenu]
public class Vector3Variable : Variable<Vector3> { }