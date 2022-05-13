using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URTracker : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private Transform goal;
    [SerializeField] private bool tracking;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float time = 0.1f;
    [SerializeField] private bool urButton;
    [SerializeField] private float gain = 4f;


    void Update()
    {
        if (!tracking)
        {
            return;
        }
        ur_data_processing.UR_Control_Data.joystick_button_pressed = true;

        var goalPosition = goal.position;
        var tipPosition = tip.position;

        var vx = (goalPosition.x - tipPosition.x)*gain;
        var vy = (goalPosition.y - tipPosition.y)*gain;
        var vz = (goalPosition.z - tipPosition.z)*gain;
        var urSpeedCommand = new SetSpeed(vx, vz, vy, 0, 0, 0, acceleration, time);
        Debug.Log(urSpeedCommand);
        ur_data_processing.UR_Control_Data.command = urSpeedCommand.ToBytes();
        if (urButton)
        {
            ur_data_processing.UR_Control_Data.button_pressed[0] = true;
        }
        ur_data_processing.UR_Control_Data.joystick_button_pressed = false;

    }
}