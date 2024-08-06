using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] GameObject _StartCanvas;
    private void Start()
    {
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
