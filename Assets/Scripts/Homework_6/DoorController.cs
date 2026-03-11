using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float TorqueForce = 150f;
    private HingeJoint _hinge;
    private bool _isOpen = false;

    private void Start()
    {
        _hinge = GetComponent<HingeJoint>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _isOpen = !_isOpen;

            JointMotor motor = _hinge.motor;
            motor.force = TorqueForce;
            motor.targetVelocity = _isOpen ? 60f : -60f;
            motor.freeSpin = false;

            _hinge.motor = motor;
            _hinge.useMotor = true;
        }
    }
}