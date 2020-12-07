using System.Collections;

/// <summary>
/// 状态同步
/// </summary>
public class MsgMove : MsgBase
{
    public MsgMove() : base("MsgMove")
    {
    }
    /// <summary>
    /// 由服务端发，客户端根据id对指定对象操作
    /// </summary>
    public string id = "";

    public float x = 0;
    public float y = 0;
    public float z = 0;

    public float ex = 0;
    public float ey = 0;
    public float ez = 0;


}

