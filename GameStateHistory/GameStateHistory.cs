using System;
using System.Net.Mime;
using System.Runtime.InteropServices;
using TurboCollections;

namespace GameStateHistory
{
    class GameStateHistory
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }

    class Game
    {
        private TurboStack<state> stateHistory = new TurboStack<state>();
        private int currentLevel = 1;

        enum stateType
        {
            MainMenu,
            Settings,
            Level
        }

        struct state
        {
            public string Name;
            public stateType StateType;
            public int Number;

            public state(string Name, stateType StateType, int Number = 0)
            {
                this.Name = Name;
                this.StateType = StateType;
                this.Number = Number;
            }
        }
        
        
        
        public void Run()
        {
            stateHistory.Push(new state("Main Menu", stateType.MainMenu));
            while (true)
            {
                state currentState = stateHistory.Peek();
                switch (currentState.StateType)
                {
                    case stateType.MainMenu:
                        if (MainMenuSelection())
                        {
                            return;
                        }
                        break;
                    case stateType.Settings:
                        SettingsSelection();    
                        break;
                    case stateType.Level:
                        LevelSelection();
                        break;
                }
            }
        }
        
        bool MainMenuSelection()
        {
            // Can go to first level, settings, or quit. Can go back.
            OptionAndIdentifier[] OaI = new OptionAndIdentifier[4];
            OaI[0] = new OptionAndIdentifier("(0): Go to first level", "0");
            OaI[1] = new OptionAndIdentifier("(1): Go to settings", "1");
            OaI[2] = new OptionAndIdentifier("(2): Quit", "2");
            OaI[3] = new OptionAndIdentifier("(3): Go back in history", "3");

            switch (MultipleChoiceMachine(OaI))
            {
                case 0:
                    currentLevel = 1;
                    stateHistory.Push(new state("Level 1", stateType.Level, 1));
                    break;
                case 1:
                    stateHistory.Push(new state("Settings", stateType.Settings));
                    break;
                case 2:
                    Console.WriteLine("EXIT GAME");
                    return true;
                case 3:
                    GoBack();
                    break;
            }

            return false;
        }

        void SettingsSelection()
        {
            OptionAndIdentifier[] OaI = new OptionAndIdentifier[1];
            OaI[0] = new OptionAndIdentifier("(0): Go back in history", "0");
            
            switch (MultipleChoiceMachine(OaI))
            {
                case 0:
                    GoBack();
                    break;
            }
        }

        void LevelSelection()
        {
            // Can go to next level, main menu, or back.
            OptionAndIdentifier[] OaI = new OptionAndIdentifier[3];
            OaI[0] = new OptionAndIdentifier("(0): Go to next level", "0");
            OaI[1] = new OptionAndIdentifier("(1): Main menu", "1");
            OaI[2] = new OptionAndIdentifier("(2): Go back in history", "2");
            
            switch (MultipleChoiceMachine(OaI))
            {
                case 0:
                    GoToNextLevel();
                    break;
                case 1:
                    stateHistory.Push(new state("Main Menu", stateType.MainMenu));
                    break;
                case 2:
                    GoBack();
                    break;
            }
        }

        void GoToNextLevel()
        {
            currentLevel++;
            stateHistory.Push(new state($"Level {currentLevel}", stateType.Level, currentLevel));
        }

        void GoBack()
        {
            if (stateHistory.Peek().StateType == stateType.Level)
                currentLevel--;
            stateHistory.Pop();
        }

        void GoForward()
        {
            
        }
        
        
        

        struct OptionAndIdentifier
        {
            public string Option;
            public string Identifier;

            public OptionAndIdentifier(string option, string identifier)
            {
                Option = option;
                Identifier = identifier;
            }
        }

        /// <summary>
        /// Returns a index for the selected 
        /// </summary>
        /// <param name="???"></param>
        /// <returns></returns>
        int MultipleChoiceMachine(OptionAndIdentifier[] optionsAndIdentifiers)
        {
            Console.WriteLine($"You are here: {stateHistory.Peek().Name}");
            Console.WriteLine("What do you want to do?");
            foreach (var option in optionsAndIdentifiers)
            {
                Console.WriteLine(option.Option);
            }
            while (true)
            {
                var input = Console.ReadLine();
                for (int i = 0; i < optionsAndIdentifiers.Length; i++)
                {
                    if (optionsAndIdentifiers[i].Identifier == input)
                        return i;
                }

                Console.WriteLine("Invalid input!");
            }
        }
    }
}