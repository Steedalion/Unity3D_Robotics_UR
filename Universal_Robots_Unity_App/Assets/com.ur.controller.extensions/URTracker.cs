using UnityEngine;

namespace Unity_UniversalRobot.com.ur.controller
{
    public class URTracker : MonoBehaviour
    {
        [SerializeField] private Transform tip;
        [SerializeField] private Transform goal;
        [SerializeField] private bool tracking;
        [SerializeField] private float acceleration = 0.1f;
        [SerializeField] private float time = 0.1f;
        [SerializeField] private float gain = 4f;
        [SerializeField] private float rGain;


        void Update()
        {
            if (!tracking)
            {
                return;
            }

            var goalPosition = goal.position;
            var tipPosition = tip.position;

            var vx = (goalPosition.x - tipPosition.x) * gain;
            var vy = (goalPosition.y - tipPosition.y) * gain;
            var vz = (goalPosition.z - tipPosition.z) * gain;
            var eulerAngles = goal.eulerAngles;
            var tipAnlge = tip.transform.eulerAngles;
            var wx = (eulerAngles.x - tipAnlge.x) * rGain;
            var wy = (eulerAngles.y - tipAnlge.y) * rGain;
            var wz = (eulerAngles.z - tipAnlge.z) * rGain;
            var urSpeedCommand = new SetSpeed(vx, vz, vy, wx, wz, wy, acceleration, time);
            // Debug.Log(urSpeedCommand);
            urSpeedCommand.Execute();
        }
    }
}