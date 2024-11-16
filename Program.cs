﻿using System.Runtime.InteropServices;
using CGI_Project;

Menu();

static void Menu() {
  // Variables
  Player player = new Player();
  Utility utility = new Utility();
  int userInput = -1;
  bool exitLoop = false;
  int reInput = -1;

  // Prompts the user at the start of the game
  while (!exitLoop) {
    Console.Clear();
    if (reInput == -1) {
      System.Console.WriteLine("1. Existing user? Login\n2. Create new account");

      try {
        userInput = int.Parse(Console.ReadLine());
      } catch (Exception e) {

      }
    }

    int[] answers = {
      1,
      2
    };

    if (userInput == 1 || reInput == 1) {
      if (utility.Login(ref player) == true) {
        Home(player);
        exitLoop = true;
      }
    } else if (userInput == 2 || reInput == 2) {
      utility.CreatePlayer();
      reInput = -1;
    } else {
      utility.IntInvalidInput(ref reInput, answers);
    }
  }
}

static void Home(Player player) {
  bool end = false;
  GameReport game = new GameReport(player);

  while(!end){
    if(player.GetXP() == 0){
      char[] items = player.GetItems();
      for(int i =0; i<items.Length;i++){
        Console.WriteLine(items[i]);
      }
    }
    end = true;
  }
}