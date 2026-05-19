using System;
using DT.UniStart;
using UnityEngine;

public class XRCamera : CBC {
  void Start() {
    var eb = this.Get<IEventBus>();
    TransformProvider transformProvider = new TransformProvider();
    // manage transform provider lifecycle
    transformProvider.TP_Init();
    this.onDestroy.AddListener(transformProvider.TP_Shutdown);

    var initPosition = this.transform.position;
    var positionOffset = Vector3.zero;
    var rotationOffset = Quaternion.identity;
    Quaternion quat = Quaternion.identity;
    this.onUpdate.AddListener(() => {
      // update the camera position & rotation from transform provider
      unsafe {
        quat = transformProvider.TP_GetRotation();
      }

      this.transform.rotation = Quaternion.Inverse(Quaternion.Inverse(rotationOffset) * (quat));

      // ctrl + R to reset
      if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R)) {
        positionOffset = this.transform.position - initPosition;
        rotationOffset = quat;
        eb.Invoke("tip", "Reset View");
      }
    });
  }
}
