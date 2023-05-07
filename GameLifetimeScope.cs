using DG.Tweening;
using EventHandlers;
using Models;
using Presenters;
using Processes;
using VContainer;
using VContainer.Unity;
using Views;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<CurrentTileModel>(Lifetime.Singleton);
        builder.Register<MainBoardModel>(Lifetime.Singleton);
        builder.Register<NextTileModel>(Lifetime.Singleton);
        builder.Register<ScoreModel>(Lifetime.Singleton);
        builder.Register<SelectedBoardCellModel>(Lifetime.Singleton);
        builder.Register<TileDeckModel>(Lifetime.Singleton);
        builder.Register<TileRestAmountModel>(Lifetime.Singleton);
        builder.Register<TilesModel>(Lifetime.Singleton);
        builder.Register<ValidCellPositionsModel>(Lifetime.Singleton);
        
        builder.RegisterComponentInHierarchy<CurrentTilePositionDetector>();
        builder.RegisterComponentInHierarchy<PanelCellDetector>();
        builder.RegisterComponentInHierarchy<BoardCellPointer>();
        builder.RegisterComponentInHierarchy<ScoreBoardFactory>();
        builder.RegisterComponentInHierarchy<TileFactory>();
        builder.RegisterComponentInHierarchy<TileRestAmountBoardFactory>();

        builder.Register<ConductMainBoardProcess>(Lifetime.Singleton);
        builder.Register<IlluminateBoardProcess>(Lifetime.Singleton);
        builder.Register<MainPanelBoardProcess>(Lifetime.Singleton);
        builder.Register<PrepareNextTileProcess>(Lifetime.Singleton);
        builder.Register<PutTileOnThePanelProcess>(Lifetime.Singleton);
        builder.Register<SelectBoardCellProcess>(Lifetime.Singleton);
        builder.Register<StartMainGameProcess>(Lifetime.Singleton);
        builder.Register<TakeTileProcess>(Lifetime.Singleton);
        builder.Register<UpdateValidCellPositionsProcess>(Lifetime.Singleton);

        builder.RegisterEntryPoint<CurrentTilePresenter>();
        builder.RegisterEntryPoint<NextTilePresenter>();
        builder.RegisterEntryPoint<ScoreBoardPresenter>();
        builder.RegisterEntryPoint<TilesPresenter>();
        builder.RegisterEntryPoint<TileRestAmountBoardPresenter>();
        
        builder.RegisterEntryPoint<OnCurrentTilePositionDetected>();
        builder.RegisterEntryPoint<OnGameStart>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForPutCurrentTile>();
        builder.RegisterEntryPoint<OnMouseButtonRightPressedForRotateCurrentTile>();
        builder.RegisterEntryPoint<OnPanelCellDetected>();
    }
}
