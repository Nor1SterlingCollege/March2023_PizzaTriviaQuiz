using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Question Pool", fileName = "New Question")]
//ScriptableObject = �f�[�^���i�[���Ă����A�K�v�Ȏ��ɓǂݍ���Ŏg���ꍇ�ɕ֗��ł��B
//��������ߖ�ł���
//�p�����[�^���C���X�y�N�^�[��ŊȒP�ɊǗ��ł���
//�ȒP�ɍ������g������ł���
//Unity�G�f�B�^��ŃQ�[�����Đ����Ă���Ƃ��ɒl��ύX�ł���
//�ǂ̃V�[������ł��Q�Ƃ��邱�Ƃ��ł���
public class QuestionSO : ScriptableObject
{
    [TextArea(7,7)]
    [SerializeField] string question = "��Input a new question here��";
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
