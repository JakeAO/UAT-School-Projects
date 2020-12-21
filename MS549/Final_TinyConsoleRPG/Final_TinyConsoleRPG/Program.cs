using System.Threading;

namespace Final_TinyConsoleRPG
{
    internal class Program
    {
        private static int? _gameplayResult = null;

        public static int Main(string[] args)
        {
            Gameplay gameplay = new Gameplay();

            gameplay.Run(result => _gameplayResult = result);

            while (_gameplayResult == null)
                Thread.Sleep(100);

            return _gameplayResult.Value;
        }
    }
}