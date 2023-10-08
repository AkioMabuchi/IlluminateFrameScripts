using Models;
using VContainer;
using Views;

namespace Processes
{
    public class AddScoreProcess
    {
        private readonly ScoreModel _scoreModel;
        private readonly Desk _desk;

        [Inject]
        public AddScoreProcess(ScoreModel scoreModel, Desk desk)
        {
            _scoreModel = scoreModel;
            _desk = desk;
        }

        public void AddScore(int score)
        {
            _scoreModel.AddScore(score);
            _desk.ValueDisplayScore.Draw();
        }
    }
}