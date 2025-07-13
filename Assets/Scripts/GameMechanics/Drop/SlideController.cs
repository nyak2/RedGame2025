using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class SlideController : MonoBehaviour
{
    [SerializeField] private List<Transform> _limitPos;
    private InputAction _touchAction;
    private TouchControl _touchControl;
    private Camera _camera;
    private float _sliderYPos;
    private float _sliderZPos;
    private Transform _sliderTransform;

    private void Awake()
    {
        _sliderTransform = transform;
        _sliderYPos = _sliderTransform.position.y;
        _sliderZPos = _sliderTransform.position.z;

        _touchAction = new InputAction(
            type: InputActionType.Value,
            binding: "<Touchscreen>/touch*/position"
        );
        _touchAction.AddBinding("<Pointer>/press");
        _touchAction.AddBinding("<Pointer>/position");
        _touchAction.Enable();
        // Add both started and performed callbacks
        _touchAction.performed += OnTouchPerformed;

        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _touchAction.performed += OnTouchPerformed;
        _touchAction.Enable();
        _touchControl = Touchscreen.current?.primaryTouch;
    }

    private void OnDisable()
    {
        _touchAction.performed -= OnTouchPerformed;
        _touchAction.Disable();
    }

    public void CanControl(bool canControl)
    {
        enabled = canControl;
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        if (_touchControl == null) _touchControl = Touchscreen.current?.primaryTouch;
        if (_touchControl == null) return;

        Vector2 touchPosition = _touchControl.position.ReadValue();
        Vector3 moveTo = _camera.ScreenToWorldPoint(new Vector3(touchPosition.x, _sliderYPos, _camera.nearClipPlane));
        moveTo.y = _sliderYPos;
        moveTo.z = _sliderZPos;
        _sliderTransform.position = moveTo;

        // Clamp position if you have limits
        if (_limitPos != null && _limitPos.Count >= 2)
        {
            Vector3 clampedPosition = _sliderTransform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, _limitPos[0].position.x, _limitPos[1].position.x);
            _sliderTransform.position = clampedPosition;
        }
    }
}