using ChessLibrary;
using System;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            King king = new King(Color.White, new Case("A", 4));
        }
    }
}