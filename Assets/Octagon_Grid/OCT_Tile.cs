using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OCT_Tile : MonoBehaviour
{

    //public bool u, ur, r, dr, d, dl, l, ul;

    public Flags flags = new Flags();

    public int x, y;
    private float speed = 10;

    private void InitializeTile() {
        for (int i = 0; i < UnityEngine.Random.Range(2, 5); i++) {
            Debug.Log(flags);
            var j = UnityEngine.Random.Range(0, 8);
            var v = (1 << j);
            Debug.Log("j = " + j + " - v = " + v + " - flag = " + (Flags)v);
            flags |= (Flags)(v);
        }
    }

    public MeshRenderer[] sprockets;
    private void LateUpdate() {
        if (OCT_Grid.isTurning)
            return;

        UpdateLooks();

    }
    

    void UpdateLooks() {
        foreach (var item in sprockets) {
            item.enabled = false;
        }

        if (flags.HasFlag(Flags.u))
            sprockets[0].enabled = true;
        if (flags.HasFlag(Flags.ur))
            sprockets[1].enabled = true;
        if (flags.HasFlag(Flags.r))
            sprockets[2].enabled = true;
        if (flags.HasFlag(Flags.dr))
            sprockets[3].enabled = true;
        if (flags.HasFlag(Flags.d))
            sprockets[4].enabled = true;
        if (flags.HasFlag(Flags.dl))
            sprockets[5].enabled = true;
        if (flags.HasFlag(Flags.l))
            sprockets[6].enabled = true;
        if (flags.HasFlag(Flags.ul))
            sprockets[7].enabled = true;
    }

    [ContextMenu("Rotate CCW")]
    public void RotateCCW() {
        OCT_Grid.isTurning = true;
        StartCoroutine(RotateCCWCoroutine());
    }
    IEnumerator RotateCCWCoroutine() {
        float t  = 0;
        transform.rotation = Quaternion.identity;
        var startRot = transform.rotation;
        var endRot = Quaternion.AngleAxis(45, Vector3.forward);
        while (t < 1) {
            yield return null;
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            t += Time.deltaTime * speed;
        }
        transform.rotation = endRot;
        var b = false;
        if (flags.HasFlag(Flags.u))
            b = true;
        flags = (Flags)((int)flags >> 1);

        if (b) {
            flags |= Flags.ul;
        }

        transform.rotation = Quaternion.identity;
        UpdateLooks();
        OCT_Grid.isTurning = false;
    }

    [ContextMenu("Rotate CW")]
    public void RotateCW() {
        StartCoroutine(RotateCWCoroutine());

    }
    IEnumerator RotateCWCoroutine() {
        float t = 0;
        transform.rotation = Quaternion.identity;
        var startRot = transform.rotation;
        var endRot = Quaternion.AngleAxis(-45, Vector3.forward);
        while (t <= 1) {
            yield return null;
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            t += Time.deltaTime * speed;
        }
        transform.rotation = endRot;
        flags = (Flags)((int)flags << 1);
        if ((int)flags > 255) {
            flags = (Flags)((int)flags - 256);
            flags |= (Flags)1;
        }


        transform.rotation = Quaternion.identity;
        UpdateLooks();
        OCT_Grid.isTurning = false;
    }


}

public static class FlagExtensions
{
    public static List<Vector2> Dir(this Flags flags) {
        var dir = new List<Vector2>();

        if (flags.HasFlag(Flags.u))
            dir.Add(Vector2.up);
        if (flags.HasFlag(Flags.r))
            dir.Add(Vector2.right);
        if (flags.HasFlag(Flags.d))
            dir.Add(Vector2.down);
        if (flags.HasFlag(Flags.l))
            dir.Add(Vector2.left);

        if (flags.HasFlag(Flags.ur))
            dir.Add(Vector2.up + Vector2.right);
        if (flags.HasFlag(Flags.dr))
            dir.Add(Vector2.down + Vector2.right);
        if (flags.HasFlag(Flags.dl))
            dir.Add(Vector2.down + Vector2.left);
        if (flags.HasFlag(Flags.ul))
            dir.Add(Vector2.up + Vector2.left);

        return dir;
    }

    //TODO: Could this be done more streamlined?
    public static Flags GetOpposites(this Flags flags) {
        Flags opposites = new Flags();
        switch (flags) {
            case Flags.u:
                opposites |= Flags.d;
                break;
            case Flags.ur:
                opposites |= Flags.dl;
                break;
            case Flags.r:
                opposites |= Flags.l;
                break;
            case Flags.dr:
                opposites |= Flags.ul;
                break;
            case Flags.d:
                opposites |= Flags.u;
                break;
            case Flags.dl:
                opposites |= Flags.ur;
                break;
            case Flags.l:
                opposites |= Flags.r;
                break;
            case Flags.ul:
                opposites |= Flags.dr;
                break;
        }
        return opposites;
    }

    public static Flags RotateCCW(this Flags flags) {
        var b = false;
        if (flags.HasFlag(Flags.u))
            b = true;
        flags = (Flags)((int)flags >> 1);

        if (b) {
            flags |= Flags.ul;
        }
        return flags;
    }
    
    public static Flags RotateCW(this Flags flags) {
        flags = (Flags)((int)flags << 1);
        if ((int)flags > 255) {
            flags = (Flags)((int)flags - 256);
            flags |= (Flags)1;
        }
        return flags;
    }

    // TODO: Highly unsafe code, should be proximated!
    public static Flags FromDirection(this Vector3 dir) {
        if (dir.x == 0 && dir.y == 1)
            return Flags.u;
        if (dir.x == 1 && dir.y == 1)
            return Flags.ur;
        if (dir.x == 1 && dir.y == 0)
            return Flags.r;
        if (dir.x == 1 && dir.y == -1)
            return Flags.dr;

        if (dir.x == 0 && dir.y == -1)
            return Flags.d;
        if (dir.x == -1 && dir.y == -1)
            return Flags.dl;
        if (dir.x == -1 && dir.y == 0)
            return Flags.l;
        if (dir.x == -1 && dir.y == 1)
            return Flags.ul;

        return 0;
    }
}

    [System.Flags]
    public enum Flags
    {
        u   =   1 << 0,
        ur  =   1 << 1,
        r   =   1 << 2,
        dr  =   1 << 3,
        d   =   1 << 4,
        dl  =   1 << 5,
        l   =   1 << 6,
        ul  =   1 << 7
    }
