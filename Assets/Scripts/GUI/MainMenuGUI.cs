using UnityEngine;
using System.Collections;

namespace GameProgramming2D.GUI
{
    public class MainMenuGUI : MonoBehaviour
    {
        public void OnStartGamePress()
        {
            GameManager.Instance.StateManager.PerformTransition(State.TransitionType.MainMenuToGame);
        }

        public void OnExitGamePress()
        {
            Application.Quit();
        }
    }
}