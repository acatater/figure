using UnityEngine;

public class FigureCheck : MonoBehaviour
{ 
    //Example input 2D array
    public bool[,] array = new bool[,]
        {

        { false, false, false, false},
        { false, false, true, true},
        { false, true, true, true},
        { false, true, false, true}

        };

    private void Start()
    {
        CheckFigure(array);
    }
   
    public Figure CheckFigure(bool[,] grid, int minSizeForFigure = 2)
    {
        var gridLenght = grid.GetLength(0);
        var gridHeight = grid.GetLength(1);

        Figure figure = null;

        int maxSize = 0;

        //loop every X cell
        for (int x = 0; x < gridLenght; x++)
        {
            //loop every Y cell
            for (int y = 0; y < gridHeight; y++)
            {
                //check if current cell is true
                if (grid[y, x])
                {
                    //Check it for Square
                    var squareSize = CheckForSquare(grid, new Vector2Int(x, y));

                    if (squareSize >= minSizeForFigure && maxSize < squareSize)
                    {
                        maxSize = squareSize;
                        figure = new Figure { size = maxSize, type = FigureType.square };
                    }
                }
            }

        }

        Debug.Log("Max figure size: " + maxSize);

        return figure;
    }

    public int CheckForSquare(bool[,] grid, Vector2Int currentCellPosition)
    {
        var gridLenght = grid.GetLength(0);
        var gridHeight = grid.GetLength(1);

        int maxSize = 0;

        int currentHeight = 0;

        bool emergencyBreak = false;

        //Start to check down cells
        for (int i = currentCellPosition.y; i < gridHeight; i++)
        {
            int currentWidth = 0;

            //Check if current cell is true
            if (grid[i, currentCellPosition.x])
            {
                currentHeight++;
            }
            else
                break;

            //Check right cells for every Y
            for (int j = currentCellPosition.x; j < gridLenght; j++)
            {
                //Check if current cell is true
                if (grid[i, j])
                {
                    currentWidth++;
                }
                else
                {
                    emergencyBreak = true;
                    break;
                }

                Debug.Log("Current width: " + currentWidth + " current height: " + currentHeight + " cell position: " + i + " : " + j);
            }

            if (emergencyBreak)
                break;

            //Check if WIDTH and HEIGHT are the same (cause it's square) and is larger then squares before
            if (currentWidth > maxSize && currentHeight == currentWidth)
                maxSize = currentWidth;
        }

        return maxSize;
    }

    public class Figure
    {
        public FigureType type;
        public int size;
    }

    public enum FigureType
    {
        square
    }
}
