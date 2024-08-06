using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : SceneController
{
    [SerializeField] GameObject _StartCanvas;

    protected override void OnEnable()
    {
        base.OnEnable();

        _StartCanvas.SetActive(true);
        FlowManager.Instance.LoadGame();
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        SceneManager.LoadScene("MainScene");
    }
}
