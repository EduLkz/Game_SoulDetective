using UnityEngine;
using UnityEngine.SceneManagement;

public class creditosButton : controlaColisao
{
    //creditos
    protected override void buttonAction()
    {
        base.buttonAction();
        Debug.Log("Chamou os creditos");
        SceneManager.LoadScene(2);
    }
}
