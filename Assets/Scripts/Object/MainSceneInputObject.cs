using UnityEngine;

public class MainSceneInputObject : MonoBehaviour
{
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
