using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureBoxController : MonoBehaviour
{
    public string featureName;
    public bool collided = false;

    public string getFeatureName()
    {
        return featureName;
    }
}
