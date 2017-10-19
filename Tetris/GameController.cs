
using System;
namespace Tetris
{
    /// <summary>
    /// Игровой контроллер.
    /// Основная логика игры.
    /// </summary>
    class GameController
    {
        /// <summary>
        /// Модель игры
        /// </summary>
        private GameModel gameModel;

        /// <summary>
        /// Генератор для выброса случайных новый фигур
        /// </summary>
        private Random rnd = new Random();



        /// <summary>
        /// Создание игрового контроллера
        /// </summary>
        /// <param name="gameModel"></param>
        public GameController(GameModel gameModel)
        {
            this.gameModel = gameModel;
        }


        /// <summary>
        /// Создание новой игры
        /// </summary>
        public void NewGame()
        {
            gameModel.ResetModel();
            GetNextFigure();
            ThrowNextFigure();
        }


        /// <summary>
        /// Создание случайной след. фигуры
        /// </summary>
        public void GetNextFigure()
        {
            // Очищаем модель фигуры
            gameModel.ResetArray(gameModel.nextFigure, GameModel.FIGURE_SIZE_X, GameModel.FIGURE_SIZE_Y);

            // Используем фабрику фигур, чтобы создать следующую фигуру из шаблона
            int newId = rnd.Next(FigureFactory.MAX_FIGURES);

            int[,] newFigure = FigureFactory.GetFigure(newId, 0);
            gameModel.CopyArray(newFigure, GameModel.FIGURE_SIZE_X, GameModel.FIGURE_SIZE_Y, gameModel.nextFigure);

            gameModel.nextFigure_id = newId;
            gameModel.nextFigure_rotate = 0;
        }


        /// <summary>
        /// Выброс след. фигуры в игровое поле
        /// </summary>
        public void ThrowNextFigure()
        {
            gameModel.CopyArray(gameModel.nextFigure, 
                                GameModel.FIGURE_SIZE_X,
                                GameModel.FIGURE_SIZE_Y, 
                                gameModel.currentFigure);
            
            gameModel.currentFigure_id = gameModel.nextFigure_id;
            gameModel.currentFigure_rotate = gameModel.nextFigure_rotate;

            // Размещаем по центру сверху
            gameModel.currentFigure_x = (GameModel.FIELD_SIZE_X - GameModel.FIGURE_SIZE_X) / 2;
            gameModel.currentFigure_y = 0;
            
            // Если фигура была только выброшена, и уже имеет столкновение
            // то завершаем игру
            if( IsCollided() == true )
                GameOver();
            
            GetNextFigure();
        }
        
        
        /// <summary>
        /// Проверяет, имеет ли фигура столкновение в тек. позиции
        /// </summary>
        /// <returns>true если да</returns>
        private bool IsCollided()
        {
            // Проверяем тек. позицию на столкновения
            for (int x = 0; x < GameModel.FIGURE_SIZE_X; x++)
            {
                for (int y = 0; y < GameModel.FIGURE_SIZE_X; y++)
                {
                    int nFieldCell = gameModel.gameField[gameModel.currentFigure_x + x, gameModel.currentFigure_y + y];

                    if ((nFieldCell == 1) && (gameModel.currentFigure[x, y] == 1))
                    {
                        return true;
                    }                        
                }
            }            
            return false;
        }


        /// <summary>
        /// Передвижение тек. фигуры вниз
        /// </summary>
        public void MoveFigureDown()
        {
            int x;
            int y;

            // Защита на всякий случай
            if (gameModel.isGameOver == true)
                return;

            // Проверяем текущую позицию на предмет столкновения
            gameModel.currentFigure_y++;

            if ( IsCollided() )
            {
                gameModel.currentFigure_y--;                
                // Помещаем фигуру на игровое поле
                for (x = 0; x < GameModel.FIGURE_SIZE_X; x++)
                {
                    for (y = 0; y < GameModel.FIGURE_SIZE_X; y++)
                    {
                        if (gameModel.currentFigure[x, y] == 1)
                            gameModel.gameField[gameModel.currentFigure_x + x, gameModel.currentFigure_y + y] = 1;
                    }
                }

                // Удаляем заполненные ряды
                CutLines();

                // И выбрасываем новую
                ThrowNextFigure();
            }
        }


