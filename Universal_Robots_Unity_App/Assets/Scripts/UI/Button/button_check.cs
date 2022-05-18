// ------------------------------------------------------------------------------------------------------------------------ //
// ----------------------------------------------------- LIBRARIES -------------------------------------------------------- //
// ------------------------------------------------------------------------------------------------------------------------ //

// -------------------- System -------------------- //

using System;
using System.Text;
// -------------------- Unity -------------------- //
using UnityEngine.EventSystems;
using UnityEngine;

public class button_check : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // -------------------- String -------------------- //
    public float acceleration = 1;
    public float time = 0.05f;
    public float vx, vy, vz, wx, wy, wz;
    public string[] speed_param_null = new string[6] { "0.0", "0.0", "0.0", "0.0", "0.0", "0.0" };
    SetSpeed urSpeedCommand;

    // -------------------- Int -------------------- //
    public ButtonSignal index;

    // -------------------- UTF8Encoding -------------------- //
    private UTF8Encoding utf8 = new UTF8Encoding();

    // -------------------- Button -> Pressed -------------------- //
    public void OnPointerDown(PointerEventData eventData)
    {

        //TODO: Move urSpeedCommand to start for effeciency
        urSpeedCommand = new SetSpeed(vx, vy, vz, wx, wy, wz, acceleration, time);

        // create auxiliary command string for speed control UR robot
        // ur_data_processing.UR_Control_Data.aux_command_str =
        //     "speedl([" + speed_param[0] + "," + speed_param[1] + "," + speed_param[2]
        //     + "," + speed_param[3] + "," + speed_param[4] + "," + speed_param[5] + "], a =" + acceleration + ", t =" +
        //     time + ")" + "\n";
        // get bytes from command string
        ur_data_processing.UR_Control_Data.command = urSpeedCommand.ToBytes();
        // confirmation variable -> is pressed
        ur_data_processing.UR_Control_Data.button_pressed[(int)index] = true;
    }

    // -------------------- Button -> Un-Pressed -------------------- //
    public void OnPointerUp(PointerEventData eventData)
    {
        // confirmation variable -> is un-pressed
        ur_data_processing.UR_Control_Data.button_pressed[(int)index] = false;
    }
}

public class SetSpeed : URCommand
{
    private float vx, vy, vz, wx, wy, wz;
    float acceleration;
    float time;
    private readonly string message;

    public SetSpeed(float vx, float vy, float vz, float wx, float wy, float wz, float acceleration, float time)
    {
        this.vx = vx;
        this.vy = vy;
        this.vz = vz;
        this.wx = wx;
        this.wy = wy;
        this.wz = wz;
        this.acceleration = acceleration;
        this.time = time;
        // message = "speedl([" + vx + "," + vy + "," + vz
                  // + "," + wx + "," + wy + "," + wz + "], a =" + acceleration + ", t =" + time + ")" + "\n";
         message = $"speedl([{vx},{vy},{vz},{wx},{wy},{wz}], a ={acceleration}, t ={time})\n";
    }

    public override string ToString()
    {
        return message;
    }

    public override string BuildCommand()
    {
        return message;
    }
}

public abstract class URCommand
{
    private UTF8Encoding utf8 = new UTF8Encoding();

    /// <summary>
    /// Builds the command into a string. This string includes the command and variables being set.
    /// </summary>
    /// <returns></returns>
    public abstract string BuildCommand();

    public byte[] ToBytes()
    {
        return utf8.GetBytes(BuildCommand());
    }
}

public enum ButtonSignal
{
    XIncrease = 0,
    XDecrease = 1,
    YIncrease = 2,
    YDecrease = 3,
    ZIncrease = 4,
    ZDecrease = 5,
    RxIncrease = 6,
    RxDecrease = 7,
    RyIncrease = 8,
    RyDecrease = 9,
    RzIncrease = 10,
    RzDecrease = 11,
}