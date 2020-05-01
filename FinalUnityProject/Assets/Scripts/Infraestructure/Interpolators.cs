using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Interpolators
{
    public enum INTERPOLATORTYPE
    {
        LINEAR,
        QUADIN,
        QUADOUT,
        QUADINOUT,
        CUBICIN,
        CUBICOUT,
        CUBICINOUT,
        QUARTIN,
        QUARTOUT,
        QUARTINOUT,
        QUINTIN,
        QUINTOUT,
        QUINTINOUT,
        BACKIN,
        BACKOUT,
        BACKINOUT,
        ELASTICIN,
        ELASTICOUT
    }

    public delegate float InterpolaltorFunc(float start, float end, float ratio);

    // Linear ------------------------------------------------------->
    public static float Linear(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        return end * ratio + start * (1 - ratio);
    }
    // -------------------------------------------------------------->

    // Quadratic ---------------------------------------------------->
    public static float QuadIn(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return c * ratio * ratio + start;
    }

    public static float QuadOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return -c * ratio * (ratio - 2.0f) + start;
    }

    public static float QuadInOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float t = ratio * 2.0f;
        if (t < 1.0f) return ((c * 0.5f) * t * t) + start;
        float tm = t - 1;
        return -(c * 0.5f) * (((tm - 2.0f) * (tm)) - 1.0f) + start;
    }
    // -------------------------------------------------------------->

    // Cubic -------------------------------------------------------->
    public static float CubicIn(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return c * ratio * ratio * ratio + start;
    }

    public static float CubicOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio <= 1.0f) return end;
        float c = end - start;
        float ratio2 = ratio - 1;
        return c * (ratio2 * ratio2 * ratio2 + 1) + start;
    }

    public static float CubicInOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float ratio2 = 2 * ratio;
        if (ratio2 < 1) return c / 2 * ratio2 * ratio2 * ratio2 + start;
        ratio2 -= 2;
        return c / 2 * (ratio2 * ratio2 * ratio2 + 2) + start;
    }
    // -------------------------------------------------------------->

    // Quart -------------------------------------------------------->
    public static float QuartIn(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return c * ratio * ratio * ratio + start;
    }

    public static float QuartOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float t = ratio - 1;
        return -c * (t * t * t * t - 1.0f) + start;
    }

    public static float QuartInOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float t = ratio * 2.0f;
        if (t < 1.0f) return c * 0.5f * t * t * t * t + start;
        t -= 2.0f;
        return -c * 0.5f * ((t) * t * t * t - 2.0f) + start;
    }
    // -------------------------------------------------------------->

    // Quint -------------------------------------------------------->
    public static float QuintIn(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return c * ratio * ratio * ratio * ratio * ratio + start;
    }

    public static float QuintOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float ratio2 = ratio - 1.0f;
        return c * ((ratio2) * ratio2 * ratio2 * ratio2 * ratio2 + 1.0f) + start;
    }

    public static float QuintInOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float t = ratio * 2.0f;
        if (t < 1.0f) return c * 0.5f * t * t * t * t * t + start;
        t -= 2.0f;
        return c * 0.5f * ((t) * t * t * t * t + 2.0f) + start;
    }
    // -------------------------------------------------------------->

    // Back --------------------------------------------------------->
    public static float BackIn(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float s = 1.70158f;
        return c * (ratio) * ratio * ((s + 1) * ratio - s) + start;
    }

    public static float BackOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float s = 1.70158f;
        float ratio2 = ratio - 1;
        return c * (ratio2 * ratio2 * ((s + 1) * ratio2 + s) + 1) + start;
    }

    public static float BackInOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float s = 1.70158f;
        s *= (1.525f);
        float ratio2 = ratio;
        if ((ratio2 *= 2) < 1) return c / 2 * (ratio2 * ratio2 * (((s) + 1) * ratio2 - s)) + start;
        float postFix = ratio2 -= 2;
        return c / 2 * ((postFix) * ratio2 * (((s) + 1) * ratio2 + s) + 2) + start;
    }
    // -------------------------------------------------------------->

    // Elastic ------------------------------------------------------>
    public static float ElasticIn(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float p = 0.3f;
        float a = c;
        float s = p * 0.25f;
        float ratio2 = ratio;
        float postFix = a * Mathf.Pow(2.0f, 10.0f * (ratio2 -= 1)); // this is a fix, again, with post-increment operators
        return -(postFix * Mathf.Sin((float)((ratio2 - s) * (2 * Mathf.PI) / p))) + start;
    }
    public static float ElasticOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float p = 0.3f;
        float a = c;
        float s = p * 0.25f;
        return (a * Mathf.Pow(2.0f, -10 * ratio) * Mathf.Sin((float)((ratio - s) * (2 * Mathf.PI) / p)) + c + start);
    }
    public static float ElasticInOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float ratio2 = 2 * ratio;
        float c = end - start;
        float p = (.3f * 1.5f);
        float a = c;
        float s = p * 0.25f;
        float postFix;
        if (ratio2 < 1)
        {
            postFix = a * Mathf.Pow(2.0f, 10.0f * (ratio2 -= 1)); // postIncrement is evil
            return -.5f * (postFix * Mathf.Sin((float)((ratio2 - s) * (2 * Mathf.PI) / p))) + start;
        }
        postFix = a * Mathf.Pow(2.0f, -10.0f * (ratio2 -= 1)); // postIncrement is evil
        return postFix * Mathf.Sin((float)((ratio2 - s) * (2 * Mathf.PI) / p)) * .5f + c + start;
    }
    // -------------------------------------------------------------->

    
    // Bounce ------------------------------------------------------->    
    public static float BounceOut(float start, float end, float ratio)
    {
        float t;
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        if (ratio < (1.0f / 2.75f)) return c * (7.5625f * ratio * ratio) + start;
        if (ratio < (2 / 2.75f))
        {
            t = ratio - (1.5f / 2.75f);
            return c * (7.5625f * t * t + .75f) + start;
        }
        if (ratio < (2.5 / 2.75))
        {
            t = ratio - (2.25f / 2.75f);
            return c * (7.5625f * t * t + .9375f) + start;
        }

        t = ratio - (2.625f / 2.75f);
        return c * (7.5625f * t * t + .984375f) + start;
    }
    public static float BounceIn(float start, float end, float ratio)
    {
        return BounceOut(end, start, 1.0f - ratio);
    }
    public static float BounceInOut(float start, float end, float ratio)
    {
        float m = (start + end) * 0.5f;
        if (ratio < 0.5f) return BounceIn(start, m, ratio * 2.0f);
        return BounceOut(m, end, ratio * 2.0f - 1.0f);
    }
    // -------------------------------------------------------------->

    // Circular ----------------------------------------------------->
    public static float CircularIn(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return -c * (Mathf.Sqrt(1.0f - ratio * ratio) - 1.0f) + start;
    }
    public static float CircularOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float t = ratio - 1.0f;
        return c * Mathf.Sqrt(1.0f - t * t) + start;
    }
    public static float CircularInOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float t = ratio * 2.0f;
        if (t < 1.0f) return -c * 0.5f * (Mathf.Sqrt(1.0f - t * t) - 1.0f) + start;
        float t2 = t - 2.0f;
        return c * 0.5f * (Mathf.Sqrt(1.0f - t2 * (t2)) + 1.0f) + start;
    }

    // Expo --------------------------------------------------------->
    public static float ExpoIn(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return c * Mathf.Pow(2.0f, 10 * (ratio - 1.0f)) + start;
    }
    public static float ExpoOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return c * (-Mathf.Pow(2.0f, -10 * ratio) + 1.0f) + start;
    }
    public static float ExpoInOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        float t = ratio * 2.0f;
        if (t < 1.0f) return c * 0.5f * Mathf.Pow(2.0f, 10 * (t - 1.0f)) + start;
        return c * 0.5f * (-Mathf.Pow(2.0f, -10.0f * --t) + 2) + start;
    }
    // -------------------------------------------------------------->

    // Sine --------------------------------------------------------->
    public static float SineIn(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return -c * Mathf.Cos((float)(ratio * Mathf.PI * 0.5f)) + c + start;
    }

    public static float SineOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return c * Mathf.Sin((float)(ratio * Mathf.PI * 0.5f)) + start;
    }
    public static float SineInOut(float start, float end, float ratio)
    {
        if (ratio <= 0.0f) return start;
        if (ratio >= 1.0f) return end;
        float c = end - start;
        return -c * 0.5f * (Mathf.Cos((float)(Mathf.PI * ratio)) - 1.0f) + start;
    }
    // -------------------------------------------------------------->
}
