using Models;
using VContainer;
using Views;

namespace Processes
{
    public class AddScoreProcess
    {
        private readonly ScoreModel _scoreModel;
        private readonly DeskFactory _deskFactory;

        [Inject]
        public AddScoreProcess(ScoreModel scoreModel, DeskFactory deskFactory)
        {
            _scoreModel = scoreModel;
            _deskFactory = deskFactory;
        }

        public void AddScore(int score)
        {
            _scoreModel.AddScore(score);
            _deskFactory.Desk.DisplayScoreTween();
        }
    }
}