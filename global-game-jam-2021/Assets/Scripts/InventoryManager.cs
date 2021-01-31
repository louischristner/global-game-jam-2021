using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject mainInventory;

    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        mainInventory.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            isActive = !isActive;
            mainInventory.SetActive(isActive);
        }
    }
}