        /// <summary>
        /// Перемещение фигуры влево
        /// </summary>
        public void MoveFigureLeft()
        {
            // Если достигли левого края, то сразу выходим
            if (gameModel.currentFigure_x == 0)
                return;

            int newPos = gameModel.currentFigure_x - 1;

            // Проверям что нет столкновения
            for (int x = 0; x < GameModel.FIGURE_SIZE_X; x++)
            {
                for (int y = 0; y < GameModel.FIGURE_SIZE_X; y++)
                {
                    if ((gameModel.currentFigure[x, y] == 1) && (gameModel.gameField[newPos + x, gameModel.currentFigure_y + y] == 1))
                        return;
                }
            }

            gameModel.currentFigure_x = newPos;
        }


        /// <summary>
        /// Перемещение фигуры вправо
        /// </summary>
        public void MoveFigureRight()
        {
            // Если достигли правого края, то сразу выходим
            if (gameModel.currentFigure_x == (GameModel.FIELD_SIZE_X-1))
                return;

            int newPos = gameModel.currentFigure_x + 1;

            // Проверям, ты выйдет ли фигура за край
            // а также на предмет столкновений
            for (int x = 0; x < GameModel.FIGURE_SIZE_X; x++)
            {
                for (int y = 0; y < GameModel.FIGURE_SIZE_X; y++)
                {
                    if ((gameModel.currentFigure[x, y] == 1) && ((newPos + x) >= GameModel.FIELD_SIZE_X))
                        return;
                    if ((gameModel.currentFigure[x, y] == 1) && (gameModel.gameField[newPos + x, gameModel.currentFigure_y + y] == 1))
                        return;
                }
            }

            gameModel.currentFigure_x = newPos;
        }


        /// <summary>
        /// Поворот фигуры
        /// </summary>
        public void RotateFigure()
        {
            int newRotate = gameModel.currentFigure_rotate - 1;

            if (newRotate < 0)
                newRotate = 3;

            int[,] newFigure = FigureFactory.GetFigure(gameModel.currentFigure_id, newRotate);

            // Делаем проверку на столкновения
            for (int x = 0; x < GameModel.FIGURE_SIZE_X; x++)
            {
                for (int y = 0; y < GameModel.FIGURE_SIZE_X; y++)
                {
                    if ((newFigure[x, y] == 1) && ((gameModel.currentFigure_x + x) >= GameModel.FIELD_SIZE_X))
                        return;
                    if ((newFigure[x, y] == 1) && (gameModel.gameField[gameModel.currentFigure_x + x, gameModel.currentFigure_y + y] == 1))
                        return;
                }
            }

            gameModel.CopyArray(newFigure, GameModel.FIGURE_SIZE_X, GameModel.FIGURE_SIZE_Y, gameModel.currentFigure);
            gameModel.currentFigure_rotate = newRotate;
        }


        /// <summary>
        /// Завершение игры
        /// </summary>
        public void GameOver()
        {
            gameModel.isGameOver = true;
        }


        /// <summary>
        /// Возвращает интерал для таймера в зависимости от
        /// текущей скрости падения фигуры
        /// </summary>
        /// <returns></returns>
        public int GetUpdateInterval()
        {
            switch (gameModel.Speed)
            {
                case 1:
                    return 500;
                case 2:
                    return 400;
                case 3:
                    return 300;
                case 4:
                    return 200;
                case 5:
                    return 100;
            }
            return 500;
        }


        /// <summary>
        /// Вырезаем полностью заполненные ряды
        /// </summary>
        private void CutLines()
        {
            int y = GameModel.FIELD_SIZE_Y - 1;
            int nCutted = 0;

            while (y != 0)
            {
                int x = 0;
                bool isFullRow = true;

                // Проверям, заполнен ли тек. ряд
                for (x = 0; x < GameModel.FIELD_SIZE_X; x++)
                {
                    if (gameModel.gameField[x, y] == 0)
                        isFullRow = false;         
                }

                // Если заполнен, то удаляем его
                if (isFullRow == true)
                {
                    CurLine(y);
                    nCutted++;
                }
                else
                {
                    y--;
                }
            }

            gameModel.Score += nCutted;

            // Каждые 25 рядом прибавляем скорость
            gameModel.Speed = (gameModel.Score / 25);
            if (gameModel.Speed > 5)
                gameModel.Speed = 5;
        }


        /// <summary>
        /// Вырезает определенную строку
        /// </summary>
        /// <param name="iLine"></param>
        private void CurLine(int iLine)
        {
            for (int y = iLine; y > 0; y--)
            {
                for (int x = 0; x < GameModel.FIELD_SIZE_X; x++)
                {
                    gameModel.gameField[x, y] = gameModel.gameField[x, y - 1];
                }
            }
        }
    }
}
