public class Player
{
    /// <summary>
    /// id
    /// </summary>
    public string id = "154";

    /// <summary>
    /// 坐标和旋转
    /// </summary>
    public float x;
    public float y;
    public float z;

    public float ex;
    public float ey;
    public float ez;



    /// <summary>
    /// 发送信息
    /// </summary>
    /// <param name="msgBase"></param>
    public void Send(MsgBase msgBase)
    {
        NetManager.Send(msgBase);
    }
}