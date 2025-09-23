using UnityEngine;

public interface IMotionController
{
    public Vector2 GetStick();

    public Quaternion GetOrientation();

    public Vector3 GetAccelerationLocal();

    public Vector3 GetAccelerationWorld();

    public Vector3 GetAccelerationWorldWithoutGravity();
}
