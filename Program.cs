using System;
using System.Collections.Generic;

class RockPaperScissors
{
    //The static movelist containing allof the valid moves. Populated in the main.
    public static List<Move> moveList = new List<Move>();
    static void Main(string[] args)
    {
        Console.WriteLine("Welcom to Rock, Paper, Scissors!\n");

        Console.WriteLine("You must win a best of 3 to claim victory. The valid moves are rock, scissors, paper and flamethrower. To start, enter 1 to play against a friend or 2 to play against the AI");

        string mode;
        string AImode = "0";

        moveList.Add(new Move("rock", new string[] { "scissors", "flamethrower" }, new string[] { "paper" }));
        moveList.Add(new Move("paper", new string[] { "rock" }, new string[] { "scissors", "flamethrower" }));
        moveList.Add(new Move("scissors", new string[] { "paper", "flamethrower" }, new string[] { "rock" }));
        moveList.Add(new Move("flamethrower", new string[] { "paper" }, new string[] { "rock", "scissors" }));

        while (true)
        {
            mode = Console.ReadLine();

            if (mode.Equals("1"))
            {
                Console.WriteLine("You have selected PvP.\n");
                playGame(AImode);
                break;
            }
            else if (mode.Equals("2"))
            {
                Console.WriteLine("You have selected PvE.\n");
                Console.WriteLine("Select the following computer behavious: 1 for previous selection and 2 for random selection");

                while (true)
                {
                    AImode = Console.ReadLine();

                    if (AImode.Equals("1"))
                    {
                        Console.WriteLine("You have selected previous selection.\n");
                        playGame(AImode);
                        break;
                    }
                    else if (AImode.Equals("2"))
                    {
                        Console.WriteLine("You have selected random selection.\n");
                        playGame(AImode);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that mode is not recognized, try again.");
                    }
                }
                break;
            }
            else
            {
                Console.WriteLine("Sorry, that mode is not recognized, try again.");
            }
        }
        Console.WriteLine("The game is finished. Goodbye!");
    }

    //Move class with a constructor that takes in the name of the move as well as what moves it beats and what moves it loses too.
    public class Move
    {
        public string[] Beats;
        public string[] Loses;
        public string name;
        public Move(string name, string[] Beats, string[] Loses)
        {
            this.name = name;
            this.Beats = Beats;
            this.Loses = Loses;
        }
    }

    //Gameplay logic.
    public static void playGame(string AImode)
    {
        string player1move, player2move, winMessage;
        int player1wins = 0, player2wins = 0;
        string previousAImove = "rock";

        while (player1wins < 2 && player2wins < 2)
        {
            Console.WriteLine("Player 1, enter your move:");

            player1move = checkIfMoveValid(Console.ReadLine());

            Console.WriteLine("Player 2, enter your move:");

            if (AImode.Equals("0"))
            {
                player2move = checkIfMoveValid(Console.ReadLine());
                winMessage = Battle(player1move, player2move);
            } else
            {
                previousAImove = AImoveSelect(previousAImove, AImode);
                Console.WriteLine(previousAImove);
                winMessage = Battle(player1move, previousAImove);
            }

            Console.WriteLine(winMessage);

            if (winMessage.Equals("Player 1 wins!"))
            {
                player1wins++;
            }
            else if (winMessage.Equals("Player 2 wins!"))
            {
                player2wins++;
            }
        }

        if (player1wins > player2wins)
        {
            Console.WriteLine("Player 1 wins the match!");
        }
        else
        {
            Console.WriteLine("Player 2 wins the match!");
        }
    }

    //Ensures that the move entered by the player is actually part of the move list.
    public static string checkIfMoveValid(string move)
    {
        string currentMove = move;

        while (true)
        {
            for(int i = 0; i < moveList.Count; i++)
            {
                if (moveList[i].name == currentMove)
                {
                    return currentMove;
                }
            }
            Console.WriteLine("Sorry! That move is not recognized. Try again.");
            currentMove = Console.ReadLine();
        }
    }

    //The battle simulator between two moves. Finds either which of the moves wins or if neither of them succeeds.
    public static string Battle(string player1move, string player2move)
    {
        int move1position = findMoveListPosition(player1move);
        int move2position = findMoveListPosition(player2move);

        if (moveList[move1position].Beats.Contains(moveList[move2position].name) && moveList[move2position].Loses.Contains(moveList[move1position].name))
        {
            return "Player 1 wins!";
        }
        else if (moveList[move2position].Beats.Contains(moveList[move1position].name) && moveList[move1position].Loses.Contains(moveList[move2position].name))
        {
            return "Player 2 wins!";
        }
        else
        {
            return "Nobody wins, try again!";
        }
    }

    //Chooses the AI move based on ether what the previous move was, or randomly.
    public static string AImoveSelect(string previousAIselect, string AImove)
    {
        var random = new Random();

        if(AImove.Equals("1"))
        {
            return moveList[findMoveListPosition(previousAIselect)].Loses[0];
        }
        else
        {
            return moveList[random.Next(moveList.Count)].name;
        }
    }

    //Finds numerical positions in movelist based on move name.
    public static int findMoveListPosition(string move)
    {
        for (int i = 0; i < moveList.Count; i++)
        {
            if (moveList[i].name == move)
            {
                return i;
            }
        }
        return 0;
    }
}

