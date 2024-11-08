﻿using System;
using System.Collections.Generic;
using UnityEngine;


public class TimerUtil
{
    private static TimerUtil ins;
    /// <summary>
    /// 定时器字典
    /// </summary>
    private Dictionary<int, TickUtil> tickDict = new Dictionary<int, TickUtil>();
    public static object locker = new object();//添加一个对象作为锁
    private float curtime;
    /// <summary>
    /// 延迟定时器字典
    /// </summary>
    private Dictionary<int, TickUtil> deltaTickDic = new Dictionary<int, TickUtil>();
    public static object deltaTickDicLocker = new object();//添加一个对象作为锁
    /// <summary>
    /// 当前延迟的时间
    /// </summary>
    private float deltaTime;
    /// <summary>
    /// 未缩放时间
    /// </summary>
    private float unscaledTime;
    private int timerid;

    List<int> dellistInUpdate = new List<int>();
    List<int> templistInUpdate = new List<int>();

    List<int> delListInFixedUpdate = new List<int>();
    List<int> tempListInFixedUpdate = new List<int>();

    public static TimerUtil Instance
    {
        get
        {
            if (ins == null)
            {
                ins = new TimerUtil();
            }
            return ins;
        }
    }

    public bool Init()
    {
        timerid = 0;
        curtime = Time.unscaledTime;
        unscaledTime = Time.unscaledTime;
        deltaTime = Time.time;
        return true;
    }

    /// <summary>
    /// 添加定时器,该定时器不受时间缩放影响
    /// </summary>
    /// <param name="interval">定时器时间间隔</param>
    /// <param name="count">定时器执行次数,0不限次数</param>
    /// <param name="start">定时器开始等待时间</param>
    /// <param name="func">定时器回调方法</param>
    /// <returns></returns>
    public int AddTimer(float interval, int count, float start, TimerCallBack func)
    {
        if (interval < 0 || count < 0 || start < 0)
        {
            Debug.LogError("error add timer args:" + interval + count + start);
            return 0;
        }
        TickUtil tick = new TickUtil();
        if (tick == null)
        {
            return 0;
        }
        timerid++;
        tick.tid = timerid;
        tick.interval = interval;
        tick.start = unscaledTime + start;
        tick.count = count;
        tick.cbfunc = func;
        lock (locker)//锁
        {
            tickDict.Add(tick.tid, tick);
        }
        return tick.tid;
    }
    /// <summary>
    /// 添加定时器,该定时器受时间缩放影响
    /// </summary>
    /// <param name="interval">定时器时间间隔</param>
    /// <param name="count">定时器执行次数,0不限次数</param>
    /// <param name="start">定时器开始等待时间</param>
    /// <param name="func">定时器回调方法</param>
    /// <returns></returns>
    public int AddDeltaTimer(float interval, int count, float start, TimerCallBack func)
    {
        if (interval < 0 || count < 0 || start < 0)
        {
            Debug.LogError("error add timer args:" + interval + count + start);
            return 0;
        }
        TickUtil tick = new TickUtil();
        if (tick == null)
        {
            return 0;
        }
        timerid++;
        tick.tid = timerid;
        tick.interval = interval;
        tick.start = deltaTime + start;
        tick.count = count;
        tick.cbfunc = func;
        lock (deltaTickDicLocker)
        {
            deltaTickDic.Add(tick.tid, tick);
        }
        return tick.tid;
    }

    /// <summary>
    /// 设置定时器为锁死状态
    /// </summary>
    /// <param name="tid"></param>
    public void SetFixTimer(int tid)
    {
        TickUtil tick = null;
        if (tickDict.ContainsKey(tid))
        {
            tick = tickDict[tid];
        }
        else if (deltaTickDic.ContainsKey(tid))
        {
            tick = deltaTickDic[tid];
        }
        if (tick == null)
        {
            // Debug.Log("pause timer no id:" + tid);
            return;
        }
        tick.fix = true;
    }

    /// <summary>
    /// 取消所有定时器,标记为锁死的除外
    /// </summary>
    public void CancelAllTimer()
    {
        List<int> _list = new List<int>();
        foreach (var item in tickDict)
        {
            TickUtil tick = item.Value;
            if (tick.fix == false)
            {
                _list.Add(tick.tid);
            }
        }

        foreach (var item in deltaTickDic)
        {
            TickUtil tick = item.Value;
            if (tick.fix == false)
            {
                _list.Add(tick.tid);
            }
        }

        foreach (var item in _list)
        {
            int tid = item;
            CancelTimer(tid);
        }
    }

    /// <summary>
    /// 取消单个定时器
    /// </summary>
    /// <param name="tid"></param>
    public void CancelTimer(int tid)
    {
        if (tickDict.ContainsKey(tid))
        {
            lock (locker)//锁
            {
                tickDict.Remove(tid);
            }
        }
        else if (deltaTickDic.ContainsKey(tid))
        {
            lock (deltaTickDicLocker)//锁
            {
                deltaTickDic.Remove(tid);
            }
        }
        else
        {
            //Output.Error("do delete timer no id:", tid);
        }
    }

    /// <summary>
    /// 暂停单个定时器
    /// </summary>
    /// <param name="tid"></param>
    public void PauseTimer(int tid)
    {
        TickUtil tick = null;
        if (tickDict.ContainsKey(tid))
        {
            tick = tickDict[tid];
        }
        else if (deltaTickDic.ContainsKey(tid))
        {
            tick = deltaTickDic[tid];
        }
        if (tick == null)
        {
            // Debug.Log("pause timer no id:" + tid);
            return;
        }
        tick.pause = true;
    }

