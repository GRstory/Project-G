using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MainScene : SceneController
{
    public GameObject _test;
    public char[] charArray = { 'a', 'b', 'c'};
    protected override void OnEnable()
    {
        base.OnEnable();
        UIManager.Instance.ChangeStaticUI(UIManager.Instance.MainUI);
        SoundManager.Instance.PlaySound2D("MainBGM", 0, true, GameEnum.SoundType.BGM);
    }


    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _test.transform.position = pos;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        test(ref charArray);
        test();
    }

    private void test(ref char[] c)
    {
        char[] tmp = new char[c.Length];

        for(int i = 0; i < c.Length; i++)
        {
            tmp[i] = c[c.Length - i - 1];
        }

        c = tmp;
    }

    private void test()
    {
        ClassA.a = 2;
        int a = ClassA.a;
    }
}

public static class ClassA
{
    public static int a = 1;
}
