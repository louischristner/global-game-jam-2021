using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHover : MonoBehaviour
{
    public float amplitude;          //Set in Inspector
    public float speed;              //Set in Inspector 
    private float tempValy;
    private float tempValx;

    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        tempValy = transform.position.y;
        tempValx = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos.y = tempValy + amplitude * Mathf.Sin(speed * Time.time);
        tempPos.x = tempValx;
        transform.position = tempPos;
    }
}
