
public class ForwardX : SetSpeed
{
    public ForwardX(float vz, float acceleration, float time) : base(0, 0, vz, 0, 0, 0, acceleration, time)
    {
    }
}
public class SetSpeed : URCommand
{
    protected float vx;
    protected float vy;
    protected float vz;
    protected float wx;
    protected float wy;
    protected float wz;
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

    public virtual void Execute()
    {
        // UrDataProcessing.UR_Control_Data.command = ToBytes();
        // UrDataProcessing.UR_Control_Data.joystick_button_pressed = true;
        // UrDataProcessing.UR_Control_Data.command = ToBytes();
        
        GlobalVariables_TCP_IP_client.command = ToBytes();
        ur_data_processing.joystick_button_pressed = true;
        
        
        // GlobalVariables_TCP_IP_client.button_pressed[0] = false;

        // ur_data_processing.UR_Control_Data.joystick_button_pressed = false;
    }
}