using DG.Tweening;
using System;
using UnityEngine;

public class TestCinematicMutant : MonoBehaviour
{
    public float duration;
    public float endPosition;
    //public float MutantSpeed; 
    public static Action OnMoveMutant;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    
    public void MoveMutant()
    {       
        transform.DOMoveZ(endPosition , duration);
        /*
        Vector3 moveDir = Vector3.forward * MutantSpeed * Time.deltaTime;
        transform.position -= moveDir;  
        
        */
    }
    
}
