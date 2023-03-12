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
        //�񓚒��t���O�I��
        if(isAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                    // ���ꂽ���� SE ���Đ�����
            var audioSource = FindObjectOfType<AudioSource>();
    //audioSource.PlayOneShot(m_clockingClip,0.09f);
                //�܂��������ԓ��F�c�莞�Ԃ𐧌����ԂŊ���
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                //�������Ԑ؂�F������\�����ĉ񓚒��t���O���I�t�ɂ���
                isAnsweringQuestion = false;
                timerValue = timeToRevealCorrectAnswer;
            }
        }
        //�񓚒��t���O�I�t
        else
        {
            if(timerValue > 0)
            {
                //�܂��������ԓ��F�c�莞�Ԃ��񓚊J�����ԂŊ���
                fillFraction = timerValue / timeToRevealCorrectAnswer;
            }
            else
            {
                //�������Ԑ؂�F���̎���ׂ̈̏����Ƃ��ĉ񓚒��t���O���I���ɂ��āA���Ԃ𐧌����ԕ��Ƀ��Z�b�g���āA���̎���ւ���t���O���I���ɂ���
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
