using System.Windows;
using System.Timers;
using System.Windows.Controls;

namespace Tetris
{
    /// <summary>
    /// Гавное окно.
    /// Отображение и контрллер.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Размер ячейки
        /// </summary>
        public const int BRICK_SIZE = 24;

        /// <summary>
        /// Игра
        /// </summary>
        private GameModel gameModel;
        private GameController gameController;

        /// <summary>
        /// Игровое поле
        /// </summary>
        public Image[,] gameField = new Image[GameModel.FIELD_SIZE_X, GameModel.FIELD_SIZE_Y];
        
        /// <summary>
        /// Следующая фигура
        /// </summary>
        public Image[,] nextFigure = new Image[GameModel.FIGURE_SIZE_X, GameModel.FIGURE_SIZE_Y];

        /// <summary>
        /// Таймер отрисовки
        /// </summary>
        private Timer renderTimer;


        public MainWindow()
        {
            InitializeComponent();

            gameModel = new GameModel();
            gameController = new GameController(gameModel);
            
            InitGameField();

            renderTimer = new Timer();
            renderTimer.Interval = 500;
            renderTimer.Elapsed += new ElapsedEventHandler(renderTimer_Elapsed);

            StartNewGame();
        }


        /// <summary>
        /// Начало новой игры
        /// </summary>
        private void StartNewGame()
        {            
            gameController.NewGame();
            Update();
            renderTimer.Enabled = true;            
        }


        /// <summary>
        /// Таймер движения фигур
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void renderTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new System.Threading.ThreadStart(delegate
                {
                    gameController.MoveFigureDown();
                    Update();
                    if (gameModel.isGameOver == true)
                        renderTimer.Enabled = false;
                }
            ));
        }


        /// <summary>
        /// Инициализация игровой модели
        /// </summary>
        public void InitGameField()
        {
            // Создаем массив игрового поля
            InitImageArray(gameField, GameModel.FIELD_SIZE_X, GameModel.FIELD_SIZE_Y, imageBrick);

            // Создаем массив след. фигуры
            InitImageArray(nextFigure, GameModel.FIGURE_SIZE_X, GameModel.FIGURE_SIZE_Y, imageBrick);

            // Заполняем контейнер игрового поля
            for (int x = 0; x < GameModel.FIELD_SIZE_X; x++) {
                for (int y = 0; y < GameModel.FIELD_SIZE_Y; y++) {
                    canvasField.Children.Add(gameField[x, y]);
                }
            }

            // Заполняем контейнер след. фигуры
            for (int x = 0; x < GameModel.FIGURE_SIZE_X; x++) {
                for (int y = 0; y < GameModel.FIGURE_SIZE_Y; y++) {
                    canvasNextFigure.Children.Add(nextFigure[x, y]);
                }
            }
        }


        /// <summary>
        /// Обновление отображения игровой модели
        /// </summary>
        void Update()
        {
            int x;
            int y;

            if (gameModel.isGameOver == false)
            {
                labelScore.Content = gameModel.Score.ToString();
                labelSpeed.Content = "x " + (gameModel.Speed + 1).ToString();

                // Игровое поле
                for (x = 0; x < GameModel.FIELD_SIZE_X; x++)
                {
                    for (y = 0; y < GameModel.FIELD_SIZE_Y; y++)
                    {
                        if (gameModel.gameField[x, y] == 0)
                            gameField[x, y].Visibility = Visibility.Hidden;
                        else
                            gameField[x, y].Visibility = Visibility.Visible;
                    }
                }

                // Тек. фигура
                for (x = 0; x < GameModel.FIGURE_SIZE_X; x++)
                {
                    for (y = 0; y < GameModel.FIGURE_SIZE_X; y++)
                    {
                        if (gameModel.currentFigure[x, y] == 1)
                            gameField[gameModel.currentFigure_x + x, gameModel.currentFigure_y + y].Visibility = Visibility.Visible;
                    }
                }

                // Следующая фигура
                for (x = 0; x < GameModel.FIGURE_SIZE_X; x++)
                {
                    for (y = 0; y < GameModel.FIGURE_SIZE_Y; y++)
                    {
                        if (gameModel.nextFigure[x, y] == 0)
                            nextFigure[x, y].Visibility = Visibility.Hidden;
                        else
                            nextFigure[x, y].Visibility = Visibility.Visible;
                    }
                }

                renderTimer.Interval = gameController.GetUpdateInterval();
            }
            else
            {
                labelSpeed.Content = "GAME OVER";
            }
        }


        /// <summary>
        /// Заполнение массива с ячейками
        /// </summary>
        /// <param name="imageArray">Массив</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="brickImage">Изображение ячейки</param>
        private void InitImageArray(Image[,] imageArray, int width, int height, Image brickImage)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Image brick = new Image();

                    brick.Source = brickImage.Source;
                    brick.Margin = new Thickness(x * BRICK_SIZE, y * BRICK_SIZE, 0, 0);

                    imageArray[x, y] = brick;
                }
            }
        }


        /// <summary>
        /// Новая игра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }


        /// <summary>
        /// Пауза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            PauseGame();
        }


        /// <summary>
        /// Перемещение фигуры с помощью клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.P)
            {
                PauseGame();
                return;
            }
            if (e.Key == System.Windows.Input.Key.F2)
            {
                StartNewGame();
                return;
            }

            if (renderTimer.Enabled == false)
                return;

            if (e.Key == System.Windows.Input.Key.Left)
                gameController.MoveFigureLeft();
            if (e.Key == System.Windows.Input.Key.Right)
                gameController.MoveFigureRight();
            if (e.Key == System.Windows.Input.Key.Space)
                gameController.RotateFigure();     
            if (e.Key == System.Windows.Input.Key.Down)
                gameController.MoveFigureDown();            

            Update();
        }


        /// <summary>
        /// Остановка/возобновление игры
        /// </summary>
        private void PauseGame()
        {
            if (gameModel.isGameOver == true)
                return;

            if (renderTimer.Enabled == true)
            {
                renderTimer.Enabled = false;
                labelSpeed.Content = "PAUSE";
            }
            else
            {
                renderTimer.Enabled = true;
                Update();
            }
        }
    }
}
