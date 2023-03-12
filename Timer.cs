using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 10f;
    [SerializeField] float timeToRevealCorrectAnswer = 5f;

    public bool loadNextQuestion;
    public float fillFraction;
    public bool isAnsweringQuestion;
    public AudioClip m_clockingClip;
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {

     
    timerValue -= Time.deltaTime;
        Debug.Log(timerValue);
        //回答中フラグオン
        if(isAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                    // やられた時の SE を再生する
            var audioSource = FindObjectOfType<AudioSource>();
    //audioSource.PlayOneShot(m_clockingClip,0.09f);
                //まだ制限時間内：残り時間を制限時間で割る
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                //もう時間切れ：正解を表示して回答中フラグをオフにする
                isAnsweringQuestion = false;
                timerValue = timeToRevealCorrectAnswer;
            }
        }
        //回答中フラグオフ
        else
        {
            if(timerValue > 0)
            {
                //まだ制限時間内：残り時間を回答開示時間で割る
                fillFraction = timerValue / timeToRevealCorrectAnswer;
            }
            else
            {
                //もう時間切れ：次の質問の為の準備として回答中フラグをオンにして、時間を制限時間分にリセットして、次の質問へうつるフラグをオンにする
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
