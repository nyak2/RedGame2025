using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLine : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private BoxCollider2D _boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        DisableLine();
    }

    public void EnableLine()
    {
        _boxCollider.enabled = true;
    }

    public void DisableLine()
    {
        _boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Capsule"))
        {
            _gameManager.InvokeOnLoseEvent();
        }
    }
}
