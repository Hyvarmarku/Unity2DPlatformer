﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GameProgramming2D.State
{
    public class GameOverState : StateBase
    {
        public GameOverState() : base()
        {
            State = StateType.GameOver;
            AddTransition(TransitionType.GameOverToGame, StateType.Game);
            AddTransition(TransitionType.GameOverToMainMenu, StateType.MainMenu);
        }
        public override void StateActivated()
        {
            GameManager.Instance.SceneLoaded += HandleSceneLoaded;
            SceneManager.LoadScene(2);
        }
    }
}