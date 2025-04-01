// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.GamerServices
{
	public enum GamerPresenceMode
	{
		ArcadeMode, //Arcade Mode
		AtMenu, //At Menu
		BattlingBoss	, //Battling Boss
		CampaignMode, //	Campaign Mode
		ChallengeMode, //	Challenge Mode
		ConfiguringSettings, // // Configuring Settings
		CoOpLevel, //	Co-Op: Level. Includes a numeric value specified with PresenceValue.
		CoOpStage,// 	Co-Op: Stage. Includes a numeric value specified with PresenceValue.
		CornflowerBlue, //Cornflower Blue
		CustomizingPlayer, //Customizing Player
		DifficultyEasy, //Difficulty: Easy
		DifficultyExtreme, //Difficulty: Extreme
		DifficultyHard, //Difficulty: Hard
		DifficultyMedium	, // Difficulty: Medium
		EditingLevel	, // Editing Level
		ExplorationMode, //Exploration Mode
		FoundSecret, //Found Secret
		FreePlay	, // Free Play
		GameOver	, //Game Over
		InCombat, 	// In Combat
		InGameStore, //In Game Store
		Level, //Level. Includes a numeric value specified with PresenceValue.
		LocalCoOp, //Local Co-Op
		LocalVersus, //Local Versus
		LookingForGames, //Looking For Games
		Losing, //Losing
		Multiplayer, //Multiplayer
		NearlyFinished, // Nearly Finished
		None	, // NoPresence String Displayed
		OnARoll, // On a Roll
		OnlineCoOp, //Online Co-Op
		OnlineVersus, //Online Versus
		Outnumbered, //Outnumbered
		Paused, //Paused
		PlayingMinigame, //Playing Minigame
		PlayingWithFriends, //Playing With Friends
		PracticeMode	, //Practice Mode
		PuzzleMode, //Puzzle Mode
		ScenarioMode	, //Scenario Mode
		Score, //Score. Includes a numeric value specified with PresenceValue.
		ScoreIsTied, //Score is Tied
		SettingUpMatch, //Setting Up Match
		SinglePlayer, //	Single Player
		Stage, //Stage. Includes a numeric value specified with PresenceValue.
		StartingGame, //Starting Game
		StoryMode, //Story Mode
		StuckOnAHardBit, //	Stuck on a Hard Bit
		SurvivalMode	, //Survival Mode
		TimeAttack, //Time Attack
		TryingForRecord, //	Trying For Record
		TutorialMode	, //Tutorial Mode
		VersusComputer, //Versus Computer
		VersusScore, //	Versus: Score. Includes a numeric value specified with PresenceValue.
		WaitingForPlayers, //Waiting For Players
		WaitingInLobby, //Waiting In Lobby
		WastingTime, //	Wasting Time
		WatchingCredits, //	Watching Credits
		WatchingCutscene, //Watching Cutscene
		Winning, //	Winning
		WonTheGame, //	Won the Game
	}
}
