using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Question Pool", fileName = "New Question")]
//ScriptableObject = データを格納しておき、必要な時に読み込んで使う場合に便利です。
//メモリを節約できる
//パラメータをインスペクター上で簡単に管理できる
//簡単に作ったり使ったりできる
//Unityエディタ上でゲームを再生しているときに値を変更できる
//どのシーンからでも参照することができる
public class QuestionSO : ScriptableObject
{
    [TextArea(7,7)]
    [SerializeField] string question = "★Input a new question here★";
    [SerializeField] string[] answers = new string[3];
    [SerializeField] int correctAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
}
