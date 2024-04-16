using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constain
{
    public static string ANIM_IDLE = "idle";
    public static string ANIM_RUN = "run";
    public static string ANIM_OPENDOOR = "opendoor";
    public static Vector3 POSBRICK_FIRSTGROUND = new Vector3(-15f, 0f, 10f);
    public static Vector3 POSBRICK_MIDGROUND = new Vector3(-15f, 21.15f, 60f);
    public static Vector3 POSBRICK_ENDGROUND = new Vector3(-15f, 42.1f, 110f);
    public enum ColorPlay
    {
        blue = 0,
        green = 1,
        red = 2,
        yellow = 3,
        purple = 4
    }
}
