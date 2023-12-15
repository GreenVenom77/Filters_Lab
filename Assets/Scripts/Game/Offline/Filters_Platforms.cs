using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filters_Platforms : MonoBehaviour
{
    [SerializeField] private int Index;
    [SerializeField] private Filters_Control FControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FControl.SetEffect(Index);
        }
    }
}
