using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SnailSegmentAimer : MonoBehaviour
{
    [SerializeField] MultiAimConstraint[] constraints;

    public void SetSource(Transform source)
    {
        foreach(MultiAimConstraint constraint in constraints)
        {
            WeightedTransformArray soruceObjects = constraint.data.sourceObjects;
            soruceObjects.SetTransform(0, source);
            constraint.data.sourceObjects = soruceObjects;
        }
    }
}
