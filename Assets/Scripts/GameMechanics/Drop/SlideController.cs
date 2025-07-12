using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class SlideController : MonoBehaviour
{
    private Rigidbody2D _slider;
    [SerializeField] private float _speed = 5.0f;
    private InputAction _touchAction;
    private Vector3 _referencePoint;
    private TouchControl _touchControl;
    private Camera _camera;
    private float _sliderYPos;
    private float _sliderZPos;

    private void Awake()
    {
        _slider = GetComponent<Rigidbody2D>();
        _sliderYPos = _slider.gameObject.transform.position.y;
        _sliderZPos = _slider.gameObject.transform.position.z;

        _touchAction = new InputAction(
            type: InputActionType.PassThrough,
            binding: "<Touchscreen>/touch*/press"
            );
        _touchAction.AddBinding("<Pointer>/press");
        _touchAction.Enable();
        _touchAction.started += OnTouchStarted;
        _touchControl = Touchscreen.current.primaryTouch;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!_touchControl.IsActuated())
        {
            return;
        }

        Vector2 currTouchPosition = _touchControl.position.ReadValue();
        int swipeMagnitude = GetSwipeMagnitude(currTouchPosition, _referencePoint);
        Vector3 moveDirection = new(swipeMagnitude * _speed, 0.0f, 0.0f);
        _slider.velocity = moveDirection;
        _referencePoint = currTouchPosition;
    }

    private int GetSwipeMagnitude(Vector2 currTouchPos, Vector2 referencePos)
    {
        float xDiff = currTouchPos.x - referencePos.x;
        if (xDiff > 0)
        {
            return 1;
        }
        else if (xDiff < 0)
        {
            return -1;
        }
        return 0;
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = _touchControl.position.ReadValue();
        Vector3 moveTo = _camera.ScreenToWorldPoint(new(touchPosition.x, 0.0f, 0.0f));
        moveTo.y = _sliderYPos;
        moveTo.z = _sliderZPos;
        _slider.gameObject.transform.position = moveTo;
        _referencePoint = touchPosition;
    }
}