    /// <summary>
    /// 暂停所有定时器
    /// </summary>
    public void PauseAllTimer()
    {
        List<int> _list = new List<int>();
        foreach (var item in tickDict)
        {
            TickUtil tick = item.Value;
            _list.Add(tick.tid);
        }
        foreach (var item in deltaTickDic)
        {
            TickUtil tick = item.Value;
            _list.Add(tick.tid);
        }

        foreach (var item in _list)
        {
            int tid = item;
            PauseTimer(tid);
        }
    }

    /// <summary>
    /// 恢复单个定时器
    /// </summary>
    /// <param name="tid"></param>
    public void RecoverTimer(int tid)
    {
        TickUtil tick = null;
        if (tickDict.ContainsKey(tid))
        {
            tick = tickDict[tid];
        }
        else if (deltaTickDic.ContainsKey(tid))
        {
            tick = deltaTickDic[tid];
        }
        if (tick == null)
        {
            // Debug.Log("recover timer no id:" + tid);
            return;
        }
        tick.pause = false;
    }

    /// <summary>
    /// 恢复所有定时器
    /// </summary>
    public void RecoverAllTimer()
    {
        List<int> _list = new List<int>();
        foreach (var item in tickDict)
        {
            TickUtil tick = item.Value;
            _list.Add(tick.tid);
        }
        foreach (var item in deltaTickDic)
        {
            TickUtil tick = item.Value;
            _list.Add(tick.tid);
        }

        foreach (var item in _list)
        {
            int tid = item;
            RecoverTimer(tid);
        }
    }

    public void DoUpdate()
    {
        deltaTime = Time.time;
        if (deltaTickDic.Count <= 0) return;

        dellistInUpdate.Clear();
        templistInUpdate.Clear();

        templistInUpdate.AddRange(deltaTickDic.Keys);

        foreach (var item in templistInUpdate)
        {
            int tid = item;
            TickUtil tick;
            if (!deltaTickDic.TryGetValue(tid, out tick))
            {
                continue;
            };
            if (tick == null)
            {
                continue;
            }
            if (tick.pause == true) continue;
            if (tick.start <= deltaTime)
            {
                tick.cbfunc();
                tick.count--;
                tick.start += tick.interval;
                if (tick.count == 0)
                {
                    dellistInUpdate.Add(tick.tid);
                }
            }
        }

        foreach (int tid in dellistInUpdate)
        {
            if (deltaTickDic.ContainsKey(tid) == false)
            {
                //Output.Error("auto delete timer no id:", tid);
                continue;
            }
            lock (deltaTickDicLocker)
            {
                deltaTickDic.Remove(tid);
            }
        }
    }

    public void DoFixUpdate()
    {
        curtime = Time.unscaledTime;
        unscaledTime = curtime;
        if (tickDict.Count <= 0) return;

        //dellistInUpdate.Clear();
        //templistInUpdate.Clear();
        delListInFixedUpdate.Clear();
        tempListInFixedUpdate.Clear();

        //templistInUpdate.AddRange(tickDict.Keys);
        tempListInFixedUpdate.AddRange(tickDict.Keys);

        foreach (var item in tempListInFixedUpdate)
        {
            int tid = item;
            TickUtil tick;
            if (!tickDict.TryGetValue(tid, out tick))
            {
                //Debug.LogError("find timer no id:" + tid);
                continue;
            };
            if (tick == null)
            {
                continue;
            }
            if (tick.pause == true) continue;
            if (tick.start <= curtime)
            {
                tick.cbfunc();
                tick.count--;
                tick.start += tick.interval;
                if (tick.count == 0)
                {
                    delListInFixedUpdate.Add(tick.tid);
                }
            }
        }

        foreach (int tid in delListInFixedUpdate)
        {
            if (tickDict.ContainsKey(tid) == false)
            {
                //Output.Error("auto delete timer no id:", tid);
                continue;
            }
            lock (locker)//锁
            {
                tickDict.Remove(tid);
            }
        }
    }
}

public delegate void TimerCallBack();

public class TickUtil
{
    public int tid;
    public float start;
    public int count;
    public float interval;
    public TimerCallBack cbfunc;
    public bool pause;
    public bool fix;
}

public class TimeUtilHandle
{
    private static TimeUtilHandle instance;
    private TimeUtilHandle()
    {
    }
    public static TimeUtilHandle Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TimeUtilHandle();
            }
            return instance;
        }
    }
    /// <summary>
    /// 获取当前系统时间戳
    /// </summary>
    /// <returns></returns>
    public long GetTimestamp()
    {
        long timestamp = (long)DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
        return timestamp;
    }

    /// <summary>
    /// 将时间转为时间戳  格式为yyyyMMddhhmmss
    /// </summary>
    /// <param name="times"></param>
    /// <returns></returns>
    public long GetTimeStampLong(string times)
    {
        DateTime dt = DateTime.ParseExact(times, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
        long t = (dt.Ticks - startTime.Ticks) / 10000;
        // Debug.Log(GetDateTimeByTimestamp(t).ToString("yyyyMMddHHmmss"));
        return t;
    }

    /// <summary>
    /// 根据时间戳获取一个DateTime
    /// </summary>
    /// /// <param name="timestamp">时间戳</param>
    public DateTime GetDateTimeByTimestamp(long timeStamp)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
        DateTime dataTime = startTime.AddMilliseconds(timeStamp);
        return dataTime;
    }

    /// <summary>
    /// 转换秒数为 mm:ss
    /// </summary>
    /// <param name="second"></param>
    /// <returns></returns>
    public string ParseSecond(int second)
    {
        int min = second / 60;
        int sec = second - min * 60;
        string result = "";
        if (min < 10)
        {
            result += "0";
        }
        result += min + ":";
        if (sec < 10)
        {
            result += "0";
        }
        result += sec;
        return result;
    }
}