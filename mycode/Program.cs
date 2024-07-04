// using System;

// namespace Application
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Console.WriteLine("Hello World");
//         }
//     }
// }

using FluentColorConsole;

Console.WriteLine("Hello World");

//somente um arquivo pode usar essa instrução de nivel superior
// void ShowMessage2()
// {
//     Console.WriteLine("Hello World2");
// }

// ShowMessage2();

// var ShowMessage = new ShowMessage();
// ShowMessage.WriteLine();

var textLine = ColorConsole.WithBlueText;
textLine.WriteLine("My text blue");