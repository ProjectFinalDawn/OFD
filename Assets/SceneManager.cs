using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject pressAnyButtonPanel;
    public GameObject signUpPanel;
    public GameObject signInPanel;
    public GameObject verificationPanel; // Added for verification

    void Start()
    {
        ShowPressAnyButton();
    }

    public void ShowPressAnyButton()
    {
        pressAnyButtonPanel.SetActive(true);
        signUpPanel.SetActive(false);
        signInPanel.SetActive(false);
        verificationPanel.SetActive(false);
    }

    public void ShowSignUp()
    {
        pressAnyButtonPanel.SetActive(false);
        signUpPanel.SetActive(true);
        signInPanel.SetActive(false);
        verificationPanel.SetActive(false);
    }

    public void ShowSignIn()
    {
        pressAnyButtonPanel.SetActive(false);
        signUpPanel.SetActive(false);
        signInPanel.SetActive(true);
        verificationPanel.SetActive(false);
    }

    public void ShowVerification()
    {
        pressAnyButtonPanel.SetActive(false);
        signUpPanel.SetActive(false);
        signInPanel.SetActive(false);
        verificationPanel.SetActive(true);
    }

    public void OnPressAnyButton()
    {
        Debug.Log("Any Button Pressed!");
        ShowSignUp();
    }
}
