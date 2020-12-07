using System;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    public InputField idTest;
    public Button login;

    public static string myId;

    private void Start()
    {
        idTest = GameObject.Find("id").GetComponent<InputField>();
        login = GameObject.Find("登入").GetComponent<Button>();

        login.onClick.AddListener(Login);
        NetManager.NetConfig("127.0.0.1", 8888);
        NetManager.Update();
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, OnConnectSucc);
        NetManager.AddMsgListener("MsgInstance", OnMsgInstance);
        //NetManager.AddMsgListener("MsgMove", OnMsgMove);
        NetManager.AddMsgListener("MsgMoveTest", OnMsgMoveTest);
    }

    private void OnMsgMoveTest(MsgBase msgBase)
    {
        MsgMoveTest msg = msgBase as MsgMoveTest;
        Debug.LogError("-------收到移动消息" + msg.id);
        CtrlMove syncMove = ConnectManager.FindPlayer(msg.id).GetComponent<CtrlMove>();

        syncMove.SyncMove(msg.v, msg.h);
    }

    /// <summary>
    /// 收到同步协议
    /// </summary>
    /// <param name="msgBase"></param>
    public  void OnMsgMove(MsgBase msgBase)
    {
        MsgMove msg = msgBase as MsgMove;
        if (msg.id==myId)
        {
            //不同步自己
            return;
        }
        Debug.LogError("-------收到移动消息" + msg.id);
        //根据id 拿到对应得到syncHuman 从而控制
        //SyncHuman syncHuman = ConnectManager.FindPlayer(msg.id).GetComponent<SyncHuman>();
        SyncMove syncMove= ConnectManager.FindPlayer(msg.id).GetComponent<SyncMove>();
        //移动同步
        //syncHuman.SyncPos(msg);
        syncMove.SyncPos(msg);
    }

    /// <summary>
    /// 登录，发送实例化角色请求
    /// </summary>
    private void Login()
    {
        if (idTest.text != null && idTest.text != "")
        {
            Debug.Log("发送实例化角色请求:id1=" + idTest.text);
            myId = idTest.text;
            MsgInstance msgInstance = new MsgInstance();

            //player.id= myId;
            //player.x = 0;
            //player.y = 0;
            //player.z = 0;
            //player.ex = 0;
            //player.ey = 0;
            //player.ez = 0;
            msgInstance.id = myId;

            Debug.Log(msgInstance.ToString());
            NetManager.Send(msgInstance);

            Debug.Log("发送实例化角色请求:id2=" + msgInstance.id);
        }
    }

    private void OnMsgInstance(MsgBase msgBase)
    {
        Debug.Log("收到服务器消息，开始根据服务器返回的数据实例化角色");
        MsgInstance msg = msgBase as MsgInstance;

        if (msg.ids.players.Count != 0)
        {
            for (int i = 0; i < msg.ids.players.Count; i++)
            {
                if (ConnectManager.players.ContainsKey(msg.ids.players[i]))
                {
                    Debug.Log("实例化玩家已经存在:id=" + msg.ids.players[i] + "不添加");
                    continue;
                   
                }

                if (msg.ids.players[i] != myId)
                {
                    Debug.Log("实例化非玩家角色:id=" + msg.ids.players[i] + ";同时添加到玩家列表");

                    GameObject game = ResMgr.Load<GameObject>("Capsule");

                    ConnectManager.AddPlayer(msg.ids.players[i], game);

                    //ResMgr.LoadAsync<GameObject>("SyncHuman", delegate (GameObject game) //可用lambada 表达式将循环变量传到函数里面
                    //{
                    //    Debug.Log("异步加载角色完成1");
                    //});

                    // ConnectManager.AddPlayer(msg.ids.players[i],)
                }
                else
                {
                    Debug.Log("实例化玩家:id=" + msg.ids.players[i] + ";同时添加到玩家列表");
                    GameObject game = ResMgr.Load<GameObject>("Capsule");
                    ConnectManager.AddPlayer(msg.ids.players[i], game);

                    //ResMgr.LoadAsync<GameObject>("CtrlHuman", delegate (GameObject game)
                    //{
                    //    Debug.Log("异步加载角色完成2");
                    //});
                }
            }
        }
    }

    //private void InstanceSucc(GameObject arg0)
    //{
    //    Debug.Log("异步加载角色完成");
    //    ConnectManager.AddPlayer(msg.ids.players[i], game);
    //}

    private void OnConnectSucc(string err)
    {
        Debug.Log("成功连接,执行回调函数");
        MsgPing msg = new MsgPing();
        NetManager.Send(msg);
    }

    private void Update()
    {
        NetManager.Update();
    }
}