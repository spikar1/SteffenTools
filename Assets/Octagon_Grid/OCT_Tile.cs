using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OCT_Tile : MonoBehaviour
{

    //public bool u, ur, r, dr, d, dl, l, ul;

    public Flags flags;


    void Start()
    {

        for (int i = 0; i < 8; i++) {
            if(Random.value >= .8f) {
                print((Flags)(1 << i));
                flags |= (Flags)(1 << i);
            }
        }
        print((int)flags);
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
    public static List<Vector2> Dir(this Flags flag) {
        var dir = new List<Vector2>();

        if (flag.HasFlag(Flags.u))
            dir.Add(Vector2.up);
        if (flag.HasFlag(Flags.r))
            dir.Add(Vector2.right);
        if (flag.HasFlag(Flags.d))
            dir.Add(Vector2.down);
        if (flag.HasFlag(Flags.l))
            dir.Add(Vector2.left);

        if (flag.HasFlag(Flags.ur))
            dir.Add(Vector2.up + Vector2.right);
        if (flag.HasFlag(Flags.dr))
            dir.Add(Vector2.down + Vector2.right);
        if (flag.HasFlag(Flags.dl))
            dir.Add(Vector2.down + Vector2.left);
        if (flag.HasFlag(Flags.ul))
            dir.Add(Vector2.up + Vector2.left);

        return dir;
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