using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeEmpty : MonoBehaviour
{
    [SerializeField]
    public bool isEmpty;
    [SerializeField]
    public void Start()
    {
        isEmpty = true;
    }
}
