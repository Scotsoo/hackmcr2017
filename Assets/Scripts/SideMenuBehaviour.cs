using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SideMenuBehaviour : MonoBehaviour {


    public class Menu
    {
        public Canvas Canvas { get; private set; }
        public string _tag { get; set; }
        public bool IsVisible { get; set; }

        public Menu(string tag)
        {
            this._tag = tag;
            this.Canvas = Helpers.GetObjectWithTag<Canvas>(this._tag);
        }

        public void Update()
        {
            Debug.Log(_tag);
            Canvas.gameObject.SetActive(IsVisible);
        }
    }

    public class SideMenu : Menu
    {
        public SideMenu(string tag, bool defaultVisible = false) : base(tag)
        {
            IsVisible = defaultVisible;
        }

  
    }

    public class MainMenu : Menu
    {
        public MainMenu(string tag) : base(tag)
        {
        }
    }
 

    private Button showMenuButton;

    private Text showMenuButtonText;

    // main menu wrapper
    private static SideMenu mainMenu;
    // login, register ect
    private static SideMenu defaultMenu;
    // register stuff
    private static SideMenu registerMenu;
    // login stuff
    private static SideMenu loginMenu;

    private static SideMenu sideMenu;

    // Main Menus
    private static MainMenu letterMenu;

    private static MainMenu letterListMenu;

    private List<Menu> menus;
    
    // Use this for initialization
    void Start ()
    {
        sideMenu = new SideMenu("SideMenu", true);
        mainMenu = new SideMenu("SideMenu_Main", true);
        defaultMenu = new SideMenu("SideMenu_Default", true);
        loginMenu = new SideMenu("SideMenu_Login");
        registerMenu = new SideMenu("SideMenu_Register");
        showMenuButton = Helpers.GetObjectWithTag<Button>("SideMenu_ShowMenu");
        showMenuButtonText = showMenuButton.GetComponentInChildren(typeof(Text)).GetComponent<Text>();
        letterMenu = new MainMenu("FullMenu_Main");
        letterListMenu = new MainMenu("FullMenu_Letter_List");
        SetMenuText();
        menus = new List<Menu> { mainMenu, defaultMenu, registerMenu, loginMenu, sideMenu, letterMenu };

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

    public void ViewLetters_OnClick()
    {
        sideMenu.IsVisible = false;
        sideMenu.Update();
        letterMenu.IsVisible = true;
        letterMenu.Update();
        letterListMenu.IsVisible = true;
        letterListMenu.Update();
        SetLetterText();
    }

    public void ViewLetters_Back_OnClick()
    {
        sideMenu.IsVisible = true;
        sideMenu.Update();
        letterMenu.IsVisible = false;
        letterMenu.Update();
        letterListMenu.IsVisible = false;
        letterListMenu.Update();
    }
    // Update is called once per frame
    void Update ()
    {
        foreach (var menu in menus)
        {
            try
            {
                menu.Update();
            }
            catch (NullReferenceException)
            {
                Debug.Log(menu._tag);
            }
        }
    }
    List<string> Letters = new List<string>() {"A", "A","C", "D", "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" };
    void SetLetterText()
    {
        Helpers.GetObjectWithTag<Text>("FullMenu_Letter_List_Text").text = string.Join(",",Letters.ToArray());
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
