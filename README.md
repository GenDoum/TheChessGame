<br/>
<p align="center">
  <a href="#">
    <img src="./src/Chess/Chess/Resources/AppIcon/appicon.svg" alt="Logo" width="250" height="250">
  </a>

  <h3 align="center">The Chess</h3>

  <p align="center">
    The Chess is an application designed for chess enthusiasts. If you are a fan of strategy and critical thinking, The Chess is the perfect app to help you improve your game and challenge your friends.
    <br/>
    <br/>
    <a href="https://codefirst.iut.uca.fr/git/Chess/Chess/wiki"><strong>Link to documentation</strong></a>
  </p>
</p>

![Static Badge](https://img.shields.io/badge/development-in%20progress-green)
[![Quality Gate Status](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Chess&metric=alert_status&token=c2dc7d57b98c8ce69ae71f96129688cc7992b423)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Chess)
[![Maintainability Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Chess&metric=sqale_rating&token=c2dc7d57b98c8ce69ae71f96129688cc7992b423)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Chess)

## Table of Contents

* [The Project](#the-project)
* [Technologies Used](#technologies-used)
* [Contributing to the Project](#contributing)
* [Contributors](#contributors)


## The Project

[to be completed]

## Technologies Used
- [JetBrains Rider](https://www.jetbrains.com/rider/) - IDE
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) - IDE
- [CodeFirst](https://codefirst.iut.uca.fr/) - Technology
- [.NET MAUI 8.0](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui?view=net-maui-8.0) - Framework
    - [XAML](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/xaml/?view=netdesktop-8.0) - Extensible Markup Language
    - [C#](https://learn.microsoft.com/en-us/dotnet/csharp/) - Language
- [Doxygen](https://www.doxygen.nl/) - Documentation

## Current state

The chess is a project in its early stages.

| Tâches                                |Explication                                             | État
| :------------------------------------ |:------------------------------------------------------:|:-----------------------:|
| **Piece Movement Management**         | Each type of piece (King, Queen, Rook, Knight, Bishop, Pawn) has its own movement logic implemented. For example, the King can move one square in any direction, the Queen can move in any direction without distance limit, etc. | :white_check_mark:
| **Movement Exception Handling**       | We have set up exceptions to handle invalid movements. For example, if the bishop tries to move in a non-diagonal direction, an `InvalidMovementException` is thrown. | :white_check_mark:
| **Calculation of Possible Moves**     | set up exceptions to handle invalid movements. For example, if the bishop tries to move in a non-diagonal direction, an `InvalidMovementException` is thrown. | :white_check_mark:
| **Unit Testing**                      | You have written unit tests to verify the validity of the bishop's movements. These tests check valid moves, invalid moves, and possible moves on an empty chessboard. | :white_check_mark:
| **Piece Color Management**            | Each piece has a color (white or black) that is used to determine possible moves and to check if a piece can be captured. | :white_check_mark:
| **Check**                             | The `Check` function verifies if a King is in check. It examines all possible moves of the opposing pieces to see if any of them can reach the King. | :x:
| **Checkmate**                         | The `Checkmate` function verifies if a player is in checkmate. It examines all possible moves of the King and its allied pieces to see if the check can be avoided. If no move can avoid the check, the player is in checkmate. | :x:
| **Piece Capture**                     | Pieces can be captured by moving to a square occupied by an opposing piece. | :white_check_mark:
| **First Move**                        | For pawns and rooks, there is special logic for their first move. For example, a pawn can move two squares on its first move. | :white_check_mark:
| **Pawn Promotion**                    | When a pawn reaches the last row of the board, it can be promoted to another piece (Queen, Rook, Knight, Bishop). | :white_check_mark:
| **Player Management**                 | he game manages two players, alternating turns between them. | :white_check_mark:
| **Move Validation**                   | Before making a move, the game checks if the move is valid according to chess rules. | :white_check_mark:
| **Game Events**                       | The game triggers events when a pawn can be promoted or when a player wins the game. | :white_check_mark:
| **Chessboard Management**             | The chessboard is represented by an 8x8 matrix, and each square can contain a piece or be empty. | :white_check_mark:

## Contributing

### Prerequisites

-   [Git](https://git-scm.com/) - Version Control
-   [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) - Integrated Development Environment (IDE)
-   [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) - Framework

All contributions you make are **greatly appreciated**.

-   If you have any suggestions, feel free to [open an issue](https://codefirst.iut.uca.fr/git/Chess/Chess/issues) to discuss it or create a merge request directly.
-   Create an individual merge request for each suggestion.

### Creating a Merge Request

1.  Fork the project
2.  Create your feature branch:  
```bash
$ git checkout -b feature/awesomeFeature
```
3.  Make your changes:
```bash
$ git commit -m 'Add a new feature'
```
4.  Push to the branch:
```
$ git push origin feature/awesomeFeature
```
5.  Open a merge request


## Contributors

* [Yannis Doumir-Fernandes](https://codefirst.iut.uca.fr/git/yannis.doumir_fernandes)
* [Mathéo Balko](https://codefirst.iut.uca.fr/git/matheo.balko) 
* [Mathéo Hersan](https://codefirst.iut.uca.fr/git/matheo.hersan)  
