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

                //NOTE: Magic Math Stolen from https://makezine.com/projects/make-40/kiwi/
                //and Thad's Wilson Code: https://github.com/MetalCowRobotics/MCR15B---Wilson/blob/master/src/org/team4213/wilson/KiwiDrive.java


                //THIS CONTROLLER CHECK IS A FAILSAFE CHECK, DO NOT REMOVE!!!!
                if (gamepad.isConnected())
                {
                    //float lx = gamepad.getLX();
                    float x = -gamepad.getRX();
                    float y = gamepad.getRY();
                    float omega = -gamepad.getLX();

                     double w1 = -x + omega * 0.5;
                     double w2 = 0.5 * x + 0.866 * y + omega * 0.5;
                     double w3 = -(-0.5 * x + 0.866 * y - omega * 0.5);

                        Debug.Print("  w1:" + w1 + "  w2:" + w2 + "  w3:" + w3);
                        motor1.Set(ControlMode.PercentOutput, w3);
                        motor2.Set(ControlMode.PercentOutput, w2);
                        motor3.Set(ControlMode.PercentOutput, w1);


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