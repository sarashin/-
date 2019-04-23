using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public string[] scenarios;
    [SerializeField] Text uiText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay;

    private string currentText;
    private float timeUntilDisplay;
    private float timeElapsed;
    private int currentLine;
    private int lastUpdateCharacter;

    public GameObject TutorialPanel;

    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        TutorialPanel.SetActive(false);
        timeUntilDisplay = 0;
        timeElapsed = 1;
        currentLine = 0;
        lastUpdateCharacter = 0;
        intervalForCharacterDisplay = 0.05f;
        currentText = string.Empty;
    }

    public bool TextControll()
    {
#if UNITY_EDITOR
        if (!TutorialPanel.activeSelf&&Input.GetMouseButtonUp(0))
            TutorialPanel.SetActive(true);
#elif UNITY_ANDROID
        if (!TutorialPanel.activeSelf&&Event.current.type == EventType.MouseUp)
            TutorialPanel.SetActive(true);
#endif
        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (currentLine < scenarios.Length)
                    SetNextLine();
                else
                    return true;
            }
           
        }
        else
        {
            // 完了してないなら文字をすべて表示する
            if (Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }
        }


        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
            Debug.Log(lastUpdateCharacter);

        if (displayCharacterCount != lastUpdateCharacter)
        {
            uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
        return false;
    }
    //テキスト消去
    public void TextBoxClear()
    {
        TutorialPanel.SetActive(false);
    }
    //次位置を計算
    void SetNextLine()
    {
        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = 0;
    }
}