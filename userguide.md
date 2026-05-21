需要下载 Zadig 连接眼镜 Libusbdotnet才能连接上
![alt text](image-3.png)
接口3
usbser (v10.0.19041.3636) 原来
libusb-win32 (v1.4.0.0) 需装驱动
接口2
![alt text](image-2.png)
WinUSB (v6.1.7600.16385) 需装驱动



https://github.com/badicsalex/ar-drivers-rs
https://github.com/DiscreteTom/HyperStudio

Rokid
https://custom.rokid.com/prod
rokid_web/c88be4bcde4c42c0b8b53409e1fa1701/pc/cn/bc65117e236641e2b0cf0a9e4a23a300.html?documentId=034b27e12d0648cd8b654e4b258e1d61
https://custom.rokid.com/prod/rokid_web/c88be4bcde4c42c0b8b53409e1fa1701/pc/cn/2e1d6bab7d89490ca1b1a61ff53a9321.html?documentId=c5541389425742f58f1153bdf98fe5a8

HyperStudio 屏幕展示
HyperDesktopDuplication 屏幕到unity中 调用gpu 但无IMU?  https://discretetom.github.io/posts/hyper-desktop-duplication/
shremdup 通过 gRPC 和共享内存调用 Windows 桌面复制 API。 与上面是分离的，包含下
rusty-duplication 调用 Windows 桌面复制 API 并管理共享内存的底层库 Rust实现？

    DesktopRenderer.cs 定义namespace及DesktopRenderer，是否可视化
    Example.cs--
    HDD_Manager.cs定义ns 绑定host、获取共享内存的信息并调用monitor的setup创建
    HDD_Monitor.cs定义ns 绑定scale 仅定义HDD_Monitor类 渲染鼠标及屏幕
    Shremdup.cs 不晓得，共享内存用
    ShremdupGrpc.cs
    Utils.cs logger manager和monitor用到了
Info IsPrimary 长宽
*表示用到了HyperDesktopDuplication（定义在DesktopRenderer中
-- 表示使用到start
Scr
│  App.cs* 其他退出进入操作
│  Config.cs
│  HelpText.cs-- c
│  MonitorControl.cs*-- 鼠标操作及部分控制
│  MonitorManager.cs*-- 按照cfg调整屏幕
│  TipText.cs-- c
│  XRCamera.cs --根据IMU调整相机 c
└─DLL
        TransformProvider.cs imu dll转换
        Win32.cs 获取屏幕


Rokid usb 解包
read_control(
    &self,
    request_type: u8,
    request: u8,
    value: u16,
    index: u16,
    buf: &mut [u8],
    timeout: Duration
)
self.device_handle.read_control(
            request_type(
                rusb::Direction::In,
                rusb::RequestType::Vendor,
                rusb::Recipient::Device,
            ),
            0x81,
            0x100,#64(长度)
            0,
            &mut result,
            TIMEOUT,
        )

https://voidcomputing.hu/blog/good-bad-ugly/
在设备管理器的对应com口的硬件信息可看到vid为0x04d2,pid为162f
dll导入 放入assert里就能using
要用libusb读取前得用zadig的格式化，但可能原有驱动不能使了，原有设备驱动为hidusb v10.0.19041.3636
https://libusbdotnet.sourceforge.net/V2/Index.html
blog中的acc和gyro数据标反了，ar-driver里的是对的

idd_instructions.txt