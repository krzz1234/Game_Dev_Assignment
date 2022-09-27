using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfor : MonoBehaviour
{
    private Vector3 initialPos;
    private bool signal = false;

    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform forward if signal is false
        if(signal == false){
            if(Vector3.Distance(initialPos, transform.position) >= 15.0f){
                signal = true;
            }
            transform.position = transform.position + new Vector3 (0,0,5*Time.deltaTime);
        }
        // Move the platform backward if signal is true
        if(signal == true){
            if(Vector3.Distance(initialPos, transform.position) <= 0.5f){
                signal = false;
            }
            transform.position = transform.position - new Vector3 (0,0,5*Time.deltaTime);
        }
    }
}
