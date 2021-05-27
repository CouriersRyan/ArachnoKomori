using System.Collections.Generic;
using UnityEngine;

public class Path<T>
{
    public readonly IEnumerable<T> coords;
    public readonly T start, end;

    public Path(T initial, T dest, IEnumerable<T> path = null)
    {
        coords = path;
        start = initial;
        end = dest;
    }
}