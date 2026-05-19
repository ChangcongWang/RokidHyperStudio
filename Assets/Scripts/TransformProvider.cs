using System;
using System.Runtime.InteropServices;

using UnityEngine;
using LibUsbDotNet;
using LibUsbDotNet.Main;

public class TransformProvider {
    ErrorCode ec = ErrorCode.None;
    int bytesRead;
    byte[] readBuffer = new byte[64];
    UsbEndpointReader reader;
    public static UsbDeviceFinder useDeviceFinder;
    public static UsbDevice usbDevice;
    AHRS.FusionVector gyro, acc, mag;
    DateTime last_time;
    static AHRS.MadgwickAHRS AHRS = new AHRS.MadgwickAHRS(0.004f,0.001f);
    Quaternion quat= new Quaternion();
  public void TP_Init(){

    useDeviceFinder = new UsbDeviceFinder(0x04d2, 0x162f);
    usbDevice = UsbDevice.OpenUsbDevice(useDeviceFinder);
    // Debug.Log(useDeviceFinder);
    // Debug.Log(usbDevice);
    IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;
    if (!ReferenceEquals(wholeUsbDevice, null))
    {
        // Select config #1
        wholeUsbDevice.SetConfiguration(1);

        // Claim interface #0.
        wholeUsbDevice.ClaimInterface(0);
    }
    reader = usbDevice.OpenEndpointReader(ReadEndpointID.Ep02);
  }
  public void TP_Shutdown(){
    if (usbDevice != null)
    {
        if (usbDevice.IsOpen)
        {
            IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;
            if (!ReferenceEquals(wholeUsbDevice, null))
            {
                // Release interface #0.
                wholeUsbDevice.ReleaseInterface(0);
            }
            usbDevice.Close();
        }
        usbDevice = null;
        // Free usb resources
        UsbDevice.Exit();
    }
  }
  public static IntPtr TP_GetPosition(){
    return IntPtr.Zero;
  }
  public Quaternion TP_GetRotation(){
    ec = reader.Read(readBuffer, 0, 64, 250, out bytesRead);
    AHRS.SamplePeriod = (float)DateTime.Now.Subtract(last_time).TotalSeconds;
    last_time = DateTime.Now;
    acc.x = BitConverter.ToSingle(readBuffer, 9);
    acc.y = BitConverter.ToSingle(readBuffer, 13);
    acc.z = BitConverter.ToSingle(readBuffer, 17);
    gyro.x = BitConverter.ToSingle(readBuffer, 21);
    gyro.y = BitConverter.ToSingle(readBuffer, 25);
    gyro.z = BitConverter.ToSingle(readBuffer, 29);
    mag.x = BitConverter.ToSingle(readBuffer, 33);
    mag.y = BitConverter.ToSingle(readBuffer, 37);
    mag.z = BitConverter.ToSingle(readBuffer, 41);

    AHRS.Update(gyro, acc);
    quat.w = AHRS.Quaternion[0];
    quat.x = AHRS.Quaternion[1];
    quat.y = AHRS.Quaternion[2];
    quat.z = AHRS.Quaternion[3];
    
    return quat;
  }
}

