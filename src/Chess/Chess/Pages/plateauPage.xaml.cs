using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary;

namespace Chess.Pages;

public partial class plateauPage : ContentPage
{
    public Chessboard Board { get; } = new Chessboard(new Case[8, 8], false);

    public plateauPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
}