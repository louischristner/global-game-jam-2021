using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedFeatureBoxController : MonoBehaviour
{
    public bool isEquipped = false;
    public GameObject featureBoxIcon;
    public Text featureBoxText;

    Image spriteRenderer;
    string featureName;

    void Start()
    {
        featureBoxIcon.SetActive(false);

        spriteRenderer = featureBoxIcon.GetComponent<Image>();
    }

    void Update()
    {
        featureBoxIcon.SetActive(isEquipped);
        featureBoxText.text = (isEquipped) ? featureName : "Empty";
    }

    public void SwitchEquipped()
    {
        isEquipped = !isEquipped;
    }

    public void SetFeatureName(string name)
    {
        featureName = name;
    }
}
