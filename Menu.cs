using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.Purchasing;

public class Menu : MonoBehaviour {

    // private IStoreController controller;
    // private IExtensionProvider extensions;

    public PlusMusic_DJ theDJ;
    enum PlayState {start, play};
    PlayState theState = new PlayState();
    public GameObject Settings;

    void Start()
    {
        // if (!GameServices.Instance.IsLoggedIn()) {
        //     GameServices.Instance.LogIn(LoginResult);
        // } else {

        // }

        // theDJ.soundtrackID = "347";
        theDJ.ChangeSoundtrack("347", true);
        theState = PlayState.start;
        
    }


    void Update()
    {
        if(theDJ.AllFilesLoaded()){
            if(theState == PlayState.start){
                theState = PlayState.play;
                theDJ.PlaySoundPM("backing_track", "hardstop", "beats", false);
            }
        }
        
    }

    private void LoginResult(bool success) {
        if (success == true)
        {
            //Login was successful
        }
        else
        {
            //Login failed
        }
        
        GleyGameServices.ScreenWriter.Write("Login success: " + success);
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    public void ShowLeaderboard() {
        GameServices.Instance.ShowSpecificLeaderboard(LeaderboardNames.Leaderboard);
        // GameServices.Instance.ShowLeaderboadsUI();
    }

    public void ShowMenu() {
        Settings.SetActive(true);
    }

    public void CloseMenu() {
        Settings.SetActive(false);
    }

    public void WebsiteOcto(){
        Application.OpenURL("https://www.octo.co/");
    }

    public void WebsitePrivacy(){
        Application.OpenURL("https://www.octo.co/privacy");
    }


}
