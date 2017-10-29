using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SideMenuBehaviour : MonoBehaviour
{


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
    private static MainMenu complaintSubmitMenu;

    private List<Menu> menus;

    private Inventory _inventory;
    private InputField _complaintInput;
    private Text _complaintRemaining;
    // Use this for initialization
    void Start()
    {
        sideMenu = new SideMenu("SideMenu", true);
        mainMenu = new SideMenu("SideMenu_Main", false);
        defaultMenu = new SideMenu("SideMenu_Default", true);
        loginMenu = new SideMenu("SideMenu_Login");
        registerMenu = new SideMenu("SideMenu_Register");
        showMenuButton = Helpers.GetObjectWithTag<Button>("SideMenu_ShowMenu");
        showMenuButtonText = showMenuButton.GetComponentInChildren(typeof(Text)).GetComponent<Text>();
        letterMenu = new MainMenu("FullMenu_Main");
        letterListMenu = new MainMenu("FullMenu_Letter_List");
        complaintSubmitMenu = new MainMenu("FullMenu_Letter_Complaint");
        SetMenuText();
        menus = new List<Menu> { mainMenu, defaultMenu, registerMenu, loginMenu, sideMenu, letterMenu, letterListMenu, complaintSubmitMenu };
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _complaintInput = Helpers.GetObjectWithTag<InputField>("FullMenu_Complaint_Input");
        _complaintInput.onValueChanged.AddListener(delegate { ComplaintOnChange(); });
        _complaintInput.lineType = InputField.LineType.MultiLineNewline;
        _complaintRemaining = Helpers.GetObjectWithTag<Text>("FullMenu_Complaint_Remaining");
    }

    public void ComplaintOnChange()
    {
        if (!surpress)
            CalculateRemainingLetters();
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

    public void MakeComplaint_OnClick()
    {
        sideMenu.IsVisible = false;
        sideMenu.Update();
        letterMenu.IsVisible = true;
        letterMenu.Update();
        complaintSubmitMenu.IsVisible = true;
        complaintSubmitMenu.Update();
        CalculateRemainingLetters();
    }

    public void MakeComplaint_Submit_OnClick()
    {
        MakeComplaint(_complaintInput.text);
    }
    public void ViewLetters_Back_OnClick()
    {
        sideMenu.IsVisible = true;
        sideMenu.Update();
        letterMenu.IsVisible = false;
        letterMenu.Update();
        letterListMenu.IsVisible = false;
        letterListMenu.Update();
        complaintSubmitMenu.IsVisible = false;
        complaintSubmitMenu.Update();
    }
    // Update is called once per frame
    void Update()
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
    //List<string> Letters = new List<string>() {"A", "A","C", "D", "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A",
    //    "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D" , "A", "A", "C", "D",
    //"M","M","M","M","M","J","j","J","j","J","J","J",};
    void SetLetterText()
    {
        var items = _inventory.Dump();
        Helpers.GetObjectWithTag<Text>("FullMenu_Letter_List_Text").text = string.Join(",", items.Select(i => string.Format("{0}({1})", i.Character, i.CaptureCount)).ToArray());

    }

    private string lastText = "";
    private bool surpress = false;
    void CalculateRemainingLetters()
    {
        var text = _complaintInput.text;
        var items = _inventory.Dump().ToArray().ToList();
        var newText = "";
        var txList = text.ToList().Select(i => i.ToString().ToUpper()).ToList();
        var len = lastText.Length;
        if (len > text.Length)
        {
            for (var i = 0; i < len - text.Length; i++)
            {
                var item = items.FirstOrDefault(o => o.Character == lastText[len - 1 -i].ToString());
                if (item != null)
                    item.CaptureCount += 1;
            }
            newText = text.ToUpper();
            lastText = newText;

        }
        else if (txList.Any())
        {
            newText += lastText;
            var last = txList.Last();
            var first = items.FirstOrDefault(i => i.Character == last);
            if (last == " ")
            {
                newText += " ";
                lastText = newText;
            }
            else if (first != null)
            {
                if (first.CaptureCount != 0)
                {
                    newText += last;
                    lastText = newText;
                    first.CaptureCount -= 1;
                }
            }
        }
        var outArray = items.Select(i => string.Format("{0}({1})", i.Character, i.CaptureCount)).ToList();
        outArray.Sort();
        if (newText != text)
        {
            //hack to surpress this re-triggering this to retrigger this to retrigger this to retrigger this to retrigger this to retrigger this to retrigger this to retrigger this to retrigger this to retrigger this to retrigger this to retrigger this...
            surpress = true;
            _complaintInput.text = newText;
            surpress = false;
        }
        _complaintRemaining.text = "Letters Remaining: \n" + string.Join(" ", outArray.ToArray());

    }

    void MakeComplaint(string complaint)
    {
        var form = new WWWForm();
        form.AddField("complaint", complaint);
        var www = UnityWebRequest.Post("localhost:3000/complain", form);
        www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var input = _complaintInput.text.ToUpper().ToArray().Select(i => i.ToString()).ToList();
            for (var i = 0; i < input.Count(); i++)
            {
                _inventory.Remove(input[i]);
            }
            _complaintInput.text = "";
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            Debug.Log("Form upload complete!");
        }
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

