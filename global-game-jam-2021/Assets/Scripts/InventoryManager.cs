using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureBox
{
    public string _name;

    public FeatureBox(string name)
    {
        this._name = name;
    }
}

public class InventoryManager : MonoBehaviour
{
    public GameObject mainInventory;

    public List<FeatureSlotController> featureSlots;
    public List<EquippedFeatureBoxController> equippedFeatureSlots;

    bool isActive = false;

    List<FeatureBox> featureBoxes = new List<FeatureBox>();
    // List<FeatureBox> equippedFeatureBoxes = new List<FeatureBox>();

    // Start is called before the first frame update
    void Start()
    {
        mainInventory.SetActive(isActive);
    }

    public bool EquippedFeatureBoxesContainsName(string name)
    {
        for (int i = 0; i < featureBoxes.Count; i++)
            if (featureBoxes[i]._name == name && featureSlots[i].isEquipped)
                return true;

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            isActive = !isActive;
            mainInventory.SetActive(isActive);
        }

        for (int i = 0; i < featureBoxes.Count; i++) {
            featureSlots[i].featureBoxIcon.SetActive(true);
            featureSlots[i].SetFeatureName(featureBoxes[i]._name);
        }

        List<FeatureBox> equipped = new List<FeatureBox>();

        for (int i = 0; i < featureSlots.Count; i++) {
            if (featureSlots[i].isEquipped && equipped.Count < 2)
                equipped.Add(featureBoxes[i]);
        }

        for (int i = 0; i < equippedFeatureSlots.Count; i++) {
            equippedFeatureSlots[i].isEquipped = false;
        }

        for (int i = 0; i < equipped.Count; i++) {
            equippedFeatureSlots[i].isEquipped = true;
            equippedFeatureSlots[i].SetFeatureName(equipped[i]._name);
        }

        /*

        for (int i = 0; i < featureBoxes.Count; i++) {
            featureSlots[i].featureBoxIcon.SetActive(true);
            featureSlots[i].SetFeatureName(featureBoxes[i]._name);

            if (featureSlots[i].isEquipped && !EquippedFeatureBoxesContainsName(featureBoxes[i]._name)) {
                equippedFeatureBoxes.Add(new FeatureBox(featureBoxes[i]._name));
            }
        }

        for (int i = 0; i < equippedFeatureSlots.Count; i++) {
            equippedFeatureSlots[i].isEquipped = false;
        }

        for (int i = 0; i < equippedFeatureBoxes.Count; i++) {
            equippedFeatureSlots[i].isEquipped = true;
            equippedFeatureSlots[i].SetFeatureName(equippedFeatureBoxes[i]._name);
        }

        for (int i = 0; i < equippedFeatureBoxes.Count; i++) {
            bool isEquipped = false;

            for (int j = 0; j < featureBoxes.Count; j++)
                if (featureBoxes[j]._name == equippedFeatureBoxes[i]._name)
                    isEquipped = featureSlots[i].isEquipped;

            if (!isEquipped)
                equippedFeatureBoxes.Remove(equippedFeatureBoxes[i]);
        }

        */
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject != null && other.gameObject.tag == "Box") {
            FeatureBoxController fbc = other.gameObject.GetComponent<FeatureBoxController>();

            if (fbc.collided)
                return;

            fbc.collided = true;
            featureBoxes.Add(new FeatureBox(string.Copy(fbc.getFeatureName())));
            Destroy(other.gameObject);
        }
    }
}
