// See https://aka.ms/new-console-template for more information
Minesweeper ms = new Minesweeper(10, 10, 0.15);

ms.Reveal(0, 0);

bool quit = false;
bool showHelp = false;

int x = -1;
int y = -1;

while (!quit)
{
    if (!showHelp){
        Console.Clear();
        Console.WriteLine(ms);
    }

    showHelp = false;

    Console.WriteLine("Entrer la commande (h pour aide) : ");
    var command = Console.ReadLine();

    var commandParts = command.Split(' ');

    switch (commandParts[0]) {
        case "r":
            x = int.Parse(commandParts[1]);
            y = int.Parse(commandParts[2]);
            commandReveal(x, y);
            break;
        case "m":
            x = int.Parse(commandParts[1]);
            y = int.Parse(commandParts[2]);
            commandMark(x, y);
            break;
        case "h":
            showHelp = true;
            commandHelp();            
            break;
        case "q":
            quit = true;
            break;
    }

    if (ms.IsGameOver)
    {
        Console.WriteLine("Game Over");
        quit = true;
    }
    else if (ms.IsGameWon)
    {
        Console.WriteLine("Tu as gagné!");
        quit = true;
    }
}


void commandReveal(int x, int y)
{
    ms.Reveal(x, y);
}

void commandMark(int x, int y)
{
    ms.Mark(x, y);
}

void commandHelp() {
    Console.WriteLine("r x y - Révéler la cellule à x y");
    Console.WriteLine("m x y - Marquer la cellule à x y");
    Console.WriteLine("h - Afficher l'aide");
    Console.WriteLine("q - Quitter");
}


