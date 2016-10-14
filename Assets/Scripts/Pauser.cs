using UnityEngine;
using System.Collections;
using GameProgramming2D.GUI;

namespace GameProgramming2D
{
    public class Pauser : MonoBehaviour
    {
        private bool paused = false;
        private Dialog _pauseDialog;

        public bool isPaused { get { return paused; } }

        public void SetPauseOn()
        {
            Time.timeScale = 0;

            if (_pauseDialog == null)
            {
                _pauseDialog = GameManager.Instance.guiManager.CreateDialog();
                _pauseDialog.SetHeadline("Pause");
                _pauseDialog.SetText("The game is now paused. Press continue to continue game");
                _pauseDialog.SetShowCancel(false);
                _pauseDialog.SetOkButtonText("Continue");
                _pauseDialog.SetOnOKClicked(ContinueGame);
            }
            _pauseDialog.Show();
            paused = true;
        }

        public void ContinueGame()
        {
            Time.timeScale = 1;
            Debug.Log("Continue");
            paused = false;
        }

        public void TogglePause()
        {

            paused = !paused;

            if (paused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

}