using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject ninjaModel;
    public Color color;
    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeColorToRed()
    {
        ninjaModel.GetComponent<Renderer>().material.color = color;
    }

}
