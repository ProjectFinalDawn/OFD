using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AWSCognitoAPI : MonoBehaviour
{
    private string region = "ap-south-1"; // Replace with your AWS region
    private string clientId = "2rgsdnv01ndv1p5fcarsjm8v59"; // Replace with your Cognito App Client ID

    // Public method for Sign-Up (called from Unity UI)
    public void SignUp()
    {
        string email = GameObject.Find("Email").GetComponent<TMPro.TMP_InputField>().text;
        string password = GameObject.Find("Password").GetComponent<TMPro.TMP_InputField>().text;

        StartCoroutine(SignUpCoroutine(email, password));
    }

    // Public method for Sign-In (called from Unity UI)
    public void SignIn()
    {
        string usernameOrEmail = GameObject.Find("Email").GetComponent<TMPro.TMP_InputField>().text;
        string password = GameObject.Find("Password").GetComponent<TMPro.TMP_InputField>().text;

        StartCoroutine(SignInCoroutine(usernameOrEmail, password));
    }

    // Public method for Verification
    public void Verify()
    {
        string email = GameObject.Find("Email").GetComponent<TMPro.TMP_InputField>().text;
        string verificationCode = GameObject.Find("VerificationCode").GetComponent<TMPro.TMP_InputField>().text;

        StartCoroutine(VerifyCodeCoroutine(email, verificationCode));
    }

    // Coroutine for Sign-Up
    private IEnumerator SignUpCoroutine(string email, string password)
    {
        string url = $"https://cognito-idp.{region}.amazonaws.com/";

        string jsonBody = $"{{\"ClientId\": \"{clientId}\", \"Username\": \"{email.Split('@')[0]}\", \"Password\": \"{password}\", \"UserAttributes\": [{{\"Name\": \"email\", \"Value\": \"{email}\"}}]}}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/x-amz-json-1.1");
        request.SetRequestHeader("X-Amz-Target", "AWSCognitoIdentityProviderService.SignUp");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Sign-Up successful: " + request.downloadHandler.text);
            GameObject.Find("ScreenManager").GetComponent<ScreenManager>().ShowVerification();
        }
        else
        {
            Debug.LogError("Sign-Up failed: " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }

    // Coroutine for Sign-In
    private IEnumerator SignInCoroutine(string usernameOrEmail, string password)
    {
        string url = $"https://cognito-idp.{region}.amazonaws.com/";

        string jsonBody = $"{{\"AuthFlow\": \"USER_PASSWORD_AUTH\", \"ClientId\": \"{clientId}\", \"AuthParameters\": {{\"USERNAME\": \"{usernameOrEmail}\", \"PASSWORD\": \"{password}\"}}}}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/x-amz-json-1.1");
        request.SetRequestHeader("X-Amz-Target", "AWSCognitoIdentityProviderService.InitiateAuth");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Sign-In successful: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Sign-In failed: " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }

    // Coroutine for Verification
    private IEnumerator VerifyCodeCoroutine(string email, string verificationCode)
    {
        string url = $"https://cognito-idp.{region}.amazonaws.com/";

        string jsonBody = $"{{\"ClientId\": \"{clientId}\", \"Username\": \"{email.Split('@')[0]}\", \"ConfirmationCode\": \"{verificationCode}\"}}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/x-amz-json-1.1");
        request.SetRequestHeader("X-Amz-Target", "AWSCognitoIdentityProviderService.ConfirmSignUp");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Verification successful: " + request.downloadHandler.text);
            GameObject.Find("ScreenManager").GetComponent<ScreenManager>().ShowSignIn();
        }
        else
        {
            Debug.LogError("Verification failed: " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }
}
