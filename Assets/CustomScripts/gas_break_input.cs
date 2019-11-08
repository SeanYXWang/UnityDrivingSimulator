using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class should be a component of the Manager object so it can run in the project. 

public class gas_break_input : MonoBehaviour
{
    private RCC_Settings RCCSettings{get{return RCC_Settings.Instance;}}
    double gasInput; 
    double breakInput;
    double steerInput;
    // Start is called before the first frame update
    void Start()
    {
        gasInput = 0.0;
        breakInput = 0.0;
        steerInput = 0.0;
    }

    void Inputs()
    {
        //checks the RCCSettings are set to keyboard mode. Can create different case for different modes. 
        switch(RCCSettings.controllerType){

		case RCC_Settings.ControllerType.Keyboard:
            gasInput = Input.GetAxis(RCCSettings.verticalInput);
            breakInput = Mathf.Clamp01(-Input.GetAxis(RCCSettings.verticalInput));
            steerInput = Input.GetAxis(RCCSettings.horizontalInput);
        break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        //TODO: edit here for Lab Stream Layer code. 
        Debug.Log("gas, break, steer: " + gasInput + "," + breakInput + "," + steerInput);
    }
}
