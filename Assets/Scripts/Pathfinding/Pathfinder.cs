using System;
using System.Collections.Generic;
using gm = GameManager;

/// <summary>
///     Finds the shortest path between two cells on a given Graph.
/// </summary>
public static class Pathfinder<T>
{
    /// <summary>
    ///     Maps each node on the graph (i.e. a square on the grid) to the total cost of moving
    ///     there from the given starting point. Note: uses double instead of int because we need
    ///     to take advantage of floating point infinity values.
    /// </summary>
    private static IDictionary<T, double> _distTo;

    /// <summary>
    ///     Maps each node N on the graph (i.e. a square on the grid) to the node that directly
    ///     precedes N when traveling on the shortest path to N from the given starting point.
    /// </summary>
    private static IDictionary<T, T> _edgeTo;

    /// <summary>
    ///     lol don't worry about this its used for the algorithm
    /// </summary>
    private static MinPriorityQueue<T> _pq;

    /// <summary>
    ///     The starting point of the shortest paths tree. We are trying to find the shortest path
    ///     starting from this coordinate.
    /// </summary>
    private static T _start;

    /// <summary>
    ///     The destination (i.e. ending point) of the path. We are trying to find the shortest path
    ///     from _start ending at this point.
    /// </summary>
    private static T _end;

    /// <summary>
    ///     lol don't worry about this its used for the algorithm
    /// </summary>
    private static Func<T, T, float> _heuristic;

    /// <summary>
    ///     The graph we are trying to find the shortest path over.
    /// </summary>
    private static Graph<T> _graph;


    /// <summary>
    ///     Fills the _distTo and _edgeTo Dictionaries with the shortest paths tree from the given
    ///     starting point.
    /// </summary>
    public static Path<T> ShortestPath(T start, T end, Graph<T> graph)
    {
        _start = start;
        _end = end;
        _graph = graph;
        _pq = new MinPriorityQueue<T>();
        _distTo = new Dictionary<T, double>();
        _edgeTo = new Dictionary<T, T>();

        _pq.Insert(_start, 0);
        _distTo[_start] = 0;
        _edgeTo[_start] = _start;

        while (_pq.Size() > 0)
        {
            var p = _pq.RemoveMin();
            foreach (var adj in _graph.Adjacent(p))
                Relax(p, adj);
        }

        if (!_distTo.ContainsKey(end)) return null;
        var currPath = new Stack<T>();
        var curr = end;
        currPath.Push(end);
        while (!curr.Equals(start))
        {
            curr = _edgeTo[curr];
            currPath.Push(curr);
        }

        return new Path<T>(start, end, currPath);
    }

    /// <summary>
    ///     Ahhhhh, relaxation.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    private static void Relax(T from, T to)
    {
        var distance = DistTo(from) + _graph.Weight(from, to);
        if (!(distance < DistTo(to))) return;
        _edgeTo[to] = from;
        _distTo[to] = distance;
        var pri = DistTo(to) + _heuristic(to, _end);
        _pq.Insert(to, pri);
    }

    private static double DistTo(T c)
    {
        return _distTo.ContainsKey(c) ? _distTo[c] : double.PositiveInfinity;
    }
}