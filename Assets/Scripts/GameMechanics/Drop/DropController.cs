using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DropController : MonoBehaviour
{
    [SerializeField] Capsule _capsulePrefab;
    [SerializeField] GameManager _gameManager;
    [SerializeField] DeathLine _deathLineObject;
    private InputAction _touchAction;
    [SerializeField] private List<Transform> _spawnPoints;
    private Vector3 _spawnPoint;
    private Capsule _currentCapsule;
    private bool _hasDroppedCurrent = false;

    private void Awake()
    {
        _touchAction = new InputAction(
            type: InputActionType.PassThrough,
            binding: "<Touchscreen>/touch*/press"
            );
        _touchAction.AddBinding("<Pointer>/press");
        _spawnPoint = _spawnPoints[0].position;
    }

    private void OnEnable()
    {
        _touchAction.performed += OnTouchPerformed;
        _touchAction.Enable();

        InitializeCapsule();
    }

    private void OnDisable()
    {
        _touchAction.performed -= OnTouchPerformed;
        _touchAction.Disable();
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        if (_hasDroppedCurrent)
        {
            return;
        }

        _hasDroppedCurrent = true;
        Drop(_currentCapsule);
    }

    public void InitializeCapsule()
    {
        if (_currentCapsule != null)
        {
            return;
        }

        Capsule capsuleInstance = Instantiate(_capsulePrefab, transform);
        _currentCapsule = capsuleInstance;
        capsuleInstance.OnLanded += OnCapsuleLanded;
        ChangeCapsuleRbTo(capsuleInstance, RigidbodyType2D.Static);

        switch (capsuleInstance.GetTier())
        {
            case CapsuleTier.Tier.One:
                {
                    _spawnPoint = _spawnPoints[0].position;
                }
                break;

            case CapsuleTier.Tier.Two:
                {
                    _spawnPoint = _spawnPoints[1].position;
                }
                break;

            case CapsuleTier.Tier.Three:
                {
                    _spawnPoint = _spawnPoints[2].position;
                }
                break;
        }

        capsuleInstance.transform.position = _spawnPoint;
    }

    private void OnCapsuleLanded(Capsule capsule)
    {
        if (capsule == null)
        {
            return;
        }

        // Clean up
        capsule.OnLanded -= OnCapsuleLanded;
        _currentCapsule = null;
        _hasDroppedCurrent = false;

        _deathLineObject.EnableLine();
        _gameManager.InvokeOnDroppedEvent();
    }

    private void Drop(Capsule capsule)
    {
        if (capsule == null)
        {
            return;
        }

        _deathLineObject.DisableLine();
        capsule.transform.parent = null;
        ChangeCapsuleRbTo(capsule, RigidbodyType2D.Dynamic);
    }

    private void ChangeCapsuleRbTo(Capsule capsule, RigidbodyType2D type)
    {
        if (!capsule.TryGetComponent<Rigidbody2D>(out var rb))
        {
            return;
        }
        rb.bodyType = type;
    }

    public void CanControl(bool canControl)
    {
        enabled = canControl;
    }
}
