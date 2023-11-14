using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class PriorityQueue<TPriority, TData> where TPriority : IComparable<TPriority>
{
    List<Tuple<TPriority, TData>> queue = new List<Tuple<TPriority, TData>>();
    public void Enqueue(TPriority priority, TData data)
    {
        for(int i = 0; i < queue.Count; i++)
        {
            if (priority.CompareTo(queue[i].Item1) > 0)
            {
                queue.Insert(i, new Tuple<TPriority, TData>(priority, data));
                return;
            }
        }

        queue.Add(new Tuple<TPriority, TData>(priority, data));
    }

    public TData Dequeue(out TPriority priority)
    {
        var TData = queue[queue.Count - 1];
        queue.RemoveAt(queue.Count - 1);
        priority = TData.Item1;
        return TData.Item2;
    }

    public int Count => queue.Count;

    public void Clear()
    {
        queue.Clear();
    }
}
