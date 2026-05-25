using System;
using System.Collections.Generic;

public static class EventCenter
{
    //事件 ：回调列表(支持多播)
    private static Dictionary<Type, Action<object>> eventDict = new();

    //缓存包装后的委托，确保取消订阅时回调函数的唯一性
    private static Dictionary<Delegate, Action<object>> wrapperMap = new();

    public static void Subscribe<T>(Action<T> callback)
    {
        Action<object> wrapper = (obj) => callback((T)obj); //对回调函数的包装
        wrapperMap[callback] = wrapper;

        Type type = typeof(T);
        if (eventDict.ContainsKey(type))
        {
            eventDict[type] += wrapper;
        }
        else
        {
            eventDict[type] = wrapper;
        }
    }

    public static void Unsubscribe<T>(Action<T> callback)
    {
        if (wrapperMap.TryGetValue(callback, out Action<object> wrapper))
        {
            Type type = typeof(T);
            eventDict[type] -= wrapper;
            wrapperMap.Remove(callback);
        }
    }

    public static void Publish<T>(T eventData)
    {
        Type type = typeof(T);
        if (eventDict.TryGetValue(type, out Action<object> action))
        {
            action?.Invoke(eventData);
        }
    }
}
