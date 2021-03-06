﻿using UnityEngine;
using System.Collections;

namespace GameProgramming2D {
    public class InputManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                GameManager.Instance.Save();
            }

            if (Input.GetKeyUp(KeyCode.P))
            {
                GameManager.Instance.Pauser.SetPauseOn();
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                GameManager.Instance.QuitGame();
            }

            UpdatePlayerKeys();
        }

        private void UpdatePlayerKeys()
        {
            if (GameManager.Instance.Player == null || GameManager.Instance.Pauser.isPaused)
                return;

            if (Input.GetButtonDown("Jump"))
            {
                GameManager.Instance.Player.Jump = true;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                GameManager.Instance.Player.Gun.Fire();
            }
            if (Input.GetButtonDown("Fire2"))
            {
                GameManager.Instance.Player.LayBomb();
            }

            var horizontal = Input.GetAxis("Horizontal");
            GameManager.Instance.Player.SetHorizontal(horizontal);
        }
    }
}
