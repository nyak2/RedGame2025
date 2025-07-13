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
    private Capsule _capsuleInstance;
    private int _currentTierNum;

    private void Awake()
    {
        _touchAction = new InputAction(
            type: InputActionType.PassThrough,
            binding: "<Touchscreen>/touch*/press"
            );
        _touchAction.AddBinding("<Pointer>/press");
        _touchAction.Enable();
        _spawnPoint = _spawnPoints[0].position;
        _currentTierNum = 0;
    }

    private void OnEnable()
    {
        InitializeCapsule();
    }

    public void InitializeCapsule()
    {
        _capsuleInstance = null;
        Capsule capsuleInstance = Instantiate(_capsulePrefab, transform);
        _capsuleInstance = capsuleInstance;
        ChangeCapsuleRbTo(RigidbodyType2D.Static);

        switch (_capsuleInstance.GetTier())
        {
            case CapsuleTier.Tier.One:
                {
                    _spawnPoint = _spawnPoints[0].position;
                    _currentTierNum = 0;
                }
                break;

            case CapsuleTier.Tier.Two:
                {
                    _spawnPoint = _spawnPoints[1].position;
                    _currentTierNum = 1;
                }
                break;

            case CapsuleTier.Tier.Three:
                {
                    _spawnPoint = _spawnPoints[2].position;
                    _currentTierNum = 2;
                }
                break;
        }
        capsuleInstance.transform.position = _spawnPoint;
    }

    private void Update()
    {
        if (_capsuleInstance == null)
        {
            return;
        }

        _spawnPoint = _spawnPoints[_currentTierNum].position;

        if (IsReleased())
        {
            Drop();
        }

        if(_capsuleInstance._isLanded)
        {
            if (_capsuleInstance != null)
            {
                _capsuleInstance.tag = "Capsule";
            }
            _deathLineObject.EnableLine();
            _gameManager.InvokeOnDroppedEvent();
        }
    }

    private void Drop()
    {
        _deathLineObject.DisableLine();
        _capsuleInstance.transform.parent = null;
        ChangeCapsuleRbTo(RigidbodyType2D.Dynamic);
    }

    private void ChangeCapsuleRbTo(RigidbodyType2D type)
    {
        _capsuleInstance.GetComponent<Rigidbody2D>().bodyType = type;
    }

    private bool IsReleased()
    {
        return _touchAction.WasReleasedThisFrame();
    }

    public void CanControl(bool canControl)
    {
        enabled = canControl;
    }
}
