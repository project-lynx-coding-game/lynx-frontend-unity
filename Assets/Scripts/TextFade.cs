using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    // The curve should be scaled from <0, 1>
    public AnimationCurve curve;
    public float time = 1f;

    async void Start()
    {
        TMPro.TextMeshProUGUI text = gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        float elapsedTime = 0;
        while(elapsedTime < time)
        {
            elapsedTime+= Time.deltaTime;
            text.alpha= curve.Evaluate(elapsedTime/time);
            await Task.Yield();
        }
    }

}
