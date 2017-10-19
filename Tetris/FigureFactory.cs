
namespace Tetris
{
    /// <summary>
    /// Фабрика для создания фигурок по шаблону [4,4]
    /// </summary>
    class FigureFactory
    {
        /// <summary>
        /// Количество шаблонов фигур
        /// </summary>
        public const int MAX_FIGURES = 6;


        /// <summary>
        /// Создание новой фигуры
        /// </summary>
        /// <param name="figureId">ID шаблона</param>
        /// <returns></returns>
        public static int[,] GetFigure(int figureId, int nRotate)
        {
            switch (figureId)
            {
                case 0:
                    return GetFigure_1(nRotate);
                case 1:
                    return GetFigure_2(nRotate);
                case 2:
                    return GetFigure_3(nRotate);
                case 3:
                    return GetFigure_4(nRotate);
                case 4:
                    return GetFigure_5(nRotate);
                case 5:
                    return GetFigure_6(nRotate);
            }

            return GetFigure_1(0);
        }


        /// <summary>
        /// *
        /// *
        /// *
        /// *
        /// </summary>
        /// <param name="nRotate"></param>
        /// <returns></returns>
        private static int[,] GetFigure_1(int nRotate)
        {
            int[,] figure1 = {
                                 {1, 0, 0, 0},
                                 {1, 0, 0, 0},
                                 {1, 0, 0, 0},
                                 {1, 0, 0, 0}
                             };
            int[,] figure2 = {
                                 {1, 1, 1, 1},
                                 {0, 0, 0, 0},
                                 {0, 0, 0, 0},
                                 {0, 0, 0, 0}
                             };

            switch (nRotate)
            {
                case 0: 
                case 2:
                    return figure1;
                case 1:
                case 3:
                    return figure2;
            }
            return figure1;
        }


        /// <summary>
        /// *
        /// *
        /// * *
        /// </summary>
        /// <param name="nRotate"></param>
        /// <returns></returns>
        private static int[,] GetFigure_2(int nRotate)
        {
            int[,] figure1 = {
                                 {1, 0, 0, 0},
                                 {1, 0, 0, 0},
                                 {1, 1, 0, 0},
                                 {0, 0, 0, 0}
                             };
            int[,] figure2 = {
                                 {1, 1, 1, 0},
                                 {1, 0, 0, 0},
                                 {0, 0, 0, 0},
                                 {0, 0, 0, 0}
                             };
            int[,] figure3 = {
                                 {1, 1, 0, 0},
                                 {0, 1, 0, 0},
                                 {0, 1, 0, 0},
                                 {0, 0, 0, 0}
                             };
            int[,] figure4 = {
                                 {0, 0, 1, 0},
                                 {1, 1, 1, 0},
                                 {0, 0, 0, 0},
                                 {0, 0, 0, 0}
                             };

            switch (nRotate)
            {
                case 0:
                    return figure1;
                case 1:
                    return figure2;
                case 2:
                    return figure3;
                case 3:
                    return figure4;
            }
            return figure1;
        }


        /// <summary>
        ///   *
        ///   *
        /// * *
        /// </summary>
        /// <param name="nRotate"></param>
        /// <returns></returns>
        private static int[,] GetFigure_3(int nRotate)
        {
            int[,] figure1 = {
                                 {0, 1, 0, 0},
                                 {0, 1, 0, 0},
                                 {1, 1, 0, 0},
                                 {0, 0, 0, 0}
                             };
            int[,] figure2 = {
                                 {1, 0, 0, 0},
                                 {1, 1, 1, 0},
                                 {0, 0, 0, 0},
                                 {0, 0, 0, 0}
                             };
            int[,] figure3 = {
                                 {1, 1, 0, 0},
                                 {1, 0, 0, 0},
                                 {1, 0, 0, 0},
                                 {0, 0, 0, 0}
                             };
            int[,] figure4 = {
                                 {1, 1, 1, 0},
                                 {0, 0, 1, 0},
                                 {0, 0, 0, 0},
                                 {0, 0, 0, 0}
                             };

            switch (nRotate)
            {
                case 0:
                    return figure1;
                case 1:
                    return figure2;
                case 2:
                    return figure3;
                case 3:
                    return figure4;
            }
            return figure1;
        }


        /// <summary>
        /// *
        /// * *
        ///   *
        /// </summary>
        /// <param name="nRotate"></param>
        /// <returns></returns>
        private static int[,] GetFigure_4(int nRotate)
        {
            int[,] figure1 = {
                                 {0, 1, 1, 0},
                                 {1, 1, 0, 0},
                                 {0, 0, 0, 0},
                                 {0, 0, 0, 0}
                             };
            int[,] figure2 = {
                                 {1, 0, 0, 0},
                                 {1, 1, 0, 0},
                                 {0, 1, 0, 0},
                                 {0, 0, 0, 0}
                             };

            switch (nRotate)
            {
                case 0:
                case 2:
                    return figure1;
                case 1:
                case 3:
                    return figure2;                
            }
            return figure1;
        }


        /// <summary>
        ///   *
        /// * *
        /// *
        /// </summary>
        /// <param name="nRotate"></param>
        /// <returns></returns>
        private static int[,] GetFigure_5(int nRotate)
        {
            int[,] figure1 = {
                                 {1, 1, 0, 0},
                                 {0, 1, 1, 0},
                                 {0, 0, 0, 0},
                                 {0, 0, 0, 0}
                             };
            int[,] figure2 = {
                                 {0, 1, 0, 0},
                                 {1, 1, 0, 0},
                                 {1, 0, 0, 0},
                                 {0, 0, 0, 0}
                             };

            switch (nRotate)
            {
                case 0:
                case 2:
                    return figure1;
                case 1:
                case 3:
                    return figure2;                
            }
            return figure1;
        }


        /// <summary>
        /// * *
        /// * *
        /// </summary>
        /// <param name="nRotate"></param>
        /// <returns></returns>
        private static int[,] GetFigure_6(int nRotate)
        {
            int[,] figure1 = {
                                 {1, 1, 0, 0},
                                 {1, 1, 0, 0},
                                 {0, 0, 0, 0},
                                 {0, 0, 0, 0}
                             };

            return figure1;
        }
    }
}
