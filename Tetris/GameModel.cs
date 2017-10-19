using System.Windows;
using System.Windows.Controls;

namespace Tetris
{
    /// <summary>
    /// Модель игры
    /// </summary>
    class GameModel
    {
        /// <summary>
        /// Игровое поле размером 10x20 ячеек
        /// </summary>
        public const int FIELD_SIZE_X = 10;
        public const int FIELD_SIZE_Y = 20;

        /// <summary>
        /// Размеры фигуры
        /// </summary>
        public const int FIGURE_SIZE_X = 4;
        public const int FIGURE_SIZE_Y = 4;

        /// <summary>
        /// Счет игры
        /// </summary>
        public int Score;

        /// <summary>
        /// Скорость игры
        /// </summary>
        public int Speed;

        /// <summary>
        /// Игровое поле.
        /// Размер спец. больше, чтобы было легче перемещать фигуру
        /// </summary>
        public int[,] gameField = new int[FIELD_SIZE_X + FIGURE_SIZE_X, FIELD_SIZE_Y + FIGURE_SIZE_Y];

        /// <summary>
        /// Модель текущей фигуры
        /// </summary>
        public int[,] currentFigure = new int[FIGURE_SIZE_X, FIGURE_SIZE_Y];
        public int currentFigure_id;
        public int currentFigure_rotate;
        public int currentFigure_x;
        public int currentFigure_y;

        /// <summary>
        /// Модель следующей фигуры
        /// </summary>
        public int[,] nextFigure = new int[FIGURE_SIZE_X, FIGURE_SIZE_Y];
        public int nextFigure_id;
        public int nextFigure_rotate;

        /// <summary>
        /// Признак завершения игры
        /// </summary>
        public bool isGameOver = false;



        /// <summary>
        /// Создание новой игры
        /// </summary>
        public void ResetModel()
        {
            // Обнулем игровое поле
            ResetArray(gameField, FIELD_SIZE_X + FIGURE_SIZE_X, FIELD_SIZE_Y + FIGURE_SIZE_Y);

            // Заполняем строчку поля, ниже его размера, чтобы было легче определить,
            // когда фигура упала до дна
            for (int x = 0; x < FIELD_SIZE_X; x++)
            {
                gameField[x, FIELD_SIZE_Y] = 1;
            }

            // Обнуляем фигуры
            ResetArray(currentFigure, FIGURE_SIZE_X, FIGURE_SIZE_Y);
            ResetArray(nextFigure, FIGURE_SIZE_X, FIGURE_SIZE_Y);

            currentFigure_x = 0;
            currentFigure_y = 0;
            currentFigure_id = 0;
            currentFigure_rotate = 0;

            nextFigure_id = 0;
            nextFigure_rotate = 0;

            // Обнуляем игровую статистику
            Score = 0;
            Speed = 0;
            isGameOver = false;
        }


        /// <summary>
        /// Обнуление массива
        /// </summary>
        /// <param name="array">Массив фигуры</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        public void ResetArray(int[,] array, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    array[x, y] = 0;
                }
            }
        }


        /// <summary>
        /// Копирование массива
        /// </summary>
        /// <param name="array">Массив фигуры</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        public void CopyArray(int[,] srcArray, int width, int height, int[,] dstArray)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    dstArray[x, y] = srcArray[x, y];
                }
            }
        }
    }
}
