using Interfaces.Processes;
using Models;
using Models.Instances.Frames;
using Models.ScreenButtons;
using Models.ScreenButtons.Prior;
using Presenters;
using Presenters.Controllers.Tiles.Dynamics;
using Presenters.Controllers.Tiles.Powers;
using Presenters.Controllers.Tiles.Terminals;
using Presenters.Screens;
using Presenters.Screens.Prior;
using Processes;
using Processes.Events;
using Processes.Events.Screens;
using Processes.Events.Screens.Prior;
using Processes.QuitGame;
using Processes.Starts;
using Processes.Updates;
using VContainer;
using VContainer.Unity;
using Views;
using Views.Banners;
using Views.Controllers;
using Views.Screens;
using Views.Screens.Prior;
using Views.TextEffectFactories;
using Views.TileFactories.Dynamics;
using Views.TileFactories.Powers;
using Views.TileFactories.Terminals;

public class SteamLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // ------------------------------ Models ------------------------------

        builder.Register<AchievementsModel>(Lifetime.Singleton);
        builder.Register<CurrentResolutionCodeModel>(Lifetime.Singleton);
        builder.Register<CurrentTileModel>(Lifetime.Singleton);
        builder.Register<FrameSmallModel>(Lifetime.Singleton);
        builder.Register<FrameMediumModel>(Lifetime.Singleton);
        builder.Register<FrameLargeModel>(Lifetime.Singleton);
        builder.Register<GameStateModel>(Lifetime.Singleton);
        builder.Register<LineCountsModel>(Lifetime.Singleton);
        builder.Register<MainBoardModel>(Lifetime.Singleton);
        builder.Register<MainFrameModel>(Lifetime.Singleton);
        builder.Register<NextTileModel>(Lifetime.Singleton);
        builder.Register<ScoreModel>(Lifetime.Singleton);
        builder.Register<SelectedBoardCellModel>(Lifetime.Singleton);
        builder.Register<SelectedHeaderButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedRecordsScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedSettingsScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedResultScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedSelectFrameSizeScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedTitleScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedInstructionScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SteamModel>(Lifetime.Singleton);
        builder.Register<TileDeckModel>(Lifetime.Singleton);
        builder.Register<TileIdModel>(Lifetime.Singleton);
        builder.Register<TileRestAmountModel>(Lifetime.Singleton);
        builder.Register<TutorialStateModel>(Lifetime.Singleton);
        builder.Register<ValidCellPositionsModel>(Lifetime.Singleton);

        // ------------------------------ View Controllers ------------------------------

        builder.Register<BoardTilePositioner>(Lifetime.Singleton);
        builder.Register<CurrentTilePositioner>(Lifetime.Singleton);
        builder.Register<NextTilePositioner>(Lifetime.Singleton);

        builder.Register<BulbTiles>(Lifetime.Singleton);
        builder.Register<TerminalTiles>(Lifetime.Singleton);
        
        builder.Register<TileMover>(Lifetime.Singleton);
        builder.Register<TileRenderer>(Lifetime.Singleton);
        builder.Register<TileRotator>(Lifetime.Singleton);
        builder.Register<TileThrower>(Lifetime.Singleton);

        // ------------------------------ Views ------------------------------

        builder.RegisterComponentInHierarchy<ClosedBanner>();
        builder.RegisterComponentInHierarchy<ExterminatedBanner>();
        builder.RegisterComponentInHierarchy<FinishedBanner>();
        builder.RegisterComponentInHierarchy<ShortedBanner>();
        builder.RegisterComponentInHierarchy<TutorialBanner>();

        builder.RegisterComponentInHierarchy<ConductScoreTextEffectFactory>();
        builder.RegisterComponentInHierarchy<IlluminateScoreTextEffectFactory>();
        builder.RegisterComponentInHierarchy<LineCountTextEffectFactory>();
        builder.RegisterComponentInHierarchy<LineScoreTextEffectFactory>();
        builder.RegisterComponentInHierarchy<NowhereTextEffectFactory>();

        builder.RegisterComponentInHierarchy<StraightTileFactory>();
        builder.RegisterComponentInHierarchy<CurveTileFactory>();
        builder.RegisterComponentInHierarchy<TwinCurvesTileFactory>();
        builder.RegisterComponentInHierarchy<CrossTileFactory>();
        builder.RegisterComponentInHierarchy<ThreeWayDistributorTileFactory>();
        builder.RegisterComponentInHierarchy<FourWayDistributorTileFactory>();
        builder.RegisterComponentInHierarchy<BulbTileFactory>();

        builder.RegisterComponentInHierarchy<NormalPowerTileFactory>();
        builder.RegisterComponentInHierarchy<PlusPowerTileFactory>();
        builder.RegisterComponentInHierarchy<MinusPowerTileFactory>();
        builder.RegisterComponentInHierarchy<AlternatingPowerTileFactory>();

        builder.RegisterComponentInHierarchy<NormalTerminalTileFactoryLeft>();
        builder.RegisterComponentInHierarchy<NormalTerminalTileFactoryRight>();
        builder.RegisterComponentInHierarchy<PlusTerminalTileFactory>();
        builder.RegisterComponentInHierarchy<MinusTerminalTileFactory>();
        builder.RegisterComponentInHierarchy<AlternatingTerminalTileFactoryLeft>();
        builder.RegisterComponentInHierarchy<AlternatingTerminalTileFactoryRight>();

        builder.RegisterComponentInHierarchy<Footer>();
        builder.RegisterComponentInHierarchy<Header>();
        
        builder.RegisterComponentInHierarchy<SteamAchievementsScreen>();
        builder.RegisterComponentInHierarchy<BackScreen>();
        builder.RegisterComponentInHierarchy<BlackScreen>();
        builder.RegisterComponentInHierarchy<CreditsScreen>();
        builder.RegisterComponentInHierarchy<SteamInstructionScreen>();
        builder.RegisterComponentInHierarchy<LicensesScreen>();
        builder.RegisterComponentInHierarchy<RecordsScreen>();
        builder.RegisterComponentInHierarchy<SteamResultScreen>();
        builder.RegisterComponentInHierarchy<SelectFrameSizeScreen>();
        builder.RegisterComponentInHierarchy<SteamSettingsScreen>();
        builder.RegisterComponentInHierarchy<StatisticsScreen>();
        builder.RegisterComponentInHierarchy<TitleScreen>();
        
        builder.RegisterComponentInHierarchy<BoardCellDetector>();
        builder.RegisterComponentInHierarchy<BoardCellPointer>();

        builder.RegisterComponentInHierarchy<CurrentTilePositionDetector>();
        builder.RegisterComponentInHierarchy<Desk>();
        builder.RegisterComponentInHierarchy<ExplosionFactory>();
        builder.RegisterComponentInHierarchy<FrameSmall>();
        builder.RegisterComponentInHierarchy<FrameMedium>();
        builder.RegisterComponentInHierarchy<FrameLarge>();
        
        builder.RegisterComponentInHierarchy<Localizer>();
        builder.RegisterComponentInHierarchy<MainCamera>();
        builder.RegisterComponentInHierarchy<MusicPlayer>();

        builder.RegisterComponentInHierarchy<SoundPlayer>();
        builder.RegisterComponentInHierarchy<SteamRecordSender>();
        builder.RegisterComponentInHierarchy<SteamRecordsReceiver>();
        builder.RegisterComponentInHierarchy<TutorialGuides>();


        // ------------------------------ Processes ------------------------------

#if UNITY_EDITOR
        builder.Register<IQuitGameProcess, EditorSteamQuitGameProcess>(Lifetime.Singleton);
#else
        builder.Register<IQuitGameProcess, SteamQuitGameProcess>(Lifetime.Singleton);
#endif
        builder.Register<AddScoreProcess>(Lifetime.Singleton);
        builder.Register<AddTileProcess>(Lifetime.Singleton);
        builder.Register<BeginMainGameProcess>(Lifetime.Singleton);
        builder.Register<ClearMainGameProcess>(Lifetime.Singleton);

        builder.Register<DecreaseTileRestAmountProcess>(Lifetime.Singleton);
        builder.Register<EarnAchievementsProcess>(Lifetime.Singleton);
        builder.Register<IEndMainGameProcess, SteamEndMainGameProcess>(Lifetime.Singleton);
        builder.Register<GenerateInitialTilesProcess>(Lifetime.Singleton);
        
        builder.Register<InitializeMainGameProcess>(Lifetime.Singleton);

        builder.Register<PrepareNextTileProcess>(Lifetime.Singleton);
        builder.Register<PutCurrentTileProcess>(Lifetime.Singleton);

        builder.Register<ResetMainGameProcess>(Lifetime.Singleton);
        builder.Register<RetryMainGameProcess>(Lifetime.Singleton);
        builder.Register<ReturnToTitleProcess>(Lifetime.Singleton);
        builder.Register<RotateCurrentTileProcess>(Lifetime.Singleton);

        builder.Register<ShowFrameProcess>(Lifetime.Singleton);
        builder.Register<ShowUpResultScreenProcess>(Lifetime.Singleton);


        builder.Register<StartMainGameProcess>(Lifetime.Singleton);
        builder.Register<TakeTileProcess>(Lifetime.Singleton);
        builder.Register<TutorialProcess>(Lifetime.Singleton);
        
        // ------------------------------ Presenter Controllers ------------------------------

        builder.Register<StraightTilePresenters>(Lifetime.Singleton);
        builder.Register<CurveTilePresenters>(Lifetime.Singleton);
        builder.Register<TwinCurvesTilePresenters>(Lifetime.Singleton);
        builder.Register<CrossTilePresenters>(Lifetime.Singleton);
        builder.Register<ThreeWayDistributorTilePresenters>(Lifetime.Singleton);
        builder.Register<FourWayDistributorTilePresenters>(Lifetime.Singleton);
        builder.Register<BulbTilePresenters>(Lifetime.Singleton);

        builder.Register<NormalPowerTilePresenters>(Lifetime.Singleton);
        builder.Register<PlusPowerTilePresenters>(Lifetime.Singleton);
        builder.Register<MinusPowerTilePresenters>(Lifetime.Singleton);
        builder.Register<AlternatingPowerTilePresenters>(Lifetime.Singleton);

        builder.Register<NormalTerminalTilePresentersLeft>(Lifetime.Singleton);
        builder.Register<NormalTerminalTilePresentersRight>(Lifetime.Singleton);
        builder.Register<PlusTerminalTilePresenters>(Lifetime.Singleton);
        builder.Register<MinusTerminalTilePresenters>(Lifetime.Singleton);
        builder.Register<AlternatingTerminalTilePresentersLeft>(Lifetime.Singleton);
        builder.Register<AlternatingTerminalTilePresentersRight>(Lifetime.Singleton);

        // ------------------------------ Presenters ------------------------------

        builder.RegisterEntryPoint<HeaderPresenter>();
        builder.RegisterEntryPoint<FooterPresenter>();

        builder.RegisterEntryPoint<SteamResultScreenPresenter>();
        builder.RegisterEntryPoint<TitleScreenPresenter>();

        builder.RegisterEntryPoint<BoardCellPointerPresenter>();
        builder.RegisterEntryPoint<DeskPresenter>();
        builder.RegisterEntryPoint<FrameSmallPresenter>();
        builder.RegisterEntryPoint<FrameMediumPresenter>();
        builder.RegisterEntryPoint<FrameLargePresenter>();
        builder.RegisterEntryPoint<SettingsScreenPresenter>();

        // ------------------------------ Event Processes  ------------------------------

        builder.RegisterEntryPoint<OnBoardCellDetected>();
        builder.RegisterEntryPoint<OnCurrentTilePositionDetected>();

        builder.RegisterEntryPoint<SteamGameStartProcess>();

        builder.RegisterEntryPoint<HeaderEventsProcess>();
        builder.RegisterEntryPoint<SteamInstructionScreenEventProcess>();
        builder.RegisterEntryPoint<RecordsScreenEventProcess>();
        builder.RegisterEntryPoint<ResultScreenEventProcess>();
        builder.RegisterEntryPoint<SelectFrameSizeScreenEventProcess>();
        builder.RegisterEntryPoint<SettingsScreenEventProcess>();
        builder.RegisterEntryPoint<TitleScreenEventProcess>();
        builder.RegisterEntryPoint<ReceivedSteamRecordsEventProcess>();

        // ------------------------------ Update Processes ------------------------------

        builder.RegisterEntryPoint<EscapeKeyPressedUpdateProcess>();
        builder.RegisterEntryPoint<SteamMouseClickedUpdateProcess>();
        builder.RegisterEntryPoint<MoveCurrentTileUpdateProcess>();
        builder.RegisterEntryPoint<MoveNextTileUpdateProcess>();

        builder.RegisterEntryPoint<SteamUpdateProcess>();
    }
}
