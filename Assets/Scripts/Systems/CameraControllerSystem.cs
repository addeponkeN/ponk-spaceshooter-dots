
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial class CameraControllerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var tomatoEntity = SystemAPI.GetSingletonEntity<EntityTag<TomatoMono>>();
        var tomatoScale = SystemAPI.GetComponent<LocalTransform>(tomatoEntity).Scale;

        var cam = CameraSingleton.Get;
        if (cam == null)
            return;

        var pos = (float)(SystemAPI.Time.ElapsedTime * cam.Speed);
        var height = cam.HeightAtScale(tomatoScale);
        var radius = cam.RadiusAtScale(tomatoScale);

        cam.transform.position = new UnityEngine.Vector3
        {
            x = Mathf.Cos(pos) * radius,
            y = height,
            z = Mathf.Sin(pos) * radius,
        };

        cam.transform.LookAt(Vector3.zero, Vector3.up);

    }
}