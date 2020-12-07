using System.Collections.Generic;

public class MsgInstance : MsgBase
{
    public MsgInstance() : base("MsgInstance")
    {
       
    }

    //public Player player;

    /// <summary>
    /// 客户端送
    /// </summary>
    public string id="";

    // public string id = "";
    /// <summary>
    /// 服务端回
    /// </summary>
    public IDs ids;
}

public class IDs
{
    public List<string> players;
}
