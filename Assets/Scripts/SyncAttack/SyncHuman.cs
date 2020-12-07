using UnityEngine;

public class SyncHuman : BaseSimpleSampleCharacterControl
{
    private Vector3 lastPos;
    private Vector3 lastRot;

    public Vector3 forecastPos;
    public Vector3 forecastRot;

    private float forecastTime;

    // Use this for initialization
    private void Start()
    {
        //初始化预测信息
        lastPos = transform.position;
        lastRot = transform.eulerAngles;
        forecastPos = transform.position;
        forecastRot = transform.eulerAngles;
        forecastTime = Time.time;
    }

    private new void Update()
    {
        base.Update();
        ForecastUpdate();
    }

    private void ForecastUpdate()
    {
        //时间
        float t = (Time.time - forecastTime) / CtrlHuman.syncInterval;
        t = Mathf.Clamp(t, 0f, 1f);
        //位置
        Vector3 pos = transform.position;
        pos = Vector3.Lerp(pos, forecastPos, t);
        transform.position = pos;
        //旋转
        Quaternion quat = transform.rotation;
        Quaternion forcastQuat = Quaternion.Euler(forecastRot);
        quat = Quaternion.Lerp(quat, forcastQuat, t);
        transform.rotation = quat;
    }

    /// <summary>
    /// 移动同步
    /// </summary>
    /// <param name="msg"></param>
    public void SyncPos(MsgMove msg)
    {
        Vector3 pos = new Vector3(msg.x, msg.y, msg.z);
        Vector3 rot = new Vector3(msg.ex, msg.ey, msg.ez);

        forecastPos = pos + 2 * (pos - lastPos);
        forecastRot = rot + 2 * (rot - lastRot);
        //更新
        lastPos = pos;
        lastPos = rot;
        forecastTime = Time.time;
    }
}