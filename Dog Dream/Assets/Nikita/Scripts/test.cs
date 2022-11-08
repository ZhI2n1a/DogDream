/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    struct Edge
    {

        public Vector2 a;
        public Vector2 b;

        public override bool Equals(object obj)
        {
            if (obj is Edge)
            {
                var edge = (Edge)obj;
                //An edge is equal regardless of which order it's points are in
                return (edge.a == a && edge.b == b) || (edge.b == a && edge.a == b);
            }

            return false;

        }

        public override int GetHashCode()
        {
            return a.GetHashCode() ^ b.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[" + a.x + "," + a.y + "->" + b.x + "," + b.y + "]");
        }

    }

    static bool CoordinatesFormLine(Vector2 a, Vector2 b, Vector2 c)
    {

        //If the area of a triangle created from three points is zero, they must be in a line.
        float area = a.x * (b.y - c.y) +
                     b.x * (c.y - a.y) +
                     c.x * (a.y - b.y);

        return Mathf.Approximately(area, 0f);

    }

    static Vector2[] CoordinatesCleaned(List<Vector2> coordinates)
    {

        List<Vector2> coordinatesCleaned = new List<Vector2>();
        coordinatesCleaned.Add(coordinates[0]);

        var lastAddedIndex = 0;

        for (int i = 1; i < coordinates.Count; i++)
        {

            var coordinate = coordinates[i];

            Vector2 lastAddedCoordinate = coordinates[lastAddedIndex];
            Vector2 nextCoordinate = (i + 1 >= coordinates.Count) ? coordinates[0] : coordinates[i + 1];

            if (!CoordinatesFormLine(lastAddedCoordinate, coordinate, nextCoordinate))
            {

                coordinatesCleaned.Add(coordinate);
                lastAddedIndex = i;

            }

        }

        return coordinatesCleaned.ToArray();

    }

    static List<Vector2[]> BuildColliderPaths(Dictionary<Edge, int> edges)
    {

        var outerEdges = new List<Edge>();

        foreach (var keyVal in edges)
            if (keyVal.Value == 1)
                outerEdges.Add(keyVal.Key);

        var orderedPaths = new List<List<Edge>>();
        List<Edge> orderedEdges = null;

        while (outerEdges.Count > 0)
        {

            int addedThisRound = 0;

            if (orderedEdges == null)
            {
                orderedEdges = new List<Edge>();
                orderedEdges.Add(outerEdges[0]);
                outerEdges.RemoveAt(0);
                orderedPaths.Add(orderedEdges);
            }

            var removeIndexes = new List<int>();
            for (int i = 0; i < outerEdges.Count; i++)
            {
                var edge = outerEdges[i];
                if (edge.b == orderedEdges[0].a)
                {
                    orderedEdges.Insert(0, edge);
                    removeIndexes.Add(i);
                }

                else if (edge.a == orderedEdges[orderedEdges.Count - 1].b)
                {
                    orderedEdges.Add(edge);
                    removeIndexes.Add(i);
                }
            }

            for (var i = removeIndexes.Count - 1; i >= 0; i--)
                outerEdges.RemoveAt(i);

            //If there wasn't any added this round, then we must need to start a new path, because the remaining edges arn't connected.
            if (addedThisRound == 0)
                orderedEdges = null;

        }

        var cleanedPaths = new List<Vector2[]>();

        foreach (var builtPath in orderedPaths)
        {
            var coords = new List<Vector2>();

            foreach (var edge in builtPath)
                coords.Add(edge.a);

            cleanedPaths.Add(CoordinatesCleaned(coords));
        }


        return cleanedPaths;
    }

    public static void DrawColliderPaths(PolygonCollider2D collider, Mesh mesh)
    {
*//*        var edges = mesh.vertices;
        var paths = BuildColliderPaths(edges);

        collider.pathCount = paths.Count;
        for (int i = 0; i < paths.Count; i++)
        {
            var path = paths[i];
            collider.SetPath(i, path);*//*
        }
    }
}
*/