using System;
using System.Threading;
using System;
using Microsoft.SPOT; //for debug
using Microsoft.SPOT.Hardware;
using CTRE.Phoenix.MotorControl;

namespace Hero_Simple_Application5
{
    public class Program
    {
        public static void Main()
        {
            //Create global objects and variables
            LogitechWirelessGamepad gamepad = new LogitechWirelessGamepad(0); //TODO: 0 is the USB port... maybe move toa configFile along with the SPX Ids?
            CTRE.Phoenix.MotorControl.CAN.VictorSPX motor1 = new CTRE.Phoenix.MotorControl.CAN.VictorSPX(5); //Motor1 is LifeBoat index:5
            CTRE.Phoenix.MotorControl.CAN.VictorSPX motor2 = new CTRE.Phoenix.MotorControl.CAN.VictorSPX(6); //Motor2 is LifeBoat index:6
            CTRE.Phoenix.MotorControl.CAN.VictorSPX motor3 = new CTRE.Phoenix.MotorControl.CAN.VictorSPX(7); //Motor3 is LifeBoat index:7

            //WARNING!

            while (true) /* loop forever */
            {

                //TODO: Needs some type of BATTERY VOLTAGE CHECK, maybe vibe contoller when low?

                //THIS CONTROLLER CHECK IS A FAILSAFE CHECK, DO NOT REMOVE!!!!
                if (gamepad.isConnected())
                {
                    //float lx = gamepad.getLX();
                    float _x = gamepad.getRX();
                    float _y = gamepad.getRY();
                    //Debug.Print("RX:" + _x + "   RY:" + _y);


                    //Convert them to a direction vector
                    double theta = (_y == 0 || _x == 0) ? 0 : System.Math.Atan(_x / _y); //ternary check for divide-by-zero
                    double magnitude = System.Math.Sqrt((_x * _x) + (_y * _y));

                    //Debug.Print("Theta:" + theta + "     magnitude:" + magnitude);

                    if (magnitude > 0.15f) //I think this is to avoid inputStick error?
                    { //NOTE: Magic Math Stolen from https://makezine.com/projects/make-40/kiwi/
                        double vy = magnitude * System.Math.Cos(theta);
                        if (_y < 0) { vy *= -1; }

                        double vx = magnitude * System.Math.Sin(theta);
                        double sqrt3o2 = 1.0 * System.Math.Sqrt(3 / 2);

                        double backWheel = -vx;
                        double frontRightWheel = 0.5 * vx + sqrt3o2 * vy;
                        double frontLeftWheel = 0.5 * vx - sqrt3o2 * vy;

                        //Im not sure what this speed is... vector intensity?  is it percent? Can't be... hmm...
                        Debug.Print("  backWheel:" + backWheel + "  Left:" + frontLeftWheel + "  Right:" + frontRightWheel);
                        motor1.Set(ControlMode.PercentOutput, frontLeftWheel);
                        motor2.Set(ControlMode.PercentOutput, frontRightWheel);
                        motor3.Set(ControlMode.PercentOutput, backWheel);

                    }
                    else
                    { // stick is idle, stop the wheels
                        motor1.Set(ControlMode.PercentOutput, 0);
                        motor2.Set(ControlMode.PercentOutput, 0);
                        motor3.Set(ControlMode.PercentOutput, 0);
                    }


                    CTRE.Phoenix.Watchdog.Feed(); //this keeps the bot active... just leave it alone
                }
                else
                { //if the Controller Disconnects, stop the motors.
                    motor1.Set(ControlMode.PercentOutput, 0);
                    motor2.Set(ControlMode.PercentOutput, 0);
                    motor3.Set(ControlMode.PercentOutput, 0);
                }

                System.Threading.Thread.Sleep(10); //read the inputs every 10 milliseconds
            }
        }

    }
}