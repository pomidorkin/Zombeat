using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.MUIP;

public class NotificationsManager : MonoBehaviour
{
    [SerializeField] private NotificationManager notification;

    public void TriggerNotification(string title, string description)
    {
        //notification.icon = spriteVariable; // Change icon
        notification.title = title; // Change title
        notification.description = description; // Change desc
        //notification.UpdateUI(); // Update UI
        notification.Open(); // Open notification
        //notification.Close(); // Close notification
        //notification.onOpen.AddListener(TestFunction); // Invoke open events
        //notification.onClose.AddListener(TestFunction); // Invoke close events
    }
}