using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Example2 : MonoBehaviour
{

    public Button addButton, resetButton;

    public Text scoreText, highscoreText;

    public IntReactiveProperty score = new IntReactiveProperty(0);

    // Use this for initialization
    void Start () {

        // Score from example 1 ( see example for more comments ) 
        score.SubscribeToText(scoreText);
        score.Subscribe(_ => AnimateObj(scoreText.gameObject));


        // Highscore ---------------------------

        // Scans through all values and gets the highest ( Math.Max( last, new ) ) and use it to compare with future values
        var highscore = score.Scan(int.MinValue, Mathf.Max).ToReactiveProperty();

        // Change text when highscore changes ( we format the string to BEST x before setting it to text )
        highscore.SubscribeToText(highscoreText, x => string.Format("BEST {0}", x));

        // Add animation when highscore changes
        highscore.Subscribe(_ => AnimateObj(highscoreText.gameObject));


        // Button actions ------------------------
        addButton.OnPointerDownAsObservable().Subscribe(ScoreUp);
        resetButton.OnPointerUpAsObservable().Subscribe(ScoreZero);
    }

    public void AnimateObj(GameObject go) {
        LeanTween.scale(go, Vector3.one, 0.2f)
            .setFrom(Vector3.one*0.5f)
            .setEase(LeanTweenType.easeOutBack);
    }
    
    protected virtual void ScoreUp(PointerEventData eventData)
    {
        score.Value++;
    }
    protected virtual void ScoreZero(PointerEventData eventData)
    {
        score.Value = 0;
    }
}
