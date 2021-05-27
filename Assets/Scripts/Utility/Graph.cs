using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     A set of vertices of type T and edges (pairs of vertices).
/// </summary>
public class Graph<T>
{
    private readonly IDictionary<Tuple<T, T>, double> _edges;
    private readonly IDictionary<T, ISet<T>> _adjList;

    public Graph(ISet<T> vertices, IDictionary<Tuple<T, T>, double> edges)
    {
        _edges = edges;
        _adjList = new Dictionary<T, ISet<T>>();
        foreach (var vertex in vertices)
        {
            _adjList.Add(vertex, new HashSet<T>());
        }

        foreach (var (from, to) in _edges.Keys)
        {
            _adjList[from].Add(to);
            _adjList[to].Add(from);
        }
    }

    public ISet<T> Adjacent(T vertex)
    {
        return _adjList[vertex];
    }

    public double Weight(T to, T from)
    {
        return _edges[new Tuple<T, T>(to, from)];
    }
}
