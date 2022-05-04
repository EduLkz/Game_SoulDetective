using UnityEngine;

public class exitButton : controlaColisao
{

    protected override void buttonAction()
    {
        base.buttonAction();
        Application.Quit();
        
    }
}
