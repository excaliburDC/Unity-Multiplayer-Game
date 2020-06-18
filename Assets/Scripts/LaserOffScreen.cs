using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOffScreen : WrappingEffect
{
    public override void Update()
    {
        base.Update();

        if(isOffscreen)
        {
            gameObject.SetActive(false);
        }
    }
}
