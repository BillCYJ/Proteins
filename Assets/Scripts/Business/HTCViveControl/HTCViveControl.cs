using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HTCViveControl : MonoBehaviour
{
    public Slider slider;

    public void ChangeModelScale()
    {
        if (null == slider)
            return;
        // slider.value的值范围是[0,1]，相要的scale范围是[0.25,4]
        if (slider.value <= 0.5) //缩小
        {
            gameObject.transform.localScale = new Vector3(slider.value * 1.5f + 0.25f, slider.value * 1.5f + 0.25f, slider.value * 1.5f + 0.25f);
        }
        else //放大
        {
            gameObject.transform.localScale = new Vector3(slider.value * 6f - 2f, slider.value * 6f - 2f, slider.value * 6f - 2f);
        }
    }

    public void ResetScale()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        slider.value = 0.5f;
    }
}
