using Chess.Pages;

namespace Chess.contentView;

public partial class viewLeaderBoard : ContentView
{

    public static readonly BindableProperty PseudoProperty = BindableProperty.Create(nameof(Pseudo), typeof(string), typeof(viewLeaderBoard), default(string));
    public static readonly BindableProperty ScoreWithSuffixProperty = BindableProperty.Create(nameof(ScoreWithSuffix), typeof(string), typeof(viewLeaderBoard), default(string));


    public string Pseudo
    {
        get => (string)GetValue(PseudoProperty);
        set => SetValue(PseudoProperty, value);
    }
    public string ScoreWithSuffix 
    {
        get => (string)GetValue(ScoreWithSuffixProperty);
        set => SetValue(ScoreWithSuffixProperty, value);
    }
    public viewLeaderBoard()
    {
        InitializeComponent();
    }
}



