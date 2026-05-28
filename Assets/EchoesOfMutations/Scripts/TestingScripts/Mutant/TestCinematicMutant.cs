using System;
using UnityEngine;

public class TestCinematicMutant : MonoBehaviour
{
    //THIS SCRIP IS FOR TESTING PURPOSES ONLY (TEMPORALY)
    public float MutantSpeed;

    public static Action OnMoveMutant;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void MoveMutant()
    {
        Vector3 moveDir = Vector3.forward * MutantSpeed * Time.deltaTime;
        transform.position -= moveDir;  
    }
}
