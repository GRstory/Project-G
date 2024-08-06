using UnityEngine;

public class TestScene : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    private void OnEnable()
    {
        FlowManager.Instance.Player.transform.position = _spawnPoint.position;
    }
}
