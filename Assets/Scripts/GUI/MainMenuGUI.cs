using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameProgramming2D.GUI
{
    public class MainMenuGUI : MonoBehaviour
    {
        [SerializeField]
        private Button _loadButton;

        void Awake()
        {
            _loadButton.interactable = SaveSystem.DoesSaveExist();
        }

        public void OnStartGamePress()
        {
            GameManager.Instance.StateManager.PerformTransition(State.TransitionType.MainMenuToGame);
        }

        public void OnLoadGamePress()
        {
            GameManager.Instance.LoadGame();
        }

        public void OnExitGamePress()
        {
            Application.Quit();
        }

    }
}