using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

namespace GameProgramming2D
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }
                return _instance;
            }
        }

        private Pauser _pauser;
        private InputManager _inputManager;
        private PlayerControl _playerControl;

        public Pauser Pauser { get { return _pauser; } }
        public PlayerControl Player
        {
            get { return _playerControl; }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                Init();
            }
            else if (_instance != this)
            {
                Destroy(this);
            }
        }

        private void Init()
        {
            _pauser = gameObject.GetOrAddComponent<Pauser>();
            _inputManager = gameObject.GetOrAddComponent<InputManager>();
            _playerControl = FindObjectOfType<PlayerControl>();
        }

        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}