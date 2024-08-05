using UnityEngine;

public class MainSceneInputObject : MonoBehaviour
{
    public GameObject _shakingCamera;

    private void OnEnable()
    {
        UI_Main main = UIManager.Instance.MainUI as UI_Main;
        if(main != null)
        {
            main._inputObject = gameObject;
            main._shakingCamera = _shakingCamera;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(UIManager.Instance.IsPopupUIActive())
            {
                UIManager.Instance.OnDeactivePopupUI();
            }
        }
    }
}
