using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweens 
{
    public static float EaseInOutElastic(float start, float end, float value)
    {
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d * 0.5f) == 2) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        if (value < 1) return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
        return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
    }
    public static float EaseOutElastic(float start, float end, float value)
    {
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d) == 1) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p * 0.25f;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
    }
    public float EaseInCirc(float t, float b, float c, float d)
    {
        t /= d;
        return -c * (Mathf.Sqrt(1 - t * t) - 1) + b;
    }
    public static float easeInOutSin(float t, float b, float c, float d)
    {
        return -c / 2 * (Mathf.Cos(Mathf.PI * t / d) - 1) + b;
    }

    public static float easeOutQuint(float t,float b,float c,float d) 
    {
            t /= d;
            t--;
            return c * (t * t * t * t * t + 1) + b;
    }
       
        
        
        

}
