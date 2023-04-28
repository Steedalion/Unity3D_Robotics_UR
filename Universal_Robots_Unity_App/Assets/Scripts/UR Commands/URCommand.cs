using System.Text;
using System;

namespace Unity_UniversalRobot.com.ur.controller.Scripts.UR3
{
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