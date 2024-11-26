using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace CGI_Project {
  public class GameReport {
    private Player player;
    private int upperBound;
    private int lowerBound;
    private ConsoleKey key;
    private ConsoleKey prev;
    private ConsoleKey accessInventory;
    private int enemy;
    private int boss;
    private int enemyHealth = 50;
    private int num;
    private bool newNum = false;
    private bool ovrRide = false;
    private int[][] currentIslands;
    private string[][] printedEnemy;
    private bool[] isEnemy;
    private bool enterLevel = false;
    private bool switchIslands = false;
    private int randomIslandCount;

    public GameReport() {

    }

    public GameReport(Player player) {
      this.player = player;
    }

    private void SetUpperBound(int bound) {
      this.upperBound = bound;
    }

    private void SetLowerBound(int bound) {
      this.lowerBound = bound;
    }

    private void SetKey(ConsoleKey key) {
      this.key = key;
    }

    private void SetPrev(ConsoleKey prev) {
      this.prev = prev;
    }

    private void SetAccessInventory(ConsoleKey accessInventory) {
      this.accessInventory = accessInventory;
    }

    private ConsoleKey GetKey() {
      return key;
    }

    private ConsoleKey GetPrev() {
      return prev;
    }

    private ConsoleKey GetAccessInventory() {
      return accessInventory;
    }

    private void SwitchOvrRide() {
      ovrRide = !ovrRide;
    }

    private void SwitchIslands(){
      switchIslands = !switchIslands;
    }

    private void SetEnemy(int enemy) {
      this.enemy = enemy;
    }
    private void SetBoss(int boss) {
      this.boss = boss;
    }

    private void SetEnemyHealth(int enemyHealth) {
      this.enemyHealth = enemyHealth;
    }

    private void SetCurrentIslands(int[][] currentIslands) {
      this.currentIslands = currentIslands;
    }

    private void SetPrintedEnemy(string[][] printedEnemy) {
      this.printedEnemy = printedEnemy;
    }

    private void SetRandomNum() {
      Random rnd = new Random();
      int number = -1;

      while (number % 3 != 0) {
        number = rnd.Next(lowerBound + 6, upperBound - 6);
      }

      if(player.GetPos() <= 20){
        this.num = number+1;
      } else {
        this.num = number;
      }
    }

    private int RandomStart(int stop){
      bool end = false;
      Random rnd = new Random();
      int number = rnd.Next(0,3);

      switch(number){
        case 0:
          return stop+3;
          break;
        case 1:
          return stop+6;
          break;
        case 2:
          return stop+9;
          break;
      }
      
      return -1;
    }

    private int RandomStop(int start, int length){
      bool end = false;
      Random rnd = new Random();
      int number = rnd.Next(0,6);

      if(player.GetPos() <= 20){

      switch(number){
        case 0:
          return start+length+3+2;
          break;
        case 1:
          return start+length+6+2;
          break;
        case 2:
          return  start+length+9+2;
          break;
        case 3:
          return start+length-3+2;
          break;
        case 4:
          return start+length-6+2;
          break;
        case 5:
          return  start+length-9+2;
          break;
      }
      } else {
        switch(number){
        case 0:
          return start+length+3;
          break;
        case 1:
          return start+length+6;
          break;
        case 2:
          return  start+length+9;
          break;
        case 3:
          return start+length-3;
          break;
        case 4:
          return start+length-6;
          break;
        case 5:
          return  start+length-9;
          break;
      }
      }
      
      return -1;
    }

    private int RandomHeight(){
      Random rnd = new Random();
      
      return rnd.Next(5,21);
    }

    private bool CreateEnemy(){
      Random rnd = new Random();
      int number = rnd.Next(0,2);

      if(number == 0){
        return true;
      } else {
        return false;
      }
    }

    private void SetIsEnemy(bool[] isEnemy){
      this.isEnemy = isEnemy;
    }

    public void Tutorial() {
      Utility util = new Utility(player);
      PlayerFileHandler file = new PlayerFileHandler(player);
      Console.Clear();

      // Variables
      ConsoleKeyInfo key;
      bool end = false;
      player.SetPos(11);
      SetLowerBound(11);
      SetUpperBound(29);
      SetPrev(ConsoleKey.D);
      util.ResetItems();
      this.enemy = 0;

      string[] enemyHead = {"","",""};
      string[] enemyBody = {"","",""};

      string[][] newPrintedEnemy = new string[][] {enemyHead, enemyBody};
      SetPrintedEnemy(newPrintedEnemy);

      util.AddItem('b');

      StartingScreen();
      TutorialMap();

      do {
        util.CheckActivatedItems();
        ConsoleKey newKey = new ConsoleKey();

        if (ovrRide == false) {

          newKey = Console.ReadKey().Key;
          SetKey(newKey);

          if (GetKey() == ConsoleKey.D && player.GetPos() < upperBound) {
            player.IncPos();
          } else if (GetKey() == ConsoleKey.A && player.GetPos() > lowerBound) {
            player.DecPos();
          } else if (player.GetPos() >= currentIslands[3][1] - 1) {
            Console.Clear();
            System.Console.WriteLine("\n\nCongratulations, you passed the tutorial! You will now be sent to your home base.\n\nPress any key to continue");
            Console.ReadKey();
            player.SetXP(player.GetXpToEarn());
            file.SaveExistingPlayer();
            end = true;
          }
        } else {
          SwitchOvrRide();
        }

        TutorialMap();

        SetAccessInventory(newKey);

      } while (!end);
    }

    private void TutorialMap() {
      Console.Clear();
      Utility util = new Utility(player);

      // Variables
      string answer = "";

      TutorialIsland();

      if (player.GetPos() == currentIslands[0][1] - 1 || player.GetPos() == currentIslands[1][1] - 1 || player.GetPos() == currentIslands[2][1] - 1) {
        System.Console.WriteLine();
        if (Question("easy") == true) {
          if (player.GetPos() == currentIslands[0][1] - 1) {
            player.SetPos(currentIslands[1][0] + 1);
            SetUpperBound(currentIslands[1][1] - 1);
            newNum = !newNum;
          } else if (player.GetPos() == currentIslands[1][1] - 1) {
            player.SetPos(currentIslands[2][0] + 1);
            SetUpperBound(currentIslands[2][1] - 1);
            newNum = !newNum;
          } else if (player.GetPos() == currentIslands[2][1] - 1) {
            player.SetPos(currentIslands[3][0] + 1);
            SetUpperBound(currentIslands[3][1] - 1);
            newNum = !newNum;
          }

          SetLowerBound(player.GetPos());
          SwitchOvrRide();
        } else {
          player.SetPos(lowerBound);
          SwitchOvrRide();
        }
      } else if (player.GetPos() >= currentIslands[3][3] - 3 && player.GetPos() <= currentIslands[3][3] - 1 && enemyHealth > 0) {
        System.Console.WriteLine();
        if (Question("medium") == true) {
          SetEnemyHealth(enemyHealth - player.GetDamage());
          SwitchOvrRide();
        } else {
          player.SetPos(currentIslands[3][0] + 1);
          SwitchOvrRide();
        }
      } else {
        Inventory();
        System.Console.WriteLine($"Health: {player.GetHealth()}");
        System.Console.WriteLine($"Damage: {player.GetDamage()}");
        System.Console.Write($"Activated Items: ");
        util.PrintActivatedItems();
        System.Console.WriteLine();
      }
    }

    private void Inventory(bool home = false) {
      if (key == ConsoleKey.Enter && accessInventory == ConsoleKey.S) {
        SwitchOvrRide();
        if (player.GetItems() != null) {
          InventoryList(home);
        } else {
          Console.Clear();
          System.Console.WriteLine("\nYou have no items in your inventory, you can buy more at your home base\n\nPress any key to continue:");
          Console.ReadKey();
        }
      } else if (key == ConsoleKey.S) {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("\nInventory");
        Console.ForegroundColor = ConsoleColor.Gray;
      } else {
        System.Console.WriteLine("\nInventory");
      }
    }

    private void InventoryList(bool home = false) {
      Utility util = new Utility(player);
      bool end = false;
      char[] items = player.GetItems();
      
      int itemNav = 0; // Tracks the current selection
      int visibleItemCount = 0; // Tracks the number of valid items displayed

      while (!end) {
        Console.Clear();
        Console.WriteLine("\nUse W to move up, S to move down, and ENTER to select\n");

        visibleItemCount = 0;

        // Display valid items and highlight the current selection
        for (int i = 0; i < items.Length; i++) {
          string description = GetItemDescription(items[i]);
          if (description == null) continue; // Skip unknown items

          if (itemNav == visibleItemCount) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(description);
            Console.ForegroundColor = ConsoleColor.Gray;
          } else {
            Console.WriteLine(description);
          }
          visibleItemCount++;
        }

        // Highlight "Exit" option
        if (itemNav == visibleItemCount) {
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("Exit");
          Console.ForegroundColor = ConsoleColor.Gray;
        } else {
          Console.WriteLine("Exit");
        }

        // Read input from the player
        ConsoleKey key = Console.ReadKey().Key;

        // Navigate up and down the list
        if (key == ConsoleKey.S && itemNav < visibleItemCount) {
          itemNav++;
        } else if (key == ConsoleKey.W && itemNav > 0) {
          itemNav--;
        } else if (key == ConsoleKey.Enter) {
          // Exit the inventory if "Exit" is selected
          if (itemNav == visibleItemCount) {
            return;
          }

          // Get the selected item (recalculate based on valid items)
          int selectedIndex = -1;
          visibleItemCount = 0;

          for (int i = 0; i < items.Length; i++) {
            string description = GetItemDescription(items[i]);
            if (description == null) continue; // Skip unknown items

            if (visibleItemCount == itemNav) {
              selectedIndex = i;
              break;
            }
            visibleItemCount++;
          }

          char selectedItem = items[selectedIndex];

          // Check if the item is already in use
          string itemsInUse = player.GetItemsInUse();
          bool isInUse = !string.IsNullOrEmpty(itemsInUse) && itemsInUse.Contains(selectedItem);

          if (isInUse) {
            Console.Clear();
            Console.WriteLine("Item is already in use.\n\nPress any key to continue...");
            Console.ReadKey();
          } else {
            if(home == true){
              Console.Clear();
              System.Console.WriteLine("Are you sure you want to remove this item from you inventory? (Enter to delete, any key to exit)");
              if(Console.ReadKey().Key == ConsoleKey.Enter){
                util.RemoveItems(selectedItem);
              }
              return;
            }
            util.ActivateItem(selectedItem);
            end = true;
          }
        }
      }
    }

    // Helper function to get item descriptions, returns null for unknown items
    private string GetItemDescription(char itemCode) {
      if (itemCode == 'b') return "Bonus XP";
      if (itemCode == 'd') return "Damage Boost";
      if (itemCode == 'h') return "Restore Health";
      if (itemCode == 'i') return "Increase Max Health";
      return null; // Unknown item
    }

    private void TutorialIsland() {
      Console.Clear();
      bool used = false;
      bool putEnemy = false;

      // Island# - Start Stop Height Enemy or Not
      int[] island1 = {
        10,
        30,
        15,
        -1
      };
      int[] island2 = {
        40,
        75,
        10,
        -1
      };
      int[] island3 = {
        80,
        100,
        17,
        -1
      };
      int[] island4 = {
        110,
        145,
        30,
        enemy
      };

      int[][] allIslands = new int[][] {
        island1,
        island2,
        island3,
        island4
      };

      SetCurrentIslands(allIslands);

      System.Console.WriteLine(Prompt());

      if(player.GetPos() >= island4[0] && player.GetPos() <= island4[1]){
        putEnemy = true;
      }

      for (int i = 0; i < 53; i++) {
        for (int j = 0; j < 147; j++) {
          Island(island1[0], island1[1], island1[2], i, j, ref used, 53, ref putEnemy, false, true);
          Island(island2[0], island2[1], island2[2], i, j, ref used, 53, ref putEnemy, false, true);
          Island(island3[0], island3[1], island3[2], i, j, ref used, 53, ref putEnemy, false, true);
          Island(island4[0], island4[1], island4[2], i, j, ref used, 53, ref putEnemy, false, true);

          if (!used){
            System.Console.Write(" ");
          }

          used = false;
        }
        System.Console.WriteLine();
      }
    }

    private void Island(int start, int stop, int height, int i, int j, ref bool used, int loopLength, ref bool enemy, bool boss = false, bool tutorial = false) {
      height = loopLength - height;
      bool onIsland = false;

      if (player.GetPos() >= start && player.GetPos() <= stop) {
        onIsland = true;
      }

      if (i == height - 1 && j == player.GetPos() && player.GetPos() >= start && player.GetPos() <= stop) {
        System.Console.Write("-|-");
        used = true;
      } else if (i == height - 2 && j == player.GetPos() && player.GetPos() >= start && player.GetPos() <= stop && GetKey() == ConsoleKey.D) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.Write(" 0>");
        Console.ForegroundColor = ConsoleColor.Gray;
        used = true;
      } else if (i == height - 2 && j == player.GetPos() && player.GetPos() >= start && player.GetPos() <= stop && GetKey() == ConsoleKey.A) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.Write("<0 ");
        Console.ForegroundColor = ConsoleColor.Gray;
        used = true;
      } else if (i == height - 2 && j == player.GetPos() && player.GetPos() >= start && player.GetPos() <= stop) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (GetPrev() == ConsoleKey.D) {
          System.Console.Write(" 0>");
        } else if (GetPrev() == ConsoleKey.A) {
          System.Console.Write("<0 ");
        }
        Console.ForegroundColor = ConsoleColor.Gray;
        used = true;
      } else if (i == height && j >= start && j <= stop) {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.Write("_");
        Console.ForegroundColor = ConsoleColor.Gray;
        used = true;
      } else if (i > height && j >= start && j <= stop) {
        System.Console.Write("|");
        used = true;
      } else if (i == height - 1 && j == player.GetPos() + 1 && onIsland == true || i == height - 2 && j == player.GetPos() + 1 && onIsland == true) {
        used = true;
      } else if (i == height - 1 && j == player.GetPos() - 1 && onIsland == true || i == height - 2 && j == player.GetPos() - 1 && onIsland == true) {
        used = true;
      }

      if (enemy == true && enemyHealth > 0) {
        if (newNum == true && enemy == true && onIsland == true) {
          SetRandomNum();
          SetEnemy(num);
          PrintedEnemy();
          newNum = !newNum;
        }

        if(num < start && player.GetPos() >= start && player.GetPos() <= stop || num > stop && player.GetPos() >= start && player.GetPos() <= stop ){
          enemy = false;
          System.Console.WriteLine("Error");
        } else {
            
          if (i == height - 1 && j == num-1 && player.GetPos() >= start && player.GetPos() <= stop && enemy == true && tutorial == false && !string.IsNullOrEmpty(printedEnemy[1][0])) {
          if(printedEnemy[1][0] == "$"){
            System.Console.Write(" ");
          } else {
            System.Console.Write(printedEnemy[1][0]);
          }
          
          used = true;
        } else if (i == height - 1 && j == num && player.GetPos() >= start && player.GetPos() <= stop && enemy == true && tutorial == false && !string.IsNullOrEmpty(printedEnemy[1][1])) {
          if(printedEnemy[1][1] == "$"){
            System.Console.Write(" ");
          } else {
            System.Console.Write(printedEnemy[1][1]);
          }

          used = true;
        } else if (i == height - 1 && j == num+1 && player.GetPos() >= start && player.GetPos() <= stop && enemy == true && tutorial == false && !string.IsNullOrEmpty(printedEnemy[1][2])) {
          if(printedEnemy[1][2] == "$"){
            System.Console.Write(" ");
          } else {
            System.Console.Write(printedEnemy[1][2]);
          }

          used = true;
        } else if (i == height - 2 && j == num-1 && player.GetPos() >= start && player.GetPos() <= stop && enemy == true && tutorial == false && !string.IsNullOrEmpty(printedEnemy[0][0])) {
          System.Console.Write(printedEnemy[0][0]);
          used = true;
        } else if (i == height - 2 && j == num && player.GetPos() >= start && player.GetPos() <= stop && enemy == true && tutorial == false && !string.IsNullOrEmpty(printedEnemy[0][1])) {
          System.Console.Write(printedEnemy[0][1]);
          used = true;
        } else if (i == height - 2 && j == num+1 && player.GetPos() >= start && player.GetPos() <= stop && enemy == true && tutorial == false && !string.IsNullOrEmpty(printedEnemy[0][2])) {
          System.Console.Write(printedEnemy[0][2]);
          used = true;
        } else if (i == height - 2 && j == num && player.GetPos() >= start && player.GetPos() <= stop && enemy == true && tutorial == true){
          Console.Write("8-8");
          used = true;
        } else if (i == height - 1 && j == num && player.GetPos() >= start && player.GetPos() <= stop && enemy == true && tutorial == true){
          Console.Write(" I ");
          used = true;
        }
      }
      }

      if (GetKey() == ConsoleKey.D || GetKey() == ConsoleKey.A) {
        SetPrev(GetKey());
      }
    }

    private void StartingScreen() {
      Console.Clear();
      System.Console.WriteLine("It looks like you're new to the game, here's a tutorial to help you get started\n\nPress any key to continue:");
      Console.ReadKey();
      Console.Clear();
      System.Console.WriteLine("Rules:\n1. In order for the game to print as intended, use an 80x24 terminal\n2. Press the D key to move forward, and the A to move backwards\n3. To interact with an object, stand on it and press the enter key\n\nPress any key to continue:");
      Console.ReadKey();
    }

    private string Prompt() {
      if (player.GetPos() >= 10 && player.GetPos() <= 30) {
        return "\n\nPress the D button to move forward, and the A button to move backwards";
      } else if (player.GetPos() >= 40 && player.GetPos() <= 75) {
        return "\n\nYou've been given a free item, to check you inventory, press s and then enter";
      } else if (player.GetPos() >= 80 && player.GetPos() <= 100) {
        return "\n\nIncompleting a jump will result in you falling to your death\n\nIn this tutorial however, you can't die";
      } else if (player.GetPos() >= 110 && player.GetPos() <= 160) {
        return "\n\nYou will encounter enemys, get the answers correct to deal damage to them";
      }
      return "";
    }

    private void PrintedEnemy() {
      Random rnd1 = new Random();
      string[] enemyHead = {" "," "," "};
      string[] enemyBody = {" "," "," "};
      string[][] newEnemy = {enemyHead, enemyBody};

      do{
      int number = rnd1.Next(0, 5);
      switch (number) {
      case 0:
        enemyHead[0] = "-";
        enemyHead[1] = "_";
        enemyHead[2] = "-";
        enemyBody[0] = "$";
        enemyBody[1] = "I";
        enemyBody[2] = "$";
        SetPrintedEnemy(newEnemy);
        break;
      case 1:
        enemyHead[0] = "^";
        enemyHead[1] = "o";
        enemyHead[2] = "^";
        enemyBody[0] = "(";
        enemyBody[1] = "$";
        enemyBody[2] = ")";
        SetPrintedEnemy(newEnemy);
        break;
      case 2:
        enemyHead[0] = ":";
        enemyHead[1] = "-";
        enemyHead[2] = ":";
        enemyBody[0] = "|";
        enemyBody[1] = "|";
        enemyBody[2] = "|";
        SetPrintedEnemy(newEnemy);
        break;
      case 3:
        enemyHead[0] = ".";
        enemyHead[1] = "O";
        enemyHead[2] = ".";
        enemyBody[0] = "$";
        enemyBody[1] = "I";
        enemyBody[2] = "$";
        SetPrintedEnemy(newEnemy);
        break;
      default:
        enemyHead[0] = "-";
        enemyHead[1] = "|";
        enemyHead[2] = "-";
        enemyBody[0] = "|";
        enemyBody[1] = "|";
        enemyBody[2] = "|";
        SetPrintedEnemy(newEnemy);
        break;
      }
      } while(string.IsNullOrEmpty(enemyHead[0]) || string.IsNullOrEmpty(enemyHead[1]) || string.IsNullOrEmpty(enemyHead[2]) || string.IsNullOrEmpty(enemyBody[0]) || string.IsNullOrEmpty(enemyBody[1]) || string.IsNullOrEmpty(enemyBody[2]));
    }

    private bool Question(string difficulty) {
      PlayerFileHandler file = new PlayerFileHandler(player);

      // Retrieve the question and options
      string[] questionData = file.Question(difficulty);
      string question = questionData[0];
      string answer = questionData[1];
      string option1 = questionData[2];
      string option2 = questionData[3];
      string option3 = questionData[4];
      string option4 = questionData[5];

      string[] options = {
        option1,
        option2,
        option3,
        option4
      };
      int currentSelection = 0; // Tracks the current highlighted option

      // Display the initial question and options
      Console.WriteLine(question);
      for (int i = 0; i < options.Length; i++) {
        if (i == currentSelection) {
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine(options[i]);
          Console.ForegroundColor = ConsoleColor.Gray;
        } else {
          Console.WriteLine(options[i]);
        }
      }

      while (true) {
        ConsoleKey key = Console.ReadKey(true).Key;

        // Update navigation and redisplay options
        if (key == ConsoleKey.S && currentSelection < options.Length - 1) {
          currentSelection++;
        } else if (key == ConsoleKey.W && currentSelection > 0) {
          currentSelection--;
        } else if (key == ConsoleKey.Enter) {
          // Check if the selected option is correct
          return options[currentSelection].Equals(answer, StringComparison.OrdinalIgnoreCase);
        }

        // Overwrite the options in place
        Console.SetCursorPosition(0, Console.CursorTop - options.Length);
        for (int i = 0; i < options.Length; i++) {
          if (i == currentSelection) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(options[i].PadRight(Console.WindowWidth - 1));
            Console.ForegroundColor = ConsoleColor.Gray;
          } else {
            Console.WriteLine(options[i].PadRight(Console.WindowWidth - 1));
          }
        }
      }
    }

    public void ItemShop(){
      Console.Clear();

      Utility util = new Utility(player);
      int itemNav = 0;
      char selected = 'x';
      bool end = false;

      do{
      Console.Clear();

      System.Console.WriteLine("Item Shop");
      if(itemNav == 0){
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Bonus XP - 25 XP");
        Console.ForegroundColor = ConsoleColor.Gray;
        selected = 'b';
      } else {
        System.Console.WriteLine("Bonus XP - 25 XP");
      }

      if(itemNav == 1){
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Damage Boost - 75 XP");
        Console.ForegroundColor = ConsoleColor.Gray;
        selected = 'd';
      } else {
        System.Console.WriteLine("Damage Boost - 75 XP");
      }

      if(itemNav == 2){
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Restore Health - 25XP");
        Console.ForegroundColor = ConsoleColor.Gray;
        selected = 'h';
      } else {
        System.Console.WriteLine("Restore Health - 25XP");
      }

      if(itemNav == 3){
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Increased Max Health - 50XP");
        Console.ForegroundColor = ConsoleColor.Gray;
        selected = 'i';
      } else {
        System.Console.WriteLine("Increased Max Health - 50XP");
      }

      if(itemNav == 4){
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Exit");
        Console.ForegroundColor = ConsoleColor.Gray;
        selected = 'e';
      } else {
        System.Console.WriteLine("Exit");
      }

      // Read input from the player
        ConsoleKey newKey = Console.ReadKey().Key;

        // Navigate up and down the list
        if (newKey == ConsoleKey.S && itemNav < 4){
          itemNav++;
        } else if (newKey == ConsoleKey.W && itemNav > 0) {
          itemNav--;
        } else if (newKey == ConsoleKey.Enter){
          System.Console.WriteLine("test");

          if(selected == 'e'){
          } else {
            switch(selected){
              case 'b':
                if(player.GetXP()<25){
                  Console.Clear();
                  System.Console.WriteLine("You don't have enough XP to purchase this item\n\nPress any key to continue");
                  Console.ReadKey();
                } else {
                  util.AddItem(selected);
                  player.SetXP(player.GetXP()-25);
                }
                break;
              case 'd':
                if(player.GetXP()<75){
                  Console.Clear();
                  System.Console.WriteLine("You don't have enough XP to purchase this item\n\nPress any key to continue");
                  Console.ReadKey();
                } else {
                  util.AddItem(selected);
                  player.SetXP(player.GetXP()-75);
                }
                break;
              case 'h':
                if(player.GetXP()<25){
                  Console.Clear();
                  System.Console.WriteLine("You don't have enough XP to purchase this item\n\nPress any key to continue");
                  Console.ReadKey();
                } else {
                  util.AddItem(selected);
                  player.SetXP(player.GetXP()-25);
                }
                break;
              case 'i':
                if(player.GetXP()<50){
                  Console.Clear();
                  System.Console.WriteLine("You don't have enough XP to purchase this item\n\nPress any key to continue");
                  Console.ReadKey();
                } else {
                  util.AddItem(selected);
                  player.SetXP(player.GetXP()-50);
                }
                break;
            }
          }
          end = true;
        }
      } while(!end);
    }

    public void PrintHome(){
      Console.Clear();
      bool used = false;
      bool homeEnemy = false;

      for (int i = 0; i < 63; i++) {
        for (int j = 0; j < 147; j++) {
          Island(0, 102, 5, i, j, ref used, 63, ref homeEnemy);
          PrintItemShop(i,j, ref used);

          if (!used) {
            System.Console.Write(" ");
          }

          used = false;
        }
        System.Console.WriteLine();
      }

      if(enterLevel == true){
        SwitchOvrRide();
        enterLevel = false;
        System.Console.WriteLine("\nPress Enter to start a game");
        if(Console.ReadKey().Key == ConsoleKey.Enter){
          RandomGame();
          player.SetPos(76);
          SwitchOvrRide();
        } else {
          player.SetPos(76);
        }
      } else {
        Inventory(true);
      }

      if(player.GetPos() == 16 && player.GetItemsCount() != 4){
        Console.Clear();
        System.Console.WriteLine("\nPress ENTER to enter the item shop, press any key to exit");
        SwitchOvrRide();

        if(Console.ReadKey().Key == ConsoleKey.Enter){
          ItemShop();
        }
        
        player.SetPos(19);
      } else if(player.GetPos() == 16 && player.GetItemsCount() == 4){
        Console.Clear();
        System.Console.WriteLine("You have the maximum amount of items\n\nPress any key to continue");
        Console.ReadKey();
        player.SetPos(19);
        SwitchOvrRide();
      } else if(player.GetPos() == 79){
        SwitchOvrRide();
        player.SetPos(91);
        enterLevel = true;
      }
    }

    public void PrintItemShop(int i, int j, ref bool used){
      int height = 63-5;

      if(i==height-1&&j==5 || i==height-2&&j==5 || i==height-3&&j==5 || i==height-1&&j==13 || i==height-2&&j==13 || i==height-3&&j==13){
        System.Console.Write("|");
        used = true;
      } else if(i==height-1&&j==6 || i==height-1&&j==7 || i==height-1&&j==8 || i==height-1&&j==9 || i==height-1&&j==10 || i==height-1&&j==11 || i==height-1&&j==12){
        System.Console.Write("-");
        used = true;
      } else if(i==height-3&&j==6 || i==height-3&&j==7 || i==height-3&&j==8 || i==height-3&&j==9 || i==height-3&&j==10 || i==height-3&&j==11 || i==height-3&&j==12){
        System.Console.Write("-");
        used = true;
      } else if(i==height-2 && j==7){
        System.Console.Write("I");
        used = true;
      } else if(i==height-2 && j==8){
        System.Console.Write("t");
        used = true;
      } else if(i==height-2 && j==9){
        System.Console.Write("e");
        used = true;
      } else if(i==height-2 && j==10){
        System.Console.Write("m");
        used = true;
      } else if(i==height-2 && j==11){
        System.Console.Write("s");
        used = true;
      } else if(i==height-1&&j==88 || i==height-2&&j==88 || i==height-3&&j==88 || i==height-1&&j==94 || i==height-2&&j==94 || i==height-3&&j==94){
        System.Console.Write("|");
        used = true;
      } else if(i==height-3&&j==89 || i==height-3&&j==90 || i==height-3&&j==91 || i==height-3&&j==92 || i==height-3&&j==93){
        System.Console.Write("-");
        used = true;
      } else if(i==height-1&&j==82 || i==height-2&&j==82 || i==height-3&&j==82 || i==height-4&&j==82 || i==height-5&&j==82 ||i==height-6&&j==82 || i==height-1&&j==100 || i==height-2&&j==100 || i==height-3&&j==100 || i==height-4&&j==100 || i==height-5&&j==100 || i==height-6&&j==100){
        System.Console.Write("|");
        used = true;
      } else if(i==height-1&&j>82&&j<88 || i==height-2&&j>82&&j<88 || i==height-3&&j>82&&j<88){
        System.Console.Write("|");
        used = true;
      } else if(i==height-4&&j>82&&j<100 || i==height-5&&j>82&&j<100 || i==height-6&&j>82&&j<100){
        if(i==height-6){
          System.Console.Write("-");
        } else {
          System.Console.Write("|");
        }
        used = true;
      } else if(i==height-1&&j>94&&j<100 || i==height-2&&j>94&&j<100 || i==height-3&&j>94&&j<100){
        System.Console.Write("|");
        used = true;
      }
    }

    public void Home(){
      PlayerFileHandler file = new PlayerFileHandler(player);
      Utility util = new Utility(player);
      bool end = false;

      // Set player back
      player.SetPos(49);
      prev = ConsoleKey.D;
      key = ConsoleKey.D;
      ovrRide = true;
      util.AddLevels();

      ConsoleKey newKey = new ConsoleKey();

      do{
        SetLowerBound(16);
        SetUpperBound(79);
        PrintHome();
        SetAccessInventory(newKey);
        if (ovrRide == false) {
          newKey = Console.ReadKey().Key;
          SetKey(newKey);

          if (GetKey() == ConsoleKey.D && player.GetPos() < upperBound) {
            player.IncPos();
          } else if (GetKey() == ConsoleKey.A && player.GetPos() > lowerBound) {
            player.DecPos();
          } else if(GetKey() == ConsoleKey.X){
            Console.Clear();
            System.Console.WriteLine("Are you sure you want to exit the game, all progress made will be saved (Enter to leave, any key to continue)");

            if(Console.ReadKey().Key == ConsoleKey.Enter){
              file.SaveExistingPlayer();
              end = true;
            }
          }

        } else {
          SwitchOvrRide();
        }
      }while(!end);
    }

    public void RandomGame(){
      Utility util = new Utility(player);
      PlayerFileHandler file = new PlayerFileHandler(player);
      Console.Clear();

      // Variables
      ConsoleKeyInfo key;
      bool end = false;
      ConsoleKey newKey = new ConsoleKey();

      // Reseting Game
      SetPrev(ConsoleKey.D);
      util.ResetItems();
      this.enemy = 0;
      player.SetItemsInUse("");
      SwitchIslands();
      newNum = !newNum;
      randomIslandCount = 0;
      enemyHealth = 50;
      
      
      string[] enemyHead = {"","",""};
      string[] enemyBody = {"","",""};

      string[][] newPrintedEnemy = new string[][] {enemyHead, enemyBody};

      do{
        util.CheckActivatedItems();
        if(switchIslands == true){
          // Island Variables
          player.SetPos(4);
          int island1Stop = RandomStop(3,45);
          int island2Start = RandomStart(island1Stop);
          int island2Stop = RandomStop(island2Start, 30);
          int island3Start = RandomStart(island2Stop);
          int island3Stop = RandomStop(island3Start, 45); 
          bool enemy1 = CreateEnemy();
          bool enemy2 = CreateEnemy();
          bool enemy3 = CreateEnemy();
          bool[] enemy = {enemy1,enemy2,enemy3};
          SetIsEnemy(enemy);
          int[] island1 = {3,island1Stop, RandomHeight()};
          int[] island2 = {island2Start, island2Stop, RandomHeight()};
          int[] island3 = {island3Start, island3Stop, RandomHeight()};
          int[][] current = new int[][]{island1, island2, island3};
          SetCurrentIslands(current);
          SetLowerBound(current[0][0]+1);
          SetUpperBound(current[0][1]-1);
          SwitchIslands();
        }

        RandomGameMap();

        SetAccessInventory(newKey);

        if (ovrRide == false) {
          newKey = Console.ReadKey().Key;
          SetKey(newKey);

          if (GetKey() == ConsoleKey.D && player.GetPos() < upperBound) {
            player.IncPos();
          } else if (GetKey() == ConsoleKey.A && player.GetPos() > lowerBound) {
            player.DecPos();
          } else if(GetKey() == ConsoleKey.X){
            end = true;
          }

        } else {
          SwitchOvrRide();
        }

      }while(!end);

    }

    private void RandomGameMap(){
      Console.Clear();
      RandomIslands();
      Utility util = new Utility(player);

      if(player.GetPos() == currentIslands[0][1] - 1 || player.GetPos() == currentIslands[1][1] - 1 || player.GetPos() == currentIslands[2][1] - 1){
        System.Console.WriteLine("Question");
        if (Console.ReadLine() == "answer") {
          if (player.GetPos() == currentIslands[0][1] - 1) {
            player.SetPos(currentIslands[1][0] + 1);
            SetUpperBound(currentIslands[1][1] - 1);
            newNum = !newNum;
            enemyHealth = 50;
          } else if (player.GetPos() == currentIslands[1][1] - 1) {
            player.SetPos(currentIslands[2][0] + 1);
            SetUpperBound(currentIslands[2][1] - 1);
            newNum = !newNum;
            enemyHealth = 50;
          } else if (player.GetPos() == currentIslands[2][1] - 1) {
            if(randomIslandCount == 0){
              randomIslandCount++;
              SwitchIslands();
              newNum = !newNum;
              enemyHealth = 50;
            } else {
              // Enter boss fight
            }
          }

          SetLowerBound(player.GetPos());
          SwitchOvrRide();
        } else {
          player.SetPos(lowerBound);
          SwitchOvrRide();
        }
      } else if(player.GetPos() == enemy-3 && enemyHealth > 0){
        System.Console.WriteLine("Question");
        if (Console.ReadLine() == "answer") {
          SetEnemyHealth(enemyHealth - player.GetDamage());
          SwitchOvrRide();
        } else {
          player.SetPos(lowerBound);
          SwitchOvrRide();
        }

      }else{
        Inventory();
        System.Console.WriteLine($"Health: {player.GetHealth()}");
        System.Console.WriteLine($"Damage: {player.GetDamage()}");
        System.Console.Write($"Activated Items: ");
        if(!string.IsNullOrEmpty(player.GetItemsInUse())){
          util.PrintActivatedItems();
        } 
      }
    }

    private bool OnIsland(int start, int stop){
      if(player.GetPos() >= start && player.GetPos() <= stop){
        return true;
      } else {
        return false;
      }
    }

    private void RandomIslands(){
      bool used = false;
      for (int i = 0; i < 63; i++) {
        for (int j = 0; j < 147; j++) {
          Island(currentIslands[0][0], currentIslands[0][1], currentIslands[0][2], i, j, ref used, 63, ref isEnemy[0]);
          Island(currentIslands[1][0], currentIslands[1][1], currentIslands[1][2], i, j, ref used, 63, ref isEnemy[1]);
          Island(currentIslands[2][0], currentIslands[2][1], currentIslands[2][2], i, j, ref used, 63, ref isEnemy[2]);

          if(!used){
            System.Console.Write(" ");
          }

          used = false;
        }
        System.Console.WriteLine();
      }  
    }
  }
}