using System;
using System.Collections.Generic;
using UnityEngine;

public class ConnectManager
{
    /// <summary>
    /// 玩家列表
    /// </summary>
    public static Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    /// <summary>
    /// 消息列表
    /// </summary>
    public static List<string> msgs = new List<string>();
    /// <summary>
    /// 添加对象
    /// </summary>
    /// <param name="id"></param>
    /// <param name="player"></param>
    public static void AddPlayer(string id, GameObject player)
    {
        if (!players.ContainsKey(id))
        {
            players.Add(id, player);
        }

    }
    /// <summary>
    /// 移除对象
    /// </summary>
    /// <param name="id"></param>
    public static void RemovePlayer(string id)
    {
        if (players.ContainsKey(id))
        {
            players.Remove(id);
        }
        else
        {
            Console.WriteLine("要删除的玩家不存在");
        }
    }

    /// <summary>
    /// 查找玩家
    /// </summary>
    /// <param name="id">ID</param>
    public static GameObject FindPlayer(string id)
    {
        if (players.ContainsKey(id))
        {
            return players[id];
        }
        Debug.Log("不存在此玩家");
        return null;
    }


    ///// <summary>
    ///// 广播消息(全体)
    ///// </summary>
    ///// <param name="msg"></param>
    //public static void DistributeMsg(MsgBase msg)
    //{
    //    foreach (var player in players.Values)
    //    {
    //        player.Send(msg);
    //    }
    //}


}