using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceManager : MonoBehaviour
{
    private int balance;

    private void Start()
    {
        UpdateBalance();
    }

    public int GetBalance()
    {
        return balance;
    }

    public void UpdateBalance()
    {
        balance = Progress.Instance.playerInfo.coins;
    }

    public bool DeductCoins(int value)
    {
        if ((balance - value) > 0)
        {
            balance -= value;
            Progress.Instance.playerInfo.coins = balance;
            Progress.Instance.Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddBalance(int value)
    {
        balance += value;
        Progress.Instance.playerInfo.coins = balance;
        Progress.Instance.Save();
    }
}
