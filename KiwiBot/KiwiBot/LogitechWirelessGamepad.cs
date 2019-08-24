using System;

public class LogitechWirelessGamepad
{

    CTRE.Phoenix.Controller.GameController gamepad;

    //ButtonNumberMappings
    uint BUTTON_X = 1;
    uint BUTTON_A = 2;
    uint BUTTON_B = 3;
    uint BUTTON_Y = 4;
    uint BUTTON_LB = 5;
    uint BUTTON_RB = 6;
    //7 is LT 100%
    //8 is RT 100%
    uint BACK = 9;
    uint START = 10;
    uint RSTICK = 11;
    uint LSTICK = 12;

    //AnalogAxis
    uint LSTICK_X = 0;
    //uint LSTICK_Y = ?;
    uint RSITCK_X = 2;
    uint RSTICK_Y = 5;
    uint DPAD_Y = 1;
    uint DPAD_X = 3;


    public LogitechWirelessGamepad(uint usbPortNumber)
    {
        gamepad = new CTRE.Phoenix.Controller.GameController(new CTRE.Phoenix.UsbHostDevice(usbPortNumber));
    }

    public Boolean isX()
    {
        return gamepad.GetButton(BUTTON_X);
    }

    public Boolean isA()
    {
        return gamepad.GetButton(BUTTON_A);
    }

    public Boolean isB()
    {
        return gamepad.GetButton(BUTTON_B);
    }

    public Boolean isY()
    {
        return gamepad.GetButton(BUTTON_Y);
    }

    public Boolean isLB()
    {
        return gamepad.GetButton(BUTTON_LB);
    }

    public Boolean isRB()
    {
        return gamepad.GetButton(BUTTON_RB);
    }

    public Boolean isBack()
    {
        return gamepad.GetButton(BACK);
    }

    public Boolean isStart()
    {
        return gamepad.GetButton(START);
    }

    public Boolean isRStick()
    {
        return gamepad.GetButton(RSTICK);
    }

    public Boolean isLStick()
    {
        return gamepad.GetButton(LSTICK);
    }

    public Boolean isDpadUp()
    {
        return gamepad.GetAxis(DPAD_Y) == -1;
    }
    public Boolean isDpadDown()
    {
        return gamepad.GetAxis(DPAD_Y) == 1;
    }

    public Boolean isDpadLeft()
    {
        return gamepad.GetAxis(DPAD_X) == -1;
    }
    public Boolean isDpadRight()
    {
        return gamepad.GetAxis(DPAD_X) == 1;
    }

    public float getRX()
    {
        return gamepad.GetAxis(RSITCK_X);
    }
    public float getRY()
    {
        return -gamepad.GetAxis(RSTICK_Y); //inverted so up is positive
    }

    public float getLX()
    {
        return gamepad.GetAxis(LSTICK_X);
    }

    public Boolean isConnected()
    {
        return gamepad.GetConnectionStatus() == CTRE.Phoenix.UsbDeviceConnection.Connected;
    }
}
