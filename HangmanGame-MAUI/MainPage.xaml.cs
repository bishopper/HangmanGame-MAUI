using System.ComponentModel;

namespace HangmanGame_MAUI;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private List<char> letters = new List<char>();

    public List<char> Letters
    {
        get { return letters; }
        set { letters = value; OnPropertyChanged(); }
    }

    private string spotLight;

    public string SpotLight
    {
        get { return spotLight; }
        set { spotLight = value; OnPropertyChanged(); }
    }

    private string gameStatus;

    public string GameStatus
    {
        get { return gameStatus; }
        set { gameStatus = value; OnPropertyChanged(); }
    }

    private string errorStatus  = $"تعداد اشتباه : 0 از 15";

    public string ErrorStatus
    {
        get { return errorStatus; }
        set { errorStatus = value; OnPropertyChanged(); }
    }

    private string imageName = "img0.jpg";

    public string ImageName
    {
        get { return imageName; }
        set { imageName = value; OnPropertyChanged(); }
    }


    List<string> Words = new List<string>()
    {
        "اسب",
        "شتر",
        "گاو",
        "سوسک",
        "غاز",
        "فیل",
        "خرس",
        "خرگوش",
        "شاهین",
        "اهو",
        "گوزن",
        "خر",
    };

    public string answer = "";
    public int mistakes = 0;
    public int maxMistakes = 15;

    //ابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهی
    //ABCDEFGHIJKLMNOPQRSTUVWXYZ
    public MainPage()
    {
        InitializeComponent();
        Letters.AddRange("ابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهی");
        BindingContext = this;
        PickWord();
        UpdateSpotLight(answer, guessList);
    }

    public void PickWord()
    {
        answer = Words[new Random().Next(0, Words.Count)].ToUpper();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var selectedLetter = btn.Text;
        btn.IsEnabled = false;
        HandleGeuss(selectedLetter[0]);
    }

    List<char> guessList = new List<char>();

    public void HandleGeuss(char letter)
    {
        if (guessList.IndexOf(letter) == -1)
        {
            guessList.Add(letter);
        }
        if (answer.IndexOf(letter) > 0)
        {
            UpdateSpotLight(answer, guessList);
            CheakIfWon();
        }
        else if (answer.IndexOf(letter) == -1)
        {
            mistakes++;
            UpdateErrorStatus();
            CheakIfLost();
            ImageName = $"img{mistakes}.jpg";
        }
    }

    public void UpdateSpotLight(string answer, List<char> guess)
    {
        var temp = answer.Select(x => (guess.IndexOf(x) >= 0) ? x : '_').ToArray();
        SpotLight = string.Join(" ", temp);
    }

    public void CheakIfWon()
    {
        if (SpotLight.Replace(" ", "") == answer)
        {
            GameStatus = "بردی !!! هورا";
            DisableLetters();
        }
    }

    public void CheakIfLost()
    {
        if (mistakes == maxMistakes)
        {
            GameStatus = "باختی !!! عیب نداره دوباره";
            DisableLetters();
        }
    }

    public void DisableLetters()
    {
        foreach (var child in ContainerBtn.Children)
        {
            var btn = child as Button;
            if (btn != null)
            {
                btn.IsEnabled = false;
            }
        }
    }

    public void EnableLetters()
    {
        foreach (var child in ContainerBtn.Children)
        {
            var btn = child as Button;
            if (btn != null)
            {
                btn.IsEnabled = true;
            }
        }
    }

    public void UpdateErrorStatus()
    {
        ErrorStatus = $"تعداد اشتباه : {mistakes} از {maxMistakes}";
    }

    private void ResetBtn_Clicked(object sender, EventArgs e)
    {
        mistakes = 0;
        guessList = new List<char>();
        ImageName = "img0.jpg";
        PickWord();
        UpdateSpotLight(answer,guessList);
        GameStatus = "";
        UpdateErrorStatus();
        EnableLetters();
    }
}

