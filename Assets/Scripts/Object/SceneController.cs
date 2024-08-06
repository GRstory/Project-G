using System.Collections;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    protected virtual void OnEnable()
    {
        FlowManager.Instance.Player.transform.position = _spawnPoint.position;
        FlowManager.Instance.Player.transform.rotation = _spawnPoint.rotation;
        StartCoroutine(PlayerSpawnPointCoroutine());
    }

    private IEnumerator PlayerSpawnPointCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        FlowManager.Instance.Player.transform.position = _spawnPoint.position;
        FlowManager.Instance.Player.transform.rotation = _spawnPoint.rotation;
    }
}
