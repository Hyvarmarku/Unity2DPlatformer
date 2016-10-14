using UnityEngine;
using System.Collections;
using GameProgramming2D.State;

namespace GameProgramming2D.GUI
{
    public class GUIManager : MonoBehaviour
    {

        [SerializeField]
        private Dialog _dialogPrefab;
        public SceneGUI sceneGUI { get; private set; }


        public void Init()
        {
            GameManager.Instance.StateManager.StateLoaded += HandleStateLoaded;
            sceneGUI = FindObjectOfType<SceneGUI>();
        }

        void OnDisable()
        {
            GameManager.Instance.StateManager.StateLoaded -= HandleStateLoaded;
        }

        private void HandleStateLoaded(StateType type)
        {
            sceneGUI = FindObjectOfType<SceneGUI>();

            if (sceneGUI == null)
            {
                Debug.LogWarning("Could not find a SceneGUI component from loaded scene. Is this intentional?");
            }
        }

        public Dialog CreateDialog()
        {
            Dialog dialog = Instantiate(_dialogPrefab);
            dialog.transform.SetParent(sceneGUI.transform);
            dialog.transform.localPosition = Vector3.zero;
            dialog.transform.SetAsLastSibling();

            return dialog;
        }
    }
}
