//using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Ump;
using GoogleMobileAds.Ump.Api;

public class UmpManagerz : MonoBehaviour
{
    private ConsentForm _consentForm;
    
    public void ConsentCall()
    {
       
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            ConsentRequestParameters request = new ConsentRequestParameters
            {
                TagForUnderAgeOfConsent = false,
            };
            // Check the current consent information status.
            ConsentInformation.Update(request, OnConsentInfoUpdated);
        }
        //}      
    }

    void OnConsentInfoUpdated(FormError error)
    {
        if (error != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(error);
            return;
        }

        // If the error is null, the consent information state was updated.
        // You are now ready to check if a form is available.
        if (ConsentInformation.IsConsentFormAvailable())
            LoadConsentForm();
    }

    void LoadConsentForm()
    {
        // Loads a consent form.
        ConsentForm.Load(OnLoadConsentForm);
    }


    void OnLoadConsentForm(ConsentForm consentForm, FormError error)
    {
        if (error != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(error);
            return;
        }

        // The consent form was loaded.
        // Save the consent form for future requests.
        _consentForm = consentForm;

        // You are now ready to show the form.
        if (ConsentInformation.ConsentStatus == ConsentStatus.Required)
            _consentForm.Show(OnShowForm);
    }

    void OnShowForm(FormError error)
    {
        if (error != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(error);
            return;
        }

        // Handle dismissal by reloading form.
        LoadConsentForm();
    }

    public void ResetConsentCaller()
    {
        ConsentInformation.Reset();
    }
}
