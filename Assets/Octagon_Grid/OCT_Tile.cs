using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OCT_Tile : MonoBehaviour
{

    //public bool u, ur, r, dr, d, dl, l, ul;

    public Flags flags = new Flags();

    public int x, y;

    void Start()
    {

        /*for (int i = 0; i < 8; i++) {
            if(UnityEngine.Random.value >= .8f) {
                print((Flags)(1 << i));
                flags |= (Flags)(1 << i);
            }
        }*/

        for (int i = 0; i < UnityEngine.Random.Range(2,5); i++) {
            Debug.Log(flags);   
            var j = UnityEngine.Random.Range(0, 8);
            var v = (1 << j);
            Debug.Log("j = " + j + " - v = " + v + " - flag = " + (Flags)v);
            flags |= (Flags)(v);
        }

    }

    private void Update() {
        
    }

    [ContextMenu("Rotate CCW")]
    public void RotateCCW() {
        var b = false;
        if (flags.HasFlag(Flags.u))
            b = true;
        flags = (Flags)((int)flags >> 1);

        if (b) {  
            flags |= Flags.ul;
        }
    }
    [ContextMenu("Rotate CW")]
    public void RotateCW() {
        flags = (Flags)((int)flags << 1);
        if((int)flags > 255) {
            flags = (Flags)((int)flags - 256);
            flags |= (Flags)1;
        }

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
