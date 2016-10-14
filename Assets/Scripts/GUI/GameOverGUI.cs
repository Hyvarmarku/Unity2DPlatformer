using UnityEngine;
using System.Collections;

namespace GameProgramming2D.GUI
{
    public class GameOverGUI : SceneGUI
    {
        public void OnStartGamePress()
        {
            GameManager.Instance.StateManager.PerformTransition(State.TransitionType.GameOverToGame);
        }

        public void OnMainMenuPress()
        {
            GameManager.Instance.StateManager.PerformTransition(State.TransitionType.GameOverToMainMenu);
        }
    }
}