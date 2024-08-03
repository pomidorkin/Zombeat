using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public void LoadScene(int sceneId)
    {
        QuestionDialogueUI.Instance.ShowQuestion("(Testing) Are you Sure you want to stert the game?", () => {
            QuestionDialogueUI.Instance.ShowQuestion("(Testing) Are you really sure?", () =>
            {
                SceneManager.LoadScene(sceneId);
            }, () => { });
        }, () => { });
    }
}
