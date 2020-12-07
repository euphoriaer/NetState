using UnityEngine;

public class CtrlMove : BaseMove
{
    public float v = 0;
    public float h = 0;
    public float speed = 2;
    public bool walk = false;

    public GameObject gameObject;

    private void Awake()
    {
    }

    private void Update()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        //NewMethod(v,h);
        if (v != 0)
        {
            //transform.Translate(-Vector3.forward * v * speed, Space.Self);
            //可用 transform.position += -(transform.forward * v )* speed * Time.deltaTime;
            //gameObject.gameObject.transform.position.Set(speed * v * Time.deltaTime * 100, transform.position.y, transform.position.z);

            //发送同步信息
            SyncUpdate2(v, h);
        }
        if (h != 0)
        {
            //transform.Translate(Vector3.left * h * speed, Space.Self);
            //gameObject.gameObject.transform.position.Set(transform.position.x, transform.position.y, 100 * speed * Time.deltaTime * h);
            //发送同步信息
            SyncUpdate2(v, h);
        }
    }

    private void SyncUpdate2(float v, float h)
    {
        if (Time.time - lastSendSyncTime < syncInterval)
        {
            return;
        }
        lastSendSyncTime = Time.time;
        //发送同步协议
        MsgMoveTest msg = new MsgMoveTest();
        msg.id = GameMain.myId;
        msg.v = v;
        msg.h = h;
        NetManager.Send(msg);
    }

    /// <summary>
    /// 接收协议
    /// </summary>
    /// <param name="v"></param>
    /// <param name="h"></param>
    public void SyncMove(float v, float h)
    {
        if (v != 0)
        {
            transform.Translate(-Vector3.forward * v * speed, Space.Self);
            //可用 transform.position += -(transform.forward * v )* speed * Time.deltaTime;
            //gameObject.gameObject.transform.position.Set(speed * v * Time.deltaTime * 100, transform.position.y, transform.position.z);
        }
        if (h != 0)
        {
            transform.Translate(Vector3.left * h * speed, Space.Self);
            //gameObject.gameObject.transform.position.Set(transform.position.x, transform.position.y, 100 * speed * Time.deltaTime * h);
        }
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
        msg.x = transform.position.x;
        msg.y = transform.position.y;
        msg.z = transform.position.z;

        msg.ex = transform.eulerAngles.x;
        msg.ey = transform.eulerAngles.y;
        msg.ez = transform.eulerAngles.z;
        NetManager.Send(msg);
    }
}