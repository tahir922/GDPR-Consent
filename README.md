# GDPR-Consent
designed to handle Custom GDPR consent
Configure UMP in AdMob:

Make sure UMP is properly configured in your AdMob account.
Set up the consent messages as required.
Integrate and Test in Unity:

Attach the GDPRPopup script to a GameObject in your scene.
Set the references for popupPanel, popupText, acceptButton, and declineButton.
Set the URLs for privacyPolicyURL and termsOfServiceURL.
Build and Run:
Accept Button Configuration
Select the acceptButton in the Hierarchy.
In the Inspector, under the Button component, add a new On Click event.
Drag the GameObject with the GDPRPopup script to the Object field.
Select GDPRPopup -> OnAccept from the dropdown.
Decline Button Configuration
Select the declineButton in the Hierarchy.
In the Inspector, under the Button component, add a new On Click event.
Drag the GameObject with the GDPRPopup script to the Object field.
Select GDPRPopup -> OnDecline from the dropdown.
Build and run your app on both iOS and Android devices.
Ensure you test in an environment where GDPR applies (e.g., using a VPN to simulate being in the EU).
