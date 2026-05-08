# Chess Board Diagram Generator

A .NET 8 WinForms application for creating and manipulating chess board layouts. Designed for educators to quickly generate board images for classes and study guides.

## Features
* **Manual Setup:** Move, add, or delete pieces without "illegal move" restrictions.
* **Lightweight:** Uses a PictureBox grid and embedded .RESX resources.
* **Quick Commands:** Rapid board manipulation via a built-in console.

## Build Requirements
* Visual Studio 2022+
* .NET 8.0 SDK

## Command Reference
*Commands accept the first letter only.*

* clear: Wipe the board.
* reset: Standard starting setup.
* delete [sq]: Remove piece (e.g., d a6).
* move [sq1] [sq2]: Move piece (e.g., m a1 a3).
* set [sq] [piece]: Place piece (e.g., s a4 wk).
* kastle [color] [type]: Castle (e.g., k w ooo).

## Credits
Chess piece images provided by Chess.com.














