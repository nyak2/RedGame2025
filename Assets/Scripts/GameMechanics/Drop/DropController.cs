using UnityEngine;
using UnityEngine.InputSystem;

public class DropController : MonoBehaviour
{
    [SerializeField] Capsule _capsulePrefab;
    private InputAction _touchAction;
    private Vector3 _spawnPoint;
    private Capsule _capsuleInstance;

    private void Awake()
    {
        _touchAction = new InputAction(
            type: InputActionType.PassThrough,
            binding: "<Touchscreen>/touch*/press"
            );
        _touchAction.AddBinding("<Pointer>/press");
        _touchAction.Enable();
        _spawnPoint = transform.Find("SpawnPoint").position;
        InitializeCapsule();
    }

    public void InitializeCapsule()
    {
        Capsule capsuleInstance = Instantiate(_capsulePrefab, transform);
        _capsuleInstance = capsuleInstance;
        ChangeCapsuleRbTo(RigidbodyType2D.Static);
        capsuleInstance.transform.position = _spawnPoint;
    }

    private void Update()
    {
        if (IsReleased())
        {
            Drop();
        }
    }

    private void Drop()
    {
        _capsuleInstance.transform.parent = null;
        ChangeCapsuleRbTo(RigidbodyType2D.Dynamic);
    }

    private void ChangeCapsuleRbTo(RigidbodyType2D type)
    {
        _capsuleInstance.GetComponent<Rigidbody2D>().bodyType = type;
    }

    private bool IsTouched()
    {
        return _touchAction.triggered;
    }

    private bool IsReleased()
    {
        return _touchAction.WasReleasedThisFrame();
    }

}
