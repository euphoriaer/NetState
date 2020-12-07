using UnityEngine;

public class BaseMove : MonoBehaviour
{
    /// <summary>
    /// 同步时间
    /// </summary>
    public float lastSendSyncTime = 0;

    /// <summary>
    /// 同步帧率
    /// </summary>
    public static float syncInterval = 0.02f;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}