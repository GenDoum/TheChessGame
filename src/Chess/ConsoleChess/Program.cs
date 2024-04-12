// See https://aka.ms/new-console-template for more information
using System;
using System.Reflection.Metadata;
using ChessLibrary;

Console.WriteLine("Hello, World!");

ChessLibrary.User a = new User();
User b = new User("Jorge", "ouho", Color.Black);


Console.WriteLine(a.Pseudo);
Console.WriteLine(b.Pseudo);
Console.WriteLine("Hello, World!");
