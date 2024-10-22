
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeEfect : MonoBehaviour
{
    [SerializeField] private Image imageFade;

    public void SceenFade(float targetAnpha, float Duration, System.Action oncomplete = null)
    {
        StartCoroutine(FadeCoroutine(targetAnpha, Duration, oncomplete));
    }
  

    private IEnumerator FadeCoroutine(float targetAnpha, float Duration, System.Action oncomplete)
    {
        float time = 0;
        Color currentCollor = imageFade.color;
        float startAnpha = currentCollor.a;
        while (time < Duration) 
        {
            time += Time.deltaTime;
            float anpha = Mathf.Lerp(startAnpha, targetAnpha, time/ Duration); 
            imageFade.color = new Color(currentCollor.r,currentCollor.g,currentCollor.b,anpha);
            yield return null;
        }
        imageFade.color = new Color(currentCollor.r, currentCollor.g, currentCollor.b, targetAnpha);
        oncomplete?.Invoke();
       
    }    
    
 
}
