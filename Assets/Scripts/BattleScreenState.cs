using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BattleScreenState : ISceneState
{
    public void HandlePlayButton(SceneTransition context)
    {
        SceneManager.LoadScene("QuestScreen");
    }
}
