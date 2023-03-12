using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    public ScoreKeeper scoreKeeper;
    public AudioClip m_endScreenClip;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {

        finalScoreText.text = "Congratulations!\nYou got a score of " +
                                scoreKeeper.CalculateScore() + "slice(s)!";
        // EndScreen���� SE ���Đ�����
        var audioSource = FindObjectOfType<AudioSource>();
        audioSource.PlayOneShot(m_endScreenClip);
    }
}
