using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// QuizCanvasにおいて各パーツの調整場所を配置する
public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    public AudioClip m_hitClip;
    public AudioClip m_incorrectClip;
    public AudioClip m_newQuestionClip;

    [Header("Answers")]
    //質問に対する回答が何個あるか？
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;

    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }

        //最後の質問に対して何も回答をしなくて時間切れになった場合、正解を出してゲームオーバーにする
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    //プレーヤーが回答を選択したときの挙動
 
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "slice(s)";
    }

    void DisplayAnswer(int index)
    {
        //プレーヤーが選択した回答のインデッkすと質問プールにある正解のインデックスを比べて同じなら正解おめでとうメッセージを出す

        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
             var audioSource = FindObjectOfType<AudioSource>();
            audioSource.PlayOneShot(m_hitClip,0.05f);
            //おめでとうメッセージを表示し、プレーヤーの選択したのボタンの色をオレンジ色へ変える
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
             var audioSource = FindObjectOfType<AudioSource>();
            audioSource.PlayOneShot(m_incorrectClip, 0.05f);
            //正解インデックスを取って、正解テキストを表示し、残念でしたメッセージを表示し、正解の選択肢のボタン色をオレンジ色へ変える
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was ... \n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
             var audioSource = FindObjectOfType<AudioSource>();
            audioSource.PlayOneShot(m_newQuestionClip, 0.05f);
            //ボタンを押せる状態にして、デフォルトボタン（青色）を表示し、質問をランダムに選び、質問を表示し、プログレスバーをインクリして、終わった質問の数もインクリする
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()
    {
        //ランダム関数で質問のインデックスをランダムに選ぶ
        int index = Random.Range(0, questions.Count);
        //選んだ質問のインデックスを変数保存する
        currentQuestion = questions[index];
        //選んだ質問が前の質問と重複していたら取り除く
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void DisplayQuestion()
    {
        //画面のテキストボックスに選んだ質問を入れる
        questionText.text = currentQuestion.GetQuestion();
        //回答の数だけ繰り返す処理
         for (int i = 0; i < answerButtons.Length; i++)
        {
            //回答インデックス●番のテキストを呼び出し、保存する
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }
    //ボタンを押せる状態にするかのロジック
    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}

