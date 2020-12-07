// using System.Collections;
// using System.Collections.Generic;
// using System.Net.Sockets;
// using UnityEngine;

// namespace Assets.Scripts.SyncAttack
// {
//     public class NetControl : MonoBehaviour
//     {
//         //定义套接字
//         public static Socket socket;
//         //接收缓冲区
//         static byte[] readBuff = new byte[1024];
//         //定义委托
//         public delegate void MsgListener(string str);

//         //监听列表
//         private static Dictionary<string, MsgListener> listeners = new Dictionary<string, MsgListener>();

//         //消息列表
//         static List<string> msgList = new List<string>();

//         /// <summary>
//         /// 添加监听
//         /// </summary>
//         /// <param name="msgname">协议名</param>
//         /// <param name="listener">监听对象</param>
//         public static void AddListener(string msgname,MsgListener listener)
//         {
//             listeners.Add(msgname, listener);
            
//         }
//         /// <summary>
//         /// 获取socket传递的内容
//         /// </summary>
//         /// <returns></returns>
//         public static string GetContent()
//         {
//             if (socket!= null)
//             {
//                 return socket.LocalEndPoint.ToString();
//             }
//             else
//             {
//                 return null;
//             }

//         }

//         public static void Connect(string ip,int port)
//         {
//             socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//             //todo 同步连接
//             socket.Connect(ip, port);

//         }

//     }
// }