using System.Collections.Generic;

public class MsgInstance : MsgBase
{
    public MsgInstance() : base("MsgInstance")
    {
       
    }

    //public Player player;

    /// <summary>
    /// �ͻ�����
    /// </summary>
    public string id="";

    // public string id = "";
    /// <summary>
    /// ����˻�
    /// </summary>
    public IDs ids;
}

public class IDs
{
    public List<string> players;
}
