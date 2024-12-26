using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ISceneState
{
    void HandlePlayButton(SceneTransition context);
}
