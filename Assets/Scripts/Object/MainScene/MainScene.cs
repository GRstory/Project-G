using UnityEngine;

public class MainScene : MonoBehaviour
{
    public GameObject _shakingCamera;

    private void OnEnable()
    {
        _shakingCamera = GameObject.FindGameObjectWithTag("ShakingCamera");

        UIManager.Instance.ChangeStaticUI(UIManager.Instance.MainUI);
    }
}
