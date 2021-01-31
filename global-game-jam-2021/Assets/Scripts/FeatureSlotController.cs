using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureSlotController : MonoBehaviour
{
    public bool isEquipped = false;
    public GameObject featureBoxIcon;
    public Text featureBoxText;

    Image spriteRenderer;

    void Start()
    {
        featureBoxIcon.SetActive(false);

        spriteRenderer = featureBoxIcon.GetComponent<Image>();
    }

    void Update()
    {
        spriteRenderer.color = (isEquipped) ? Color.gray : Color.white;
    }

    public void SwitchEquipped()
    {
        isEquipped = !isEquipped;
    }

    public void SetFeatureName(string name)
    {
        featureBoxText.text = name;
    }
}
