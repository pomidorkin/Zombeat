using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class QuestionDialogueUI : MonoBehaviour
{
    // Код манки хуйню нагородил
    public static QuestionDialogueUI Instance { get; private set; }
    [SerializeField] TMP_Text QuestionText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    Action confirmAction;
    Action declineAction;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public void ShowQuestion(string questionText, Action confirmAction, Action declineAction)
    {
        gameObject.SetActive(true);
        this.confirmAction = confirmAction;
        this.declineAction = declineAction;
        QuestionText.text = questionText;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Confirm()
    {
        Hide();
        confirmAction();
    }

    public void Cancel()
    {
        declineAction();
        Hide();
    }
}
