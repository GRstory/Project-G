using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

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
        yield return new WaitForSecondsRealtime(1.5f);

        SceneManager.LoadScene("MainScene");
    }
    public override bool IsClearScene()
    {
        return true;
    }
}
