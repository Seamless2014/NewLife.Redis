﻿namespace NewLife.Caching.Models;

/// <summary>主从复制信息</summary>
public class ReplicationInfo
{
    #region 属性
    /// <summary>主从角色。master/slave</summary>
    public String Role { get; set; }

    /// <summary>从节点集合</summary>
    public SlaveInfo[] Slaves { get; set; }

    /// <summary>主节点地址。仅slave有</summary>
    public String MasterHost { get; set; }

    /// <summary>主节点端口。仅slave有</summary>
    public Int32 MasterPort { get; set; }
    #endregion

    #region 方法
    /// <summary>加载数据</summary>
    /// <param name="data"></param>
    public void Load(IDictionary<String, String> data)
    {
        if (data.TryGetValue("role", out var str)) Role = str;
        if (data.TryGetValue("master_host", out str)) MasterHost = str;
        if (data.TryGetValue("master_port", out str)) MasterPort = str.ToInt();

        if (data.TryGetValue("connected_slaves", out str))
        {
            var num = str.ToInt();
            if (num > 0)
            {
                var list = new List<SlaveInfo>();
                for (var i = 0; i < num; i++)
                {
                    if (data.TryGetValue($"slave{i}", out str))
                    {
                        var inf = SlaveInfo.Parse(str);
                        if (inf != null) list.Add(inf);
                    }
                }

                Slaves = list.ToArray();
            }
        }
    }
    #endregion
}