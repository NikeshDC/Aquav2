using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FillBar : MonoBehaviour
{
    [SerializeField] protected Slider slider;

    public bool transitionAnimate;
    public float transitionSpeed = 1;
    private Coroutine transitionCoroutine;

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }

    public void SetCurrentValue(float value)
    {
        if(transitionAnimate)
        {
            if (transitionCoroutine != null)
            { StopCoroutine(transitionCoroutine); }
            transitionCoroutine = StartCoroutine(C_TransitionToTargetValue(value));
        }
        else
        { slider.value = value; }
    }

    private IEnumerator C_TransitionToTargetValue(float targetValue)
    {
        float targetDir = Mathf.Sign(targetValue - slider.value);   
        while(slider.value != targetValue)
        {
            float transitionAmount = transitionSpeed * Time.deltaTime * slider.maxValue;
            if(targetDir > 1)
            { slider.value = Mathf.Min(slider.value + transitionAmount, targetValue); }
            else
            { slider.value = Mathf.Max(slider.value - transitionAmount, targetValue); }
            yield return null;
        }
    }

    public void SetNew(float maxValue, float currentValue)
    {
        slider.maxValue = maxValue;
        slider.value = currentValue;
        if (transitionCoroutine != null)
        { StopCoroutine(transitionCoroutine); }
    }
}
