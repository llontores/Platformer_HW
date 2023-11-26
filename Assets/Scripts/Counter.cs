using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _coinsAmount = -1;

    private void OnEnable()
    {
        Coin.Collected += ShowAmount;
        ShowAmount();
    }

    private void OnDisable()
    {
        Coin.Collected -= ShowAmount;
    }

    private void ShowAmount()
    {
        _coinsAmount++;
        _text.text = "Монеток : " + _coinsAmount.ToString();
    }
}
