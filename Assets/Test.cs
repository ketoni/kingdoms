using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Text foo = GetComponent<Text>();
        //foo.text = "FOOBAR";

        using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var activity = actClass.GetStatic<AndroidJavaObject>("currentActivity");
            if (activity == null)
            {
                foo.text = "NULL";
                return;
            }
            List<String> bar = activity.Call<List<String>>("getWifiScanResults");
            if (bar == null)
            {
                foo.text = "FUCK";
                return;
            }
            foreach (String name in bar)
            {
                foo.text += name + " - ";
            }

        }
    }
}
