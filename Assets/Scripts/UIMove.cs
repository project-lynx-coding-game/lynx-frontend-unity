using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIMove : MonoBehaviour
{

    public AnimationCurve vx;
    public AnimationCurve vy;
    public float time = 1f;

    async void Start()
    {
        RectTransform element = gameObject.GetComponent<RectTransform>();

        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            element.anchoredPosition = new Vector2(
                element.anchoredPosition.x + vx.Evaluate(elapsedTime / time),
                element.anchoredPosition.y + vy.Evaluate(elapsedTime / time)
            );

            await Task.Yield();
        }
    }
}
