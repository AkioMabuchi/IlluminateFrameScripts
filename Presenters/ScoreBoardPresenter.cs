using Models;
using UniRx;
using VContainer;
using VContainer.Unity;
using Views;

namespace Presenters
{
    public class ScoreBoardPresenter: IInitializable
    {
        private readonly ScoreModel _scoreModel;
        private readonly ScoreBoardFactory _scoreBoardFactory;

        [Inject]
        public ScoreBoardPresenter(ScoreModel scoreModel ,ScoreBoardFactory scoreBoardFactory)
        {
            _scoreModel = scoreModel;
            _scoreBoardFactory = scoreBoardFactory;
        }
        
        public void Initialize()
        {
            _scoreModel.OnChangedScore.Subscribe(score =>
            {
                if (_scoreBoardFactory.ScoreBoard)
                {
                    _scoreBoardFactory.ScoreBoard.SetDisplayNumber(score);
                }
            });
        }
    }
}