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

  <h2 align="center">Promotionnal video</h3>
  <p align="center">
  <a href="https://opencast.dsi.uca.fr/paella/ui/watch.html?id=cdb94fa1-d62b-4b23-83eb-f84f0fecf61d" align="center">Link to the video</a> </p>
</p>

![Static Badge](https://img.shields.io/badge/development-in%20progress-green)
[![Quality Gate Status](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Chess&metric=alert_status&token=c2dc7d57b98c8ce69ae71f96129688cc7992b423)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Chess)
[![Maintainability Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Chess&metric=sqale_rating&token=c2dc7d57b98c8ce69ae71f96129688cc7992b423)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Chess)

## Table of Contents

* [The Project](#the-project)
* [Technologies Used](#technologies-used)
* [Current state of the project](#current-state)
* [How to play](#how-to-play)
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

| Features                              | Explanation                                            | State
| :------------------------------------ |:------------------------------------------------------:|:-----------------------:|
| **Piece Movement Management**         | Each type of piece (King, Queen, Rook, Knight, Bishop, Pawn) has its own movement logic implemented. For example, the King can move one square in any direction, the Queen can move in any direction without distance limit, etc. | :white_check_mark:
| **Movement Exception Handling**       | We have set up exceptions to handle invalid movements. For example, if the bishop tries to move in a non-diagonal direction, an `InvalidMovementException` is thrown. | :white_check_mark:
| **Calculation of Possible Moves**     | For each piece, we have a PossibleMoves method that calculates all the squares to which the piece can legally move. | :white_check_mark:
| **Unit Testing**                      | We have written unit tests to verify the validity of all pieces movements. These tests check valid moves, invalid moves, and possible moves. | :white_check_mark:
| **Piece Color Management**            | Each piece has a color (white or black) that is used to determine possible moves and to check if a piece can be captured. | :white_check_mark:
| **Check**                             | The `Check` function verifies if a King is in check. It examines all possible moves of the opposing pieces to see if any of them can reach the King. | :x:
| **Checkmate**                         | The `Checkmate` function verifies if a player is in checkmate. It examines all possible moves of the king and its allied pieces to see if the check can be avoided. If no move can avoid the check, the player is in checkmate. We can have a checkmate, but if the king is stuck between other pieces, the player cannot move those pieces to try to free the king. | :x:
| **Piece Capture**                     | Pieces can be captured by moving to a square occupied by an opposing piece. | :white_check_mark:
| **First Move**                        | For pawns and rooks, there is special logic for their first move. For example, a pawn can move two squares on its first move. | :white_check_mark:
| **Pawn Promotion**                    | When a pawn reaches the last row of the board, it can be promoted to another piece (Queen, Rook, Knight, Bishop). | :white_check_mark:
| **Player Management**                 | he game manages two players, alternating turns between them. | :white_check_mark:
| **Move Validation**                   | Before making a move, the game checks if the move is valid according to chess rules. | :white_check_mark:
| **Game Events**                       | The game triggers events when a pawn can be promoted or when a player wins the game. | :white_check_mark:
| **Chessboard Management**             | The chessboard is represented by an 8x8 matrix, and each square can contain a piece or be empty. | :white_check_mark:
## How to Play

### User Registration and Login

Before you can start playing the game, you will be asked to log in. Here's how you can do it:
However, you can register or start a game without a password.

### User Login

To register as a new user, you will be asked to enter a unique username and a password. In our application, there are 2 users saved. They are: 
- Username: MatheoB, Password: chef 
- Username: MatheoH, Password: proMac.

If you enter an incorrect username or password, you will be asked to try again.

### Exiting the Game

If you want to exit the game, you can do so by selecting the 'Exit' option from the main menu.

```bash
Welcome to the Chess Game!
Please select an option:
1. Register
2. Login
3. Exit
> 3
Thank you for playing the Chess Game. Goodbye!
```

Actually to chose an option we don't have to type 3, 2, etc... You just have to press your arrow key and press enter to chose an option.

### Starting the Game

When you start the game, you will be presented with a chessboard. The chessboard is an **8x8 grid**, with each cell identified by a unique coordinate. The horizontal axis is labeled from **'a' to 'h'** and the vertical axis is labeled from **'1' to '8'**.

The game will start with **Player 1's turn**.

### Moving a Piece

When it's your turn, you will be asked to enter the position of the piece you want to move. The position should be entered in the format of a letter followed by a number (e.g., **'a1'**, **'f7'**).

For example, if you want to move the piece at position **'e2'**, you would type **'e2'** and press enter.

The game will then ask you to enter the destination for the piece. Again, you should enter the destination in the same format. For example, if you want to move the piece from **'e2'** to **'e4'**, you would type **'e4'** and press enter.

If the move is **valid**, the piece will be moved to the new position. If the move is **not valid**, you will be asked to enter a different move.

Here's an example : 
```bash
   a   b   c   d   e   f   g   h
 +---+---+---+---+---+---+---+---+
8 | R | C | B | Q | K | B | C | R | 8
 +---+---+---+---+---+---+---+---+
7 | P | P | P | P | P | P | P | P | 7
 +---+---+---+---+---+---+---+---+
6 |   |   |   |   |   |   |   |   | 6
 +---+---+---+---+---+---+---+---+
5 |   |   |   |   |   |   |   |   | 5
 +---+---+---+---+---+---+---+---+
4 |   |   |   |   |   |   |   |   | 4
 +---+---+---+---+---+---+---+---+
3 |   |   |   |   |   |   |   |   | 3
 +---+---+---+---+---+---+---+---+
2 | P | P | P | P | P | P | P | P | 2
 +---+---+---+---+---+---+---+---+
1 | R | C | B | Q | K | B | C | R | 1
 +---+---+---+---+---+---+---+---+
   a   b   c   d   e   f   g   h
   
Player 1's turn
Enter the position of the piece you want to move (a1, f7 ...):
```

### Capturing a Piece

If your piece's move ends on a square occupied by an opponent's piece, the opponent's piece is **captured** and removed from the game.

### Check and Checkmate

If your move places the opponent's king under attack, that's called **"check"**. If your opponent's king is in check, they must make a move that eliminates the threat of capture on the next move. If they cannot do so, that's called **"checkmate"**, and you win the game.

### Pawn Promotion

If a pawn reaches the opposite side of the board, it can be **promoted** to any other piece (except the king). The game will ask you to choose between **'Queen'**, **'Rook'**, **'Bishop'**, or **'Knight'**. Type your choice and press enter.

Here's how it looks in the game:

### Ending the Game

The game ends when a player is **checkmated** (or if a player **resigns**. To resign, type **'resign'** instead of a move.)

Remember, the goal of the game is to **checkmate** your opponent's king. **Good luck!**

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
