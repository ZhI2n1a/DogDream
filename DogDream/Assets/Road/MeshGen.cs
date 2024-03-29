﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MeshGen : MonoBehaviour
{
    public float SegmentLength = 5;
    public int SegmentResolution = 32;
    public int MeshCount = 8;
    public int VisibleMeshes = 4;

    public MeshFilter SegmentPrefab;
    private Vector3[] _vertexArray;
    private List<MeshFilter> _freeMeshFilters = new List<MeshFilter>();
    private List<Segment> _usedSegments = new List<Segment>();

    public bool GenerateCollider = false;

    void Awake()
    {
        _vertexArray = new Vector3[SegmentResolution * 2];
        
        int iterations = _vertexArray.Length / 2 - 1;
        var triangles = new int[(_vertexArray.Length - 2) * 3];
        
        for (int i = 0; i < iterations; ++i)
        {
            int i2 = i * 6;
            int i3 = i * 2;
            
            triangles[i2] = i3 + 2;
            triangles[i2 + 1] = i3 + 1;
            triangles[i2 + 2] = i3 + 0;
            
            triangles[i2 + 3] = i3 + 2;
            triangles[i2 + 4] = i3 + 3;
            triangles[i2 + 5] = i3 + 1;
        }
        
        var colors = new Color32[_vertexArray.Length];
        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i] = new Color32(255, 255, 255, 255);
        }

        for (int i = 0; i < MeshCount; ++i)
        {
            MeshFilter filter = Instantiate(SegmentPrefab);
        
            Mesh mesh = filter.mesh;
            mesh.Clear();
            
            mesh.vertices = _vertexArray;
            mesh.triangles = triangles;
            
            filter.gameObject.SetActive(false);
            _freeMeshFilters.Add(filter);
        }
    }

    void Update()
    {
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        int currentSegment = (int) (worldCenter.x / SegmentLength);
        
        for (int i = 0; i < _usedSegments.Count;)
        {
            int segmentIndex = _usedSegments[i].Index;
            if (!IsSegmentInSight(segmentIndex))
            {
                EnsureSegmentNotVisible(segmentIndex);
            } else {
                ++i;
            }
        }
        
        for (int i = currentSegment - VisibleMeshes / 2; i < currentSegment + VisibleMeshes / 2; ++i)
        {
            if (IsSegmentInSight(i))
            {
                EnsureSegmentVisible(i);
            }
        }
    }
    
    private void EnsureSegmentVisible(int index)
    {
        if (!IsSegmentVisible(index))
        {
            int meshIndex = _freeMeshFilters.Count - 1;
            MeshFilter filter = _freeMeshFilters[meshIndex];
            _freeMeshFilters.RemoveAt(meshIndex);
            
            Mesh mesh = filter.mesh;
            GenerateSegment(index, ref mesh);
            
            filter.transform.position = new Vector3(index * SegmentLength, 0, 0);
            filter.gameObject.SetActive(true);
            
            var segment = new Segment();
            segment.Index = index;
            segment.MeshFilter = filter;

            if (GenerateCollider)
            {
                Destroy(filter.GetComponent<MeshCollider>());
                Destroy(filter.GetComponent<EdgeCollider2D>());
                EdgeCollider2D collider = filter.gameObject.AddComponent<EdgeCollider2D>();
                DrawColliderPaths(collider, mesh);
            }
            _usedSegments.Add(segment);

        }
    }
    
    private void EnsureSegmentNotVisible(int index)
    {

        if (IsSegmentVisible(index))
        {
            int listIndex = SegmentCurrentlyVisibleListIndex(index);
            Segment segment = _usedSegments[listIndex];
            _usedSegments.RemoveAt(listIndex);
            
            MeshFilter filter = segment.MeshFilter;
            filter.gameObject.SetActive(false);
            
            _freeMeshFilters.Add(filter);
        }
    }
    
    private bool IsSegmentVisible(int index)
    {
        return SegmentCurrentlyVisibleListIndex(index) != -1;
    }
    
    private int SegmentCurrentlyVisibleListIndex(int index)
    {
        for (int i = 0; i < _usedSegments.Count; ++i)
        {
            if (_usedSegments[i].Index == index)
            {
                return i;
            }
        }
        
        return -1;
    }

    private float GetHeight(float position)
    {
        return (0.75f * Mathf.Sin(position) + 5f + Mathf.Sin(position * 1.75f) * 0.25f + 7f) / 2f;
    }

    public void GenerateSegment(int index, ref Mesh mesh)
    {
        float startPosition = index * SegmentLength;
        float step = SegmentLength / (SegmentResolution - 1);
        
        for (int i = 0; i < SegmentResolution; ++i)
        {
            // get the relative x position
            float xPos = step * i;
            
            // top vertex
            float yPosTop = GetHeight(startPosition + xPos); // position passed to GetHeight() must be absolute
            _vertexArray[i * 2] = new Vector3(xPos, yPosTop, 0);
            
            // bottom vertex always at y=0
            _vertexArray[i * 2 + 1] = new Vector3(xPos, 0, 0);

            
        }

        Vector2[] uvs = new Vector2[_vertexArray.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(_vertexArray[i].x, _vertexArray[i].y);
        }
        mesh.uv = uvs;

        mesh.vertices = _vertexArray;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        // need to recalculate bounds, because mesh can disappear too early
    }
    
    private bool IsSegmentInSight(int index)
    {
        Vector3 worldLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 worldRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        
        // check left and right segment side
        float x1 = index * SegmentLength;
        float x2 = x1 + SegmentLength;
        
        return x1 <= worldRight.x && x2 >= worldLeft.x;
    }
    
    private struct Segment
    {
        public int Index { get; set; }
        public MeshFilter MeshFilter { get; set; }
    }

    public static void DrawColliderPaths(EdgeCollider2D collider, Mesh mesh)
    {
        var edges = mesh.vertices;
        int index = 0;
        Vector2[] path2D = new Vector2[edges.Length/2];
        for (int i = 0; i < edges.Length; i++)
        {
            if (i % 2 == 0)
            {
                path2D[index] = (new Vector2(edges[i].x, edges[i].y));
                index++;
            }
                
        }
        collider.points = path2D;
    }
}
