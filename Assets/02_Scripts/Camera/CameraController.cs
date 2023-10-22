using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : CinemachineExtension
{
    [SerializeField]
    private float horizontalSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 10f;

    [SerializeField]
    private float clampAngle = 80f;

    private InputManager _inputManager;
    private Vector3 _startingRotation;
    private PlayerController _playerController;

    protected override void Awake()
    {
        _inputManager = InputManager.Instance;
        _playerController = FindObjectOfType<PlayerController>();
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow && _inputManager != null && !_playerController.IsDead)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if(_startingRotation == null)
                {
                    _startingRotation = transform.transform.eulerAngles;
                }

                Vector2 deltaInput = _inputManager.GetMouseDelta();

                _startingRotation.x += deltaInput.x * verticalSpeed * Time.fixedDeltaTime;
                _startingRotation.y += deltaInput.y * horizontalSpeed * Time.fixedDeltaTime;
                _startingRotation.y = Mathf.Clamp(_startingRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f);
            }
        }
    }
}
