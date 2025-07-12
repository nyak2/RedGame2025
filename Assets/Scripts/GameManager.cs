using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DropController _dropController;
    [SerializeField] private SlideController _slideController;
    [SerializeField] private GameObject _gameOverScreenObject;
    public delegate void OnDropped();
    public event OnDropped OnDroppedEvent;

    public delegate void OnLose();
    public event OnLose OnLoseEvent;
    // Start is called before the first frame update
    void Start()
    {
        _dropController.InitializeCapsule();
        OnDroppedEvent += SpawnNewCapsule;
        OnLoseEvent += ShowGameOverScreen;
    }

    private void OnDestroy()
    {
        OnDroppedEvent -= SpawnNewCapsule;
        OnLoseEvent -= ShowGameOverScreen;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnNewCapsule()
    {
        _dropController.InitializeCapsule();
    }

    public void InvokeOnDroppedEvent()
    {
        OnDroppedEvent?.Invoke();
    }

    public void ShowGameOverScreen()
    {
        _dropController.DisableControls();
        _slideController.DisableControls();
        _gameOverScreenObject.SetActive(true);
        //show screen here
    }

    public void InvokeOnLoseEvent()
    {
        OnLoseEvent?.Invoke();
    }

}
