using UnityEngine;
using UnityEngine.SceneManagement;

public class newGameButton : controlaColisao
{
    //newGame
    protected override void buttonAction()
    {
        base.buttonAction();
        Debug.Log("Chamou o game");

        SceneManager.LoadScene(1);
    }
}