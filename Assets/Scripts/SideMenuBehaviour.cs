using System;
using System.Collections;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SideMenuBehaviour : MonoBehaviour {

    public class SideMenu
    {
        public SideMenu(string tag, bool defaultVisible = false)
        {
            this._tag = tag;
            this.Canvas = Helpers.GetObjectWithTag<Canvas>(this._tag);
            IsVisible = defaultVisible;
        }
        public Canvas Canvas { get; private set; }
        private string _tag { get; set; }
        public bool IsVisible { get; set; }

        public void Update()
        {
            Canvas.gameObject.SetActive(IsVisible);
        }
    }

    private Button showMenuButton;

    private Text showMenuButtonText;

    // main menu wrapper
    private SideMenu mainMenu;
    // login, register ect
    private SideMenu defaultMenu;
    // register stuff
    private SideMenu registerMenu;
    // login stuff
    private SideMenu loginMenu;

    // Use this for initialization
    void Start ()
    {
        mainMenu = new SideMenu("SideMenu_Main", true);
        defaultMenu = new SideMenu("SideMenu_Default", true);
        loginMenu = new SideMenu("SideMenu_Login");
        registerMenu = new SideMenu("SideMenu_Register");
        showMenuButton = Helpers.GetObjectWithTag<Button>("SideMenu_ShowMenu");
        showMenuButtonText = showMenuButton.GetComponentInChildren(typeof(Text)).GetComponent<Text>();
        SetMenuText();
    }

    void SetMenuText()
    {
        showMenuButtonText.text = (mainMenu.IsVisible ? "Hide" : "Show") + " Menu";
    }
    public void Login_OnClick()
    {
        Debug.Log("CLICKED");
        defaultMenu.IsVisible = false;
        loginMenu.IsVisible = true;
        mainMenu.Update();
        loginMenu.Update();

    }

    public void Login_Submit_Onclick()
    {
        StartCoroutine(Login());
    }

    public void Register_Submit_OnClick()
    {
        var username = Helpers.GetObjectWithTag<InputField>("SideMenu_Register_Username").text;
        var password = Helpers.GetObjectWithTag<InputField>("SideMenu_Register_Password").text;
        var passwordConfirm = Helpers.GetObjectWithTag<InputField>("SideMenu_Register_ConfirmPassword").text;
        if (password != passwordConfirm) return;

        StartCoroutine(Register(username, password));

    }
    public void Register_OnClick()
    {
        defaultMenu.IsVisible = false;
        registerMenu.IsVisible = true;
        mainMenu.Update();
        registerMenu.Update();

    }
    public void HideMenu_OnClick()
    {
        mainMenu.IsVisible = !mainMenu.IsVisible;
        mainMenu.Update();
        SetMenuText();
    }



    public void Back_OnClick()
    {
        loginMenu.IsVisible = false;
        registerMenu.IsVisible = false;
        defaultMenu.IsVisible = true;

    }
    // Update is called once per frame
    void Update ()
    {
        mainMenu.Update();
        defaultMenu.Update();
        registerMenu.Update();
        loginMenu.Update();
        
    }

    IEnumerator Register(string username, string password)
    {
        var form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        var www = UnityWebRequest.Post("localhost:3000", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            Debug.Log("Form upload complete!");
        }
    }
    IEnumerator Login()
    {
        var username = Helpers.GetObjectWithTag<InputField>("SideMenu_Login_Username");
        var password = Helpers.GetObjectWithTag<InputField>("SideMenu_Login_Password");
        var form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        var www = UnityWebRequest.Post("localhost:3000", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            Debug.Log("Form upload complete!");
        }
    }
}
