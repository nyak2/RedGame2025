using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeneUIManager : MonoBehaviour
{
    [SerializeField] string _gameScenename;
    public void PlayGame()
    {
        SceneManager.LoadScene(_gameScenename);
    }
}
