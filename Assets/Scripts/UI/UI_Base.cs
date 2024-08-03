using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }

        int a = 1;
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected int GetCountOfType<T>() where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return 0;
        return objects.Length;
    }

    protected void AddAllTextToLocalizeStringEvent()
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(TMP_Text), out objects) == false)
            return;

        for(int i = 0; i < objects.Length; i++)
        {
            TMP_Text textComponent = Get<TMP_Text>(i);
            LocalizeStringEvent eventComponent = textComponent.transform.gameObject.GetOrAddComponent<LocalizeStringEvent>();

            UnityEngine.Localization.Events.UnityEventString eventString = new UnityEngine.Localization.Events.UnityEventString();
            eventString.AddListener(textComponent.SetText);
            eventComponent.OnUpdateString = eventString;
            eventComponent.StringReference.TableReference = "UI Table";
            eventComponent.StringReference.TableEntryReference = textComponent.name;
        }
    }

    protected void AddAllImageToLocalizeSpriteEvent(string tableName)
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(Image), out objects) == false)
            return;

        for(int i = 0; i < objects.Length; i++)
        {
            Image imageComponent = Get<Image>(i);
            LocalizeSpriteEvent eventComponent = imageComponent.transform.gameObject.GetOrAddComponent<LocalizeSpriteEvent>();

            eventComponent.AssetReference.TableReference = tableName;
            eventComponent.AssetReference.TableEntryReference = imageComponent.name;
        }
    }

    protected void BindAllButtonToOnClickFunc()
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(Button), out objects) == false)
            return;

        for(int i = 0;i < objects.Length; i++)
        {
            Button buttonComp = Get<Button>(i);
            MethodInfo methodInfo = GetType().GetMethod(buttonComp.name);
            if (methodInfo != null)
            {
                UnityEngine.Events.UnityAction action = (UnityEngine.Events.UnityAction)Delegate.CreateDelegate(typeof(UnityEngine.Events.UnityAction), this, methodInfo);
                buttonComp.onClick.AddListener(action);
            }
        }
    }
}
