using System;
using UnityEngine;

/// <summary>
/// RectTransformReference Class.
/// </summary>
[Serializable]
public class RectTransformReference : Reference<RectTransform, RectTransformVariable>
{
    public RectTransformReference(RectTransform Value) : base(Value)
    {
    }

    public RectTransformReference()
    {
    }
}

/// <summary>
/// RectTransformVariable Class.
/// </summary>
[CreateAssetMenu]
public class RectTransformVariable : Variable<RectTransform>
{
}