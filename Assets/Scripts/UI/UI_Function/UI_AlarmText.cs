using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AlarmText : MonoBehaviour
{
    static float _destroyTime = 2f;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(DestroyCoroutine());
    }

    public void SetAlarmMessage(string message)
    {
        GetComponentInChildren<TMP_Text>().text = message;
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSecondsRealtime(_destroyTime);
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;

            yield return null;
            if (time >= _destroyTime / 2)
                break;
        }

        while(true)
        {
            time += Time.deltaTime;
            yield return null;

            if (time > _destroyTime / 2)
            {
                _canvasGroup.alpha = 1 - (time - (_destroyTime / 2)) / (_destroyTime / 2);
            }
            if (time >= _destroyTime)
                break;
        }

        EventHandler.CallExcludeAlarmMessageEvent();
        Destroy(gameObject);
    }
}
