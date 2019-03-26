using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class MeshExtension {
    public static Mesh Copy(this Mesh mesh, string newName = null) {
        var copy = Object.Instantiate(mesh);
        if (!string.IsNullOrWhiteSpace(newName))
            copy.name = newName;
        
        return copy;
    }

    public static void OverwriteWith(this Mesh mesh, Mesh otherMesh) {
        mesh.vertices = otherMesh.vertices;
        mesh.subMeshCount = otherMesh.subMeshCount;
        for (int i = 0; i < mesh.subMeshCount; i++) {
            mesh.SetTriangles(otherMesh.GetTriangles(i), i);
        }
        
        mesh.bindposes = otherMesh.bindposes;
        mesh.boneWeights = otherMesh.boneWeights;
        mesh.colors = otherMesh.colors;
        mesh.normals = otherMesh.normals;
        mesh.tangents = otherMesh.tangents;
        mesh.uv = otherMesh.uv;
        mesh.uv2 = otherMesh.uv2;
        mesh.uv3 = otherMesh.uv3;
        mesh.uv4 = otherMesh.uv;
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying) {
            if (!string.IsNullOrEmpty(UnityEditor.AssetDatabase.GetAssetPath(mesh))) {
                UnityEditor.EditorUtility.SetDirty(mesh);
            }
        }
#endif
    }

    public static void RotateVertices(this Mesh mesh, Quaternion rotation) {
        var vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
            vertices[i] = rotation * vertices[i];
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}

public static class Physics2DExtension {
    public static bool Raycast(this Collider2D collider, Vector2 origin, Vector2 direction, out RaycastHit2D hitInfo,
        float maxDistance) {
        var oriLayer = collider.gameObject.layer;
        const int tempLayer = 31;
        const int tempMask = 1 << tempLayer;
        collider.gameObject.layer = tempLayer;
        hitInfo = Physics2D.Raycast(origin, direction, maxDistance, tempMask);
        collider.gameObject.layer = oriLayer;
        if (hitInfo.collider && hitInfo.collider != collider) {
            Debug.LogError(
                $"Collider2D.Raycast() need a unique temp layer to work! Make sure Layer #{tempLayer} is unused!");
            return false;
        }

        return hitInfo.collider != null;
    }
}

public static class Physics2DHelper {
    private static readonly Collider2D[] buffer = new Collider2D[100];

    public static void GetAllNear<T>(Vector2 fromPoint, float radius, int layermask, List<T> results) {
        results.Clear();
        var numFound = Physics2D.OverlapCircleNonAlloc(fromPoint, radius, buffer, layermask);
        for (int i = 0; i < numFound; i++) {
            var foundObj = buffer[i].GetComponentInParent<T>();
            if (foundObj == null)
                continue;
            results.Add(foundObj);
        }
    }

    public static T GetClosest<T>(Vector2 fromPoint, float radius, int layerMask, List<T> resultBuffer) {
        GetAllNear(fromPoint, radius, layerMask, resultBuffer);
        if (resultBuffer.Count == 0)
            return default(T);
        resultBuffer.Sort((x, y) => ByDistanceTo(x, y, fromPoint));
        return resultBuffer[0];
    }

    public static T GetClosest<T>(Vector2 fromPoint, float radius, int layerMask, List<T> resultBuffer, Predicate<T> predicate) {
        GetAllNear(fromPoint, radius, layerMask, resultBuffer);
        for (int i = resultBuffer.Count - 1; i >= 0; i--) {
            if(!predicate(resultBuffer[i]))
                resultBuffer.RemoveAt(i);
        }
        if (resultBuffer.Count == 0)
            return default(T);
        
        resultBuffer.Sort((x, y) => ByDistanceTo(x, y, fromPoint));
        return resultBuffer[0];
    }

    private static int ByDistanceTo<T>(T x, T y, Vector2 fromPoint) {
        var xPos = (Vector2) (x as Component).transform.position;
        var yPos = (Vector2) (y as Component).transform.position;
        var xDist = Vector2.SqrMagnitude(xPos - fromPoint);
        var yDist = Vector2.SqrMagnitude(yPos - fromPoint);
        return xDist.CompareTo(yDist);
    }
}