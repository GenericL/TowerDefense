using System;
using UnityEngine;
using UnityEngine.Events;

public class Observer<T>
{
    [SerializeField] T value;
    [SerializeField] UnityEvent<T> onValueChanged;

    public T Value
    {
        get => value; 
        set => Set(value);
    }
    public Observer(T value = default, UnityAction<T> callback = null)
    {
        this.value = value;
        onValueChanged = new UnityEvent<T>();
        if (callback != null) onValueChanged.AddListener(callback);
    }

    public void Set(T value)
    {
        if(Equals(this.value, value)) return;
        this.value = value;
    }

    public void Invoke()
    {
        onValueChanged.Invoke(value);
    }
    public void Invoke(T value)
    {
        Set(value);
        Invoke();
    }

    public void AddListener(UnityAction<T> callback)
    {
        if (callback == null) return;
        if (onValueChanged == null) onValueChanged = new UnityEvent<T>();
        onValueChanged.AddListener(callback);
    }

    public void RemoveListener(UnityAction<T> callback)
    {
        if (callback == null) return;
        if (onValueChanged == null) onValueChanged = new UnityEvent<T>();
        onValueChanged.RemoveListener(callback);
    }

    public void RemoveAllListeners()
    {
        if (onValueChanged == null) return;

        onValueChanged.RemoveAllListeners();
    }

    public void Dispose()
    {
        RemoveAllListeners();
        onValueChanged = null;
        value = default;
    }
}
public class Observer<T,V>
{
    [SerializeField] T firstValue;
    [SerializeField] V secondValue;
    [SerializeField] UnityEvent<T,V> onValueChanged;

    public T Value
    {
        get => firstValue;
        set => Set(value);
    }
    public Observer(V secondValue = default, T firstValue = default,  UnityAction<T,V> callback = null)
    {
        this.firstValue = firstValue;
        this.secondValue = secondValue;
        onValueChanged = new UnityEvent<T,V>();
        if (callback != null) onValueChanged.AddListener(callback);
    }

    public void Set(T value)
    {
        if (Equals(this.firstValue, value)) return;
        this.firstValue = value;
    }

    public void Invoke()
    {
        onValueChanged.Invoke(firstValue, secondValue);
    }
    public void Invoke(T value)
    {
        Set(value);
        Invoke();
    }

    public void AddListener(UnityAction<T,V> callback)
    {
        if (callback == null) return;
        if (onValueChanged == null) onValueChanged = new UnityEvent<T,V>();
        onValueChanged.AddListener(callback);
    }

    public void RemoveListener(UnityAction<T,V> callback)
    {
        if (callback == null) return;
        if (onValueChanged == null) onValueChanged = new UnityEvent<T,V>();
        onValueChanged.RemoveListener(callback);
    }

    public void RemoveAllListeners()
    {
        if (onValueChanged == null) return;

        onValueChanged.RemoveAllListeners();
    }

    public void Dispose()
    {
        RemoveAllListeners();
        onValueChanged = null;
        firstValue = default;
        secondValue = default;
    }
}