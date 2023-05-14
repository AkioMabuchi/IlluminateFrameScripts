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
        builder.Register<GameStateModel>(Lifetime.Singleton);
        builder.Register<MainBoardModel>(Lifetime.Singleton);
        builder.Register<MainFrameModel>(Lifetime.Singleton);
        builder.Register<NextTileModel>(Lifetime.Singleton);
        builder.Register<ScoreModel>(Lifetime.Singleton);
        builder.Register<SelectedBoardCellModel>(Lifetime.Singleton);
        builder.Register<SelectedHeaderButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedSelectFrameSizeScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedTitleScreenButtonModel>(Lifetime.Singleton);
        builder.Register<TileDeckModel>(Lifetime.Singleton);
        builder.Register<TileRestAmountModel>(Lifetime.Singleton);
        builder.Register<TilesModel>(Lifetime.Singleton);
        builder.Register<ValidCellPositionsModel>(Lifetime.Singleton);

        builder.RegisterComponentInHierarchy<BannerShorted>();
        builder.RegisterComponentInHierarchy<BannerClosed>();
        builder.RegisterComponentInHierarchy<BoardCellDetector>();
        builder.RegisterComponentInHierarchy<BoardCellPointer>();
        builder.RegisterComponentInHierarchy<CurrentTilePositionDetector>();
        builder.RegisterComponentInHierarchy<Footer>();
        builder.RegisterComponentInHierarchy<Header>();
        builder.RegisterComponentInHierarchy<MainCamera>();
        builder.RegisterComponentInHierarchy<DeskFactory>();
        builder.RegisterComponentInHierarchy<SelectFrameSizeScreen>();
        builder.RegisterComponentInHierarchy<TextEffectFactory>();
        builder.RegisterComponentInHierarchy<TileFactory>();
        builder.RegisterComponentInHierarchy<TitleScreen>();

        builder.Register<AddScoreProcess>(Lifetime.Singleton);
        builder.Register<ClearMainGameProcess>(Lifetime.Singleton);
        builder.Register<ConductMainBoardProcess>(Lifetime.Singleton);
        builder.Register<DecreaseTileRestAmountProcess>(Lifetime.Singleton);
        builder.Register<GameStartProcess>(Lifetime.Singleton);
        builder.Register<IlluminateBoardProcess>(Lifetime.Singleton);
        builder.Register<InitializeMainGameProcess>(Lifetime.Singleton);
        builder.Register<MainBoardProcess>(Lifetime.Singleton);
        builder.Register<PrepareNextTileProcess>(Lifetime.Singleton);
        builder.Register<PutTileOnFrameProcess>(Lifetime.Singleton);
        builder.Register<QuitGameProcess>(Lifetime.Singleton);
        builder.Register<ReturnToTitleProcess>(Lifetime.Singleton);
        builder.Register<SelectBoardCellProcess>(Lifetime.Singleton);
        builder.Register<SelectHeaderButtonProcess>(Lifetime.Singleton);
        builder.Register<SelectSelectFrameSizeScreenProcess>(Lifetime.Singleton);
        builder.Register<SelectTitleScreenButtonProcess>(Lifetime.Singleton);
        builder.Register<ShowUpTitleScreenProcess>(Lifetime.Singleton);
        builder.Register<StartMainGameProcess>(Lifetime.Singleton);
        builder.Register<TakeTileProcess>(Lifetime.Singleton);
        builder.Register<UpdateValidCellPositionsProcess>(Lifetime.Singleton);

        builder.RegisterEntryPoint<CurrentTilePresenter>();
        builder.RegisterEntryPoint<DeskPresenter>();
        builder.RegisterEntryPoint<NextTilePresenter>();
        builder.RegisterEntryPoint<TilesPresenter>();

        builder.RegisterEntryPoint<OnBoardCellDetected>();
        builder.RegisterEntryPoint<OnCurrentTilePositionDetected>();
        builder.RegisterEntryPoint<OnEscapeKeyPressedForReturnToTitle>();
        builder.RegisterEntryPoint<OnGameStart>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForPutCurrentTile>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForSelectHeader>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForSelectTitleScreen>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForSelectSelectFrameSizeScreen>();
        builder.RegisterEntryPoint<OnMouseButtonRightPressedForRotateCurrentTile>();
        builder.RegisterEntryPoint<OnPointerEventsFiredHeader>();
        builder.RegisterEntryPoint<OnPointerEventsFiredSelectFrameSizeScreen>();
        builder.RegisterEntryPoint<OnPointerEventsFiredTitleScreen>();
    }
}
