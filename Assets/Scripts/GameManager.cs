﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using GameProgramming2D.State;
using System.Collections.Generic;
using GameProgramming2D.GUI;

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

        public delegate void SceneLoadedDelegate(int sceneIndex);
        public event SceneLoadedDelegate SceneLoaded;

        private Pauser _pauser;
        private InputManager _inputManager;
        private PlayerControl _playerControl;
        private List<Enemy> _enemies = new List<Enemy>();

        [SerializeField]
        private Enemy _enemyWithShip;
        [SerializeField]
        private Enemy _enemyWithoutShip;

        [SerializeField]
        private GUIManager _guiManagerPrefab;

        public Pauser Pauser { get { return _pauser; } }
        public PlayerControl Player
        {
            get
            {
                if (_playerControl == null)
                {
                    _playerControl = FindObjectOfType<PlayerControl>();
                }
                return _playerControl;
            }
        }

        public GameStateManager StateManager { get; private set;}
        public GUIManager guiManager { get; private set; }


        private void Awake()
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
                Init();
            }
            else if (_instance != this)
            {
                Destroy(this);
            }
        }

        protected void OnLevelWasLoaded(int levelIndex)
        {
            if (SceneLoaded != null)
            {
                SceneLoaded(levelIndex);
            }
        }

        private void Init()
        {
            _pauser = gameObject.GetOrAddComponent<Pauser>();
            _inputManager = gameObject.GetOrAddComponent<InputManager>();
            _playerControl = FindObjectOfType<PlayerControl>();
            InitGameStateManager();
            InitGUIManager();
        }

        private void InitGUIManager()
        {
            guiManager = Instantiate(_guiManagerPrefab);
            guiManager.transform.SetParent(transform, true);
            guiManager.Init();
        }

        private void InitGameStateManager()
        {
            StateManager = new GameStateManager(new MenuState());
            StateManager.AddState(new GameState ());
            StateManager.AddState(new GameOverState());

        }

        public void QuitGame()
        {
            Dialog dialog = guiManager.CreateDialog();
            dialog.SetHeadline("Quit Game");
            dialog.SetText("Are you sure you want to quit game?");
            dialog.SetOnOKClicked(() => Application.Quit());
            dialog.SetOnCancelClicked();
            dialog.Show();
        }

        public void Reload()
        {
            GameManager.Instance.StateManager.PerformTransition(State.TransitionType.GameToGameOver);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Save()
        {
            GameData data = new GameData();

            data.PlayerData = new PlayerData();
            data.PlayerData.Health = Player.Health.health;
            data.PlayerData.FacingRight = Player.FacingRight;
            data.PlayerData.Position = Player.transform.position;
            data.PlayerData.Velocity = Player.Rigidbody.velocity;

            data.EnemyDatas = new List<EnemyData>();

            foreach (Enemy enemy in _enemies)
            {
                EnemyData enemyData = new EnemyData();

                enemyData.Health = enemy.HP;
                enemyData.Type = enemy.Type;
                enemyData.XScale = enemy.transform.localScale.x;
                enemyData.Position = enemy.transform.position;
                enemyData.Velocity = enemy.Rigidbody.velocity;

                data.EnemyDatas.Add(enemyData);
            }

            Score score = GameObject.FindObjectOfType<Score>();
            data.Score = score.CurrentScore;

            SaveSystem.Save(data);
        }

        public void LoadGame()
        {
            StateManager.StateLoaded += HandleStateLoaded;
            StateManager.PerformTransition(TransitionType.MainMenuToGame);
        }

        private void HandleStateLoaded(StateType type)
        {
            StateManager.StateLoaded -= HandleStateLoaded;

            if (type == StateType.Game)
            {
                GameData data = SaveSystem.Load<GameData>();

                var score = GameObject.FindObjectOfType<Score>();
                score.CurrentScore = data.Score;

                Player.transform.position =(Vector3) data.PlayerData.Position;
                Player.FacingRight = data.PlayerData.FacingRight;
                var playerScale = Player.transform.localScale;
                playerScale.x *= Player.FacingRight ? 1 : -1;
                Player.transform.localScale = playerScale;

                Player.Health.health = data.PlayerData.Health;
                Player.Rigidbody.velocity = (Vector2)data.PlayerData.Velocity;

                foreach (var enemyData in data.EnemyDatas)
                {
                    Enemy enemyPrefab = GetEnemyPrefab(enemyData.Type);
                    Enemy enemy = Instantiate(enemyPrefab);

                    enemy.transform.position = (Vector3)enemyData.Position;
                    enemy.Rigidbody.velocity = (Vector2)enemyData.Velocity;
                    enemy.HP = enemyData.Health;
                    Vector3 enemyScale = enemy.transform.localScale;
                    enemyScale.x = enemyData.XScale;
                    enemy.transform.localScale = enemyScale;
                }
            }
        }

        private Enemy GetEnemyPrefab(Enemy.EnemyType type)
        {
            if (type == Enemy.EnemyType.WithoutShip)
            {
                return _enemyWithoutShip;
            }
            else
            {
                return _enemyWithShip;
            }
        }

        public void AddEnemy(Enemy enemy)
        {
            if (_enemies.Contains(enemy))
                return;

            _enemies.Add(enemy);
        }

        public bool RemoveEnemy(Enemy enemy)
        {
            return _enemies.Remove(enemy);
        }
    }
}