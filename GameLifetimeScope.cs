using EventHandlers;
using Models;
using Presenters;
using Processes;
using Processes.Updates;
using VContainer;
using VContainer.Unity;
using Views;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<CurrentResolutionCodeModel>(Lifetime.Singleton);
        builder.Register<CurrentTileModel>(Lifetime.Singleton);
        builder.Register<GameStateModel>(Lifetime.Singleton);
        builder.Register<MainBoardModel>(Lifetime.Singleton);
        builder.Register<MainFrameModel>(Lifetime.Singleton);
        builder.Register<NextTileModel>(Lifetime.Singleton);
        builder.Register<ScoreModel>(Lifetime.Singleton);
        builder.Register<SelectedBoardCellModel>(Lifetime.Singleton);
        builder.Register<SelectedHeaderButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedRecordsScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedResolutionButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedResultScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedSelectFrameSizeScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SelectedTitleScreenButtonModel>(Lifetime.Singleton);
        builder.Register<SteamModel>(Lifetime.Singleton);
        builder.Register<TileDeckModel>(Lifetime.Singleton);
        builder.Register<TileRestAmountModel>(Lifetime.Singleton);
        builder.Register<TilesModel>(Lifetime.Singleton);
        builder.Register<ValidCellPositionsModel>(Lifetime.Singleton);

        builder.RegisterComponentInHierarchy<AchievementsScreen>();
        builder.RegisterComponentInHierarchy<BannerShorted>();
        builder.RegisterComponentInHierarchy<BannerClosed>();
        builder.RegisterComponentInHierarchy<BlackScreen>();
        builder.RegisterComponentInHierarchy<BoardCellDetector>();
        builder.RegisterComponentInHierarchy<BoardCellPointer>();
        builder.RegisterComponentInHierarchy<CreditsScreen>();
        builder.RegisterComponentInHierarchy<CurrentTilePositionDetector>();
        builder.RegisterComponentInHierarchy<DeskFactory>();
        builder.RegisterComponentInHierarchy<Footer>();
        builder.RegisterComponentInHierarchy<Header>();
        builder.RegisterComponentInHierarchy<InstructionScreen>();
        builder.RegisterComponentInHierarchy<MainCamera>();
        builder.RegisterComponentInHierarchy<MusicPlayer>();
        builder.RegisterComponentInHierarchy<RecordsScreen>();
        builder.RegisterComponentInHierarchy<ResultScreen>();
        builder.RegisterComponentInHierarchy<SelectFrameSizeScreen>();
        builder.RegisterComponentInHierarchy<SettingsScreen>();
        builder.RegisterComponentInHierarchy<SoundPlayer>();
        builder.RegisterComponentInHierarchy<TextEffectFactory>();
        builder.RegisterComponentInHierarchy<TileFactory>();
        builder.RegisterComponentInHierarchy<TitleScreen>();

        builder.Register<AddScoreProcess>(Lifetime.Singleton);
        builder.Register<ChangeMusicVolumeProcess>(Lifetime.Singleton);
        builder.Register<ChangeResolutionProcess>(Lifetime.Singleton);
        builder.Register<ChangeSoundVolumeProcess>(Lifetime.Singleton);
        builder.Register<ClearMainGameProcess>(Lifetime.Singleton);
        builder.Register<ConductMainBoardProcess>(Lifetime.Singleton);
        builder.Register<DecreaseTileRestAmountProcess>(Lifetime.Singleton);
        builder.Register<EndMainGameProcess>(Lifetime.Singleton);
        builder.Register<GameStartProcess>(Lifetime.Singleton);
        builder.Register<IlluminateBoardProcess>(Lifetime.Singleton);
        builder.Register<InitializeMainGameProcess>(Lifetime.Singleton);
        builder.Register<LoadSettingsProcess>(Lifetime.Singleton);
        builder.Register<MainBoardProcess>(Lifetime.Singleton);
        builder.Register<PrepareNextTileProcess>(Lifetime.Singleton);
        builder.Register<PutTileOnFrameProcess>(Lifetime.Singleton);
        builder.Register<QuitGameProcess>(Lifetime.Singleton);
        builder.Register<ReturnToTitleProcess>(Lifetime.Singleton);
        builder.Register<SelectBoardCellProcess>(Lifetime.Singleton);
        builder.Register<SelectHeaderButtonProcess>(Lifetime.Singleton);
        builder.Register<SelectRecordsScreenButtonProcess>(Lifetime.Singleton);
        builder.Register<SelectResolutionSettingScreenProcess>(Lifetime.Singleton);
        builder.Register<SelectResultScreenButtonProcess>(Lifetime.Singleton);
        builder.Register<SelectSelectFrameSizeScreenProcess>(Lifetime.Singleton);
        builder.Register<SelectTitleScreenButtonProcess>(Lifetime.Singleton);
        builder.Register<ShowUpResultScreenProcess>(Lifetime.Singleton);
        builder.Register<ShowUpTitleScreenProcess>(Lifetime.Singleton);
        builder.Register<StartMainGameProcess>(Lifetime.Singleton);
        builder.Register<TakeTileProcess>(Lifetime.Singleton);
        builder.Register<UpdateValidCellPositionsProcess>(Lifetime.Singleton);

        builder.RegisterEntryPoint<CurrentTilePresenter>();
        builder.RegisterEntryPoint<DeskPresenter>();
        builder.RegisterEntryPoint<NextTilePresenter>();
        builder.RegisterEntryPoint<SettingsScreenPresenter>();
        builder.RegisterEntryPoint<TilesPresenter>();

        builder.RegisterEntryPoint<OnBoardCellDetected>();
        builder.RegisterEntryPoint<OnCurrentTilePositionDetected>();
        builder.RegisterEntryPoint<OnEscapeKeyPressedForReturnToTitle>();
        builder.RegisterEntryPoint<OnGameStart>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForPutCurrentTile>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForSelectHeader>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForSelectRecordsScreen>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForSelectResolution>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForSelectResultScreen>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForSelectTitleScreen>();
        builder.RegisterEntryPoint<OnMouseButtonLeftPressedForSelectSelectFrameSizeScreen>();
        builder.RegisterEntryPoint<OnMouseButtonPressedForQuitApplicationBlackScreen>();
        builder.RegisterEntryPoint<OnMouseButtonRightPressedForRotateCurrentTile>();
        builder.RegisterEntryPoint<OnPointerEventsFiredHeader>();
        builder.RegisterEntryPoint<OnPointerEventsFiredRecordsScreen>();
        builder.RegisterEntryPoint<OnPointerEventsFiredResultScreen>();
        builder.RegisterEntryPoint<OnPointerEventsFiredSelectFrameSizeScreen>();
        builder.RegisterEntryPoint<OnPointerEventsFiredSettingsScreen>();
        builder.RegisterEntryPoint<OnPointerEventsFiredTitleScreen>();
        

        builder.RegisterEntryPoint<SteamUpdateProcess>();
    }
}
