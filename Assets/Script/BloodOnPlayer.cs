using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BloodOnPlayer : MonoBehaviour
{
    VisualEffect vfx;
    public GameObject player;
    public float scale;
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        vfx.SetVector3("PlayerPosition", player.transform.position);
        vfx.SetVector3("PlayerScale", player.transform.localScale*scale);
    }
}
