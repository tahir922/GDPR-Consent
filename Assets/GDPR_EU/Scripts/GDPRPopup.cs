using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Ump.Api;
using GoogleMobileAds.Ump.Common;

public class GDPRPopup : MonoBehaviour
{
    [Header("Popup Elements")]
    public GameObject popupPanel;
    public Text popupText;
    public Button acceptButton;
    public Button declineButton;

    [Header("Links")]
    public string privacyPolicyURL;
    public string termsOfServiceURL;

    private const string GDPRConsentKey = "GDPR_CONSENT_GIVEN";
    private ConsentForm consentForm;

    void Start()
    {
        if (!HasGivenConsent())
        {
            // Check internet connectivity
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                RequestConsentInfoUpdate();
            }
            else
            {
                Debug.LogError("No internet connection available.");
            }
        }
        //else
        //{
        //    LoadMainMenu();
        //}
    }

    private bool HasGivenConsent()
    {
        return PlayerPrefs.GetInt(GDPRConsentKey, 0) == 1;
    }

    private void RequestConsentInfoUpdate()
    {
        ConsentRequestParameters requestParameters = new ConsentRequestParameters
        {
            TagForUnderAgeOfConsent = false
        };

        ConsentInformation.Update(requestParameters, OnConsentInfoUpdated);
    }

    private void OnConsentInfoUpdated(FormError error)
    {
        if (error != null)
        {
            Debug.LogError("Consent info update failed: " + error.Message);
            ShowCustomGDPRPanel();
        }
        else
        {
            if (ConsentInformation.IsConsentFormAvailable())
            {
                LoadConsentForm();
            }
            else
            {
                ShowCustomGDPRPanel();
            }
        }
    }

    private void LoadConsentForm()
    {
        ConsentForm.Load(OnLoadConsentForm);
    }

    private void OnLoadConsentForm(ConsentForm form, FormError error)
    {
        if (error != null)
        {
            Debug.LogError("Consent form load failed: " + error.Message);
            ShowCustomGDPRPanel();
        }
        else
        {
            consentForm = form;
            if (ConsentInformation.ConsentStatus == ConsentStatus.Required)
            {
                ShowCustomGDPRPanel();
            }
            //else
            //{
            //    LoadMainMenu();
            //}
        }
    }

    private void ShowCustomGDPRPanel()
    {
        popupPanel.SetActive(true);
        acceptButton.onClick.AddListener(OnAccept);
        declineButton.onClick.AddListener(OnDecline);
    }

    public void OnAccept()
    {
        PlayerPrefs.SetInt(GDPRConsentKey, 1);
        PlayerPrefs.Save();
        popupPanel.SetActive(false);
        //LoadMainMenu();
    }

    public void OnDecline()
    {
        Application.Quit();
    }

    //private void LoadMainMenu()
    //{
    //    // Load your main menu scene
    //    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    //}

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL(privacyPolicyURL);
    }

    public void OpenTermsOfService()
    {
        Application.OpenURL(termsOfServiceURL);
    }

    public void ResetConsent()
    {
        ConsentInformation.Reset();
        PlayerPrefs.DeleteKey(GDPRConsentKey);
    }
}
