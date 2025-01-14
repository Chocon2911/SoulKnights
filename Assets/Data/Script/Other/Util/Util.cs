using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    //==========================================Variable==========================================
    private static Util instance;

    //==========================================Get Set===========================================
    public static Util Instance
    {
        get
        {
            if (instance == null) instance = new Util();
            return instance;
        }
    }

    //========================================Constructor=========================================
    public Util()
    {
        if (instance != null)
        {
            Debug.LogError("One Util Only");
            return;
        }

        instance = this;
    }

    //===========================================Random===========================================
    public float RandomFloat(float min, float max)
    {
        if (min >= max)
        {
            float temp = min;
            min = max;
            max = temp;
        }

        return Random.Range(min, max);
    }

    public Vector2 RandomVector2(Vector2 min, Vector2 max)
    {
        return new Vector2(this.RandomFloat(min.x, max.x), this.RandomFloat(min.y, max.y));
    }

    public Vector2 RandomPos(Vector2 center, float rad)
    {
        // Circle Function: (x - center.x)^2 + (y - center.y)^2 = rad^2
        float x = this.RandomFloat(center.x - rad, center.x + rad);
        
        // y = sqrt(rad^2 - (x - center.x)^2) + center.y
        float yLimit = Mathf.Sqrt(rad * rad - (x - center.x) * (x - center.x)) + center.y;
        float y = this.RandomFloat(center.y - yLimit, center.y + yLimit);
        return new Vector2(x, y);
    }
}
