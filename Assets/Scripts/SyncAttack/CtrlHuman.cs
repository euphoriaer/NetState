using UnityEngine;

public class CtrlHuman : BaseSimpleSampleCharacterControl
{
    /// <summary>
    /// 同步时间
    /// </summary>
    private float lastSendSyncTime = 0;

    /// <summary>
    /// 同步帧率
    /// </summary>
    public static float syncInterval = 0.1f;

    private void Awake()
    {
        base.Awake();
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        walk = Input.GetKey(KeyCode.LeftShift);
        //error 1.指令同步，所有指令都发给服务器，再由服务器分发下来，客户端根据服务器获取需要移动的对象（网卡了，人物无法移动型，或者出现瞬移（状态同步），）
        //error 2.本地操作的只同步位置信息类型，只有非玩家对象由服务器控制（网卡了，不影响个人操作，随便跑，随便打）
    }

    public void Update()
    {
        if (!m_jumpInput && Input.GetKey(KeyCode.Space))
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
        base.Update();

        //发送同步信息
        SyncUpdate();
    }

    private void SyncUpdate()
    {
        if (Time.time - lastSendSyncTime < syncInterval)
        {
            return;
        }
        lastSendSyncTime = Time.time;
        //发送同步协议
        MsgMove msg = new MsgMove();
        msg.id = GameMain.myId;
        msg.x = this.gameObject.transform.position.x;
        msg.y = this.gameObject.transform.position.y;
        msg.z = this.gameObject.transform.position.z;

        msg.ex = this.gameObject.transform.eulerAngles.x;
        msg.ey = this.gameObject.transform.eulerAngles.y;
        msg.ez = this.gameObject.transform.eulerAngles.z;
        NetManager.Send(msg);
    }

    public void FixedUpdate()
    {
        CtrlInput();
        base.FixedUpdate();
        DirectUpdate();
    }

    public void CtrlInput()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        walk = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
        }
    }
}