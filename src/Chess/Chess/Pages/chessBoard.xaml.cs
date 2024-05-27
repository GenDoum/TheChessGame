using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChessLibrary;

namespace Chess.Pages;

public partial class chessBoard : ContentPage
{

    public Chessboard Board { get; } = new Chessboard(new Case[8, 8], false);
    
    public chessBoard()
    {
        InitializeComponent();
        BindingContext = this;
    }
    
}