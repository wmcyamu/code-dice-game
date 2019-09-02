using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DiceGame_Prog
{
    class Game
    {
        public Player player1 = new Player();
        public Player player2 = new Player();
        public int score = 0;
        public int turn = 1;
        int Turn;
        int player;
        int nreRoll;
        public bool retry;
        public ArrayList MainRoll = new ArrayList();
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to My dice Game \nPress enter to see description.");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Once again hello this game can be played by 2 players so decide who goes first and who goes second.\nThis Game is basic, all you have to do is press 1 then your 5 dice roll and goes in order \nPlayer 1 being first and Player 2 being second.\n\nThe scoring is also simple if you roll 3,4 or 5 of a kind of dice you get either 3,6 or 12 points respectively\nallocated to you and first player to reach 50 points wins. \nShould you roll 2 of a kind the game will automaticaly re roll once for you \nbut if after that re roll you still do not have 3 or more of a kind you will get 0 points.\nIf your roll and you do not have any matching dice i.e.: 0 of a kind you will get 0 points.\n\nThat is all, when your ready to start press enter to start rolling your dice and enjoy.");
            Console.ReadLine();
            Console.Clear();

            Game NG = new Game();
            NG.menu();

        }


        public void menu()
        {

            Console.WriteLine("\nPress 1 to roll all dice");
            string choice = Console.ReadLine();
            game(choice);

        }

        public void game(string menuChoice)
        {
            Dice CRoll = new Dice();
            List<string> pastRolls = new List<string>();

            switch (menuChoice)
            {

                case "1":

                    do
                    {
                        retry = false;

                        CRoll.RollAll(turn);
                        //Rolls all Dice method
                        MainRoll = CRoll.DieGen;
                        int[] myArray2 = (int[])MainRoll.ToArray(typeof(int));
                        //Converst my array from string to int
                        for (int i = 0; i < myArray2.Length; i++)
                        {
                            for (int j = i + 1; j < myArray2.Length; j++)
                            {
                                for (int k = j + 1; k < myArray2.Length; k++)
                                {

                                    if ((myArray2[i] == myArray2[j] && myArray2[j] == myArray2[k]))
                                    {
                                        //Variable Checker
                                        score = score + 6;
                                    }
                                }
                            }
                        }
                        if ((score > 6 && score < 37))
                        {
                            //Sets desired Score for 4 and 5 of a kind
                            score = 12;
                        }
                        else
                        {
                            if (score > 37)
                            {
                                score = 24;
                            }
                        }
                        if (score == 0 && retry == false)
                        {
                            retry = true;
                            nreRoll++;
                        }

                        MainRoll.Clear();
                        Array.Clear(myArray2, 0, myArray2.Length);
                        //Clears array and array list for re use
                    }

                    while ((retry == true && nreRoll == 1));
                    Turn = turn % 2;
                    //Turn Handling
                    if (Turn == 1)
                    {
                        player = 1;
                        player1.RScore = score;
                        player1.PScore(player);
                    }
                    else
                    {

                        player = 2;
                        player2.RScore = score;
                        player2.PScore(player);
                    }
                    turn++;
                    score = 0;
                    nreRoll = 0;
                    menu();
                    //Start Menu call to start again                          
                    break;


                default:
                    //Exception Handling
                    Console.WriteLine("\nIn Dun to you traveler i see you did not read the instructions, you know press 1 to roll all dice,\n use this information wisely ;)");
                    menu();
                    break;

            }

        }


    }

    class Dice
    {
        public ArrayList DieGen = new ArrayList();
        public void RollAll(int GenNumber)
        {
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                //Loops
                int rng = rnd.Next(1, 6 + 1);

                //Get rnd Numb in range 1-6   
                Console.WriteLine("You Rolled: {0}", rng);
                //Adds rnd Generated Number to list
                DieGen.Add(rng);

            }
            Console.WriteLine();

        }

        public void retry()
        {
            Random rnd = new Random();
            DieGen.RemoveAt(4);
            DieGen.RemoveAt(3);
            for (int i = 0; i < 2; i++)
            {
                int rng = rnd.Next(1, 6 + 1);
                Console.WriteLine("You reRolled: {0}", rng);

                DieGen.Add(rng);
            }
        }
    }

    class Player
    {
        public int TScore = 0;
        public int RScore = 0;

        public void PScore(int player)

        {
            TScore = TScore + RScore;
            Console.WriteLine("\nPlayer {0} score is {1}", player, TScore);
            if (TScore >= 50)
            {
                //Score Check to see if Game ends
                Console.WriteLine("Congrtulations Player {0} you won seems, lady luck was on your side ;)", player);
                Console.ReadLine();
                System.Environment.Exit(1);
            }
        }
    }
}