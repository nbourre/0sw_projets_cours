class Minesweeper
{
    List<List<Cell>> board;
    Random random = new Random();

    public int Width { get; set; } = 10;
    public int Height { get; set; } = 10;
    public double Density { get; set; } = 0.15;
    public class Cell
    {
        public bool IsMine { get; set; } = false;
        public bool IsRevealed { get; set; } = false;
        public int AdjacentMines { get; set; }

        public override string ToString()
        {
            return ToString(false);
        }

        public string ToString(bool show = false) {
            if (IsRevealed || show)
            {
                if (IsMine)
                {
                    return "X";
                }
                else
                {
                    return AdjacentMines.ToString();
                }
            }
            else
            {
                return " ";
            }       
        }
    }

    public Minesweeper(int sizeX, int sizeY, double density = 0.15)
    {
        Width = sizeX;
        Height = sizeY;
        Density = density;
        GenerateBoard();
    }

    /// <summary>
    /// Function that generate the minesweeper board
    /// </summary>
    public void GenerateBoard()
    {
        int nbCells = Width * Height;

        if (board == null)
        {
            board = new List<List<Cell>>();
            for (int j = 0; j < Height; j++)
            {
                board.Add(new List<Cell>());
                for (int i = 0; i < Width; i++)
                {
                    var cell = new Cell();

                    // Randomly generate mines
                    if (random.Next(0, 100) < (Density * 100))
                    {
                        cell.IsMine = true;
                    }
                    board[j].Add(cell);
                }
            }
        }

        // Calculate the number of adjacent mines
        GenerateNumbers();
    }

    /// <summary>
    /// Generate numbers for each cell
    /// </summary>
    private void GenerateNumbers() 
    {
        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                if (!board[j][i].IsMine)
                {
                    board[j][i].AdjacentMines = GetAdjacentMines(i, j);
                    //Console.WriteLine($"Cell {i},{j} has {board[j][i].AdjacentMines} adjacent mines");
                }
            }
        }
    }

    /// <summary>
    /// Get the number of adjacent mines for a given cell
    /// </summary>
    /// <param name="x">X position of the cell</param>
    /// <param name="y">Y position of the cell</param>
    /// <returns>Number of adjacent mines</returns>
    /// <remarks>Return -1 if the cell is a mine</remarks>
    /// <exception cref="ArgumentOutOfRangeException">If the cell is out of the board</exception>
    /// <exception cref="InvalidOperationException">If the board is not generated</exception>
    private int GetAdjacentMines(int x, int y)
    {
        if (board == null)
        {
            throw new InvalidOperationException("The board is not generated");
        }

        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            throw new ArgumentOutOfRangeException("The cell is out of the board");
        }

        int count = 0;

        for (int j = y - 1; j <= y + 1; j++)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i >= 0 && i < Width && j >= 0 && j < Height)
                {
                    if (board[j][i].IsMine)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public override string ToString() {
        return ToString(false);
    }

    public string ToString(bool showAll = false)
    {
        string result = "";
        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                result += board[j][i].ToString(showAll);
            }
            result += "\n";
        }
        return result;
    }
    
}