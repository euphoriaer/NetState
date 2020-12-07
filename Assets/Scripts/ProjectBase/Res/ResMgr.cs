using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 资源加载模块 1.异步加载 2.委托和lambda表达式，lambda表达式在使用中模块时，避免重复创建函数（异步加载要有一个委托函数，需要做的事的函数） 3.协程 4.泛型
/// </summary>
public class ResMgr : BaseManager<ResMgr>
{
    /// <summary>
    /// 同步加载资源（实例化返回）
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="path">Resources路径(类似：UI/EventSystem)</param>
    /// <returns></returns>
    public static T Load<T>(string path) where T : Object
    {
        T res = Resources.Load<T>(path);
        //如果对象是一个Gameobject类型的  实例化后  再返回  外部可以直接使用
        if (res is GameObject)
        {
            return GameObject.Instantiate(res);//实例化返回
        }
        else
            return res;
    }

    /// <summary>
    /// 同步加载资源（加载到内存中）
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="path">Resources路径(类似：UI/EventSystem)</param>
    /// <returns></returns>
    public static T Load2<T>(string path) where T : Object
    {
        T res = Resources.Load<T>(path);
        return res;
    }

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T">类型（一般为Gameobject）</typeparam>
    /// <param name="path">Resources路径(类似：UI/EventSystem)</param>
    /// <param name="callback">加载完成之后的回调函数</param>
    public static void LoadAsync<T>(string path, UnityAction<T> callback) where T : Object
    {
        Resources.LoadAsync(path);
        //开启异步加载的协程
        MonoMgr.Getinstate().StartCoroutine(ReallyLoadAsync(path, callback));
    }

    //真正的协同函数 用于 开启异步加载对应的资源
    private static IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;

        if (r.asset is GameObject)
        {
            callback(GameObject.Instantiate(r.asset) as T);
        }
        else
            callback(r.asset as T);
    }

    //public static void LoadAsync2<T>(string path, UnityAction<T> callback) where T : Object
    //{
    //    Resources.LoadAsync(path);
    //    //开启异步加载的协程
    //    MonoMgr.Getinstate().StartCoroutine(ReallyLoadAsync2(path, callback));
    //}

    ////真正的协同函数 用于 开启异步加载对应的资源
    //private static IEnumerator ReallyLoadAsync2<T>(string name, UnityAction<T> callback) where T : Object
    //{
    //    ResourceRequest r = Resources.LoadAsync<T>(name);
    //    yield return r;

    //    if (r.asset is GameObject)
    //    {
    //        callback(GameObject.Instantiate(r.asset) as T);
    //    }
    //    else
    //        callback(r.asset as T);
    //}
}
