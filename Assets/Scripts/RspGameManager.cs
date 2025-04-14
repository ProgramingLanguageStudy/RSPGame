using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 승부 결과 종류
public enum ResultType
{
    Victory,
    Draw,
    Defeat,
}


// 0: 가위, 1: 바위, 2: 보
public class RspGameManager : MonoBehaviour
{
    public const int HandCount = 3; // 가위바위보의 총 개수

    // 결과 메시지 배열
    public string[] ResultMessages = { "이겼습니다!", "무승부", "졌습니다..." };

    // 손패 문자열 배열
    public string[] HandStrings = new string[] { "가위", "바위", "보" };


    public Text ResultText; // 승부 결과 텍스트 컴포넌트
    public Text PlayerRecordText;   // 플레이어 기록 텍스트 컴포넌트
    public Text ComRecordText;      // 컴퓨터 기록 텍스트 컴포넌트
    public Image PlayerHandImage;   // 플레이어 손패 이미지 컴포넌트
    public Image ComHandImage;      // 컴퓨터 손패 이미지 컴포넌트
    public Text ComHandText;        // 컴퓨터 손패 텍스트 컴포넌트

    public Sprite[] HandSprites;    // 손패 스프라이트 배열


    // 플레이어 전적 저장 배열
    // 0번: 승리 수, 1번: 비긴 수, 2번: 패배 수
    public int[] ResultCounts = new int[3];


    // 플레이어 손패(가위 바위 보 중 하나)를 제출하는 함수
    public void SubmitHand(int hand)
    {
        // 오류값이 들어왔을 경우 예외 처리
        if(hand < 0 || hand >= HandCount)
        {
            Debug.Log("가위바위보 중 하나만 낼 수 있습니다.");

            // 함수를 마저 실행하지 않고 중단
            return;
        }

        int playerHand = hand;  // 플레이어의 손패
        int comHand = Random.Range(0, HandCount);   // 컴퓨터의 손패

        // 플레이어 손 UI 갱신
        UpdatePlayerHandUI(playerHand);

        // 컴퓨터 손 UI 갱신
        UpdateComHandUI(comHand);

        // 결과 처리
        ResultType result = ResultType.Draw;
        if((playerHand + 1) % HandCount == comHand)
        {
            result = ResultType.Defeat;
        }
        else if((playerHand + 2) % HandCount == comHand)
        {
            result = ResultType.Victory;
        }

        // 결과 텍스트 UI 갱신
        UpdateResultText(result);

        // 승무패 기록 갱신
        ResultCounts[(int)result]++;

        // 승무패 기록 텍스트 갱신
        UpdateRecordTexts();
    }

    // 플레이어 손 UI 설정하는 함수
    void UpdatePlayerHandUI(int hand)
    {
        PlayerHandImage.sprite = HandSprites[hand];
    }

    // 컴퓨터 손 UI 설정하는 함수
    void UpdateComHandUI(int hand)
    {
        ComHandImage.sprite = HandSprites[hand];
        ComHandText.text = HandStrings[hand];
    }

    // 결과 메시지 표시하는 함수
    void UpdateResultText(ResultType result)
    {
        ResultText.text = ResultMessages[(int)result];
    }

    // 승무패 전적을 텍스트에 갱신
    void UpdateRecordTexts()
    {
        // 문자열 표시 방식
        // 1) string.Format()
        string playerRecordStr = string.Format("{0}승 {1}무 {2}패",
            ResultCounts[(int)ResultType.Victory],
            ResultCounts[(int)ResultType.Draw],
            ResultCounts[(int)ResultType.Defeat]);
        PlayerRecordText.text = playerRecordStr;

        // 2) $""
        string comRecordStr =
            $"{ResultCounts[(int)ResultType.Defeat]}승 " +
            $"{ResultCounts[(int)ResultType.Draw]}무 " +
            $"{ResultCounts[(int)ResultType.Victory]}패";
        ComRecordText.text = comRecordStr;


        //PlayerRecordText.text =
        //    ResultCounts[(int)ResultType.Victory] + "승 " +
        //    ResultCounts[(int)ResultType.Defeat] + "패";

        //ComRecordText.text =
        //    ResultCounts[(int)ResultType.Defeat] + "승 " +
        //    ResultCounts[(int)ResultType.Victory] + "패";
    }
}
