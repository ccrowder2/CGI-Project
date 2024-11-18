using System.Security.Cryptography;

namespace CGI_Project {
  public class GameReport {
    private Player player;
    private int upperBound;
    private int lowerBound;
    private ConsoleKey key;
    private ConsoleKey prev;
    private ConsoleKey accessInventory;

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

    private ConsoleKey GetAccessInventory(){
      return accessInventory;
    }

    public void Tutorial() {
      Utility util = new Utility(player);
      Console.Clear();

      // Variables
      ConsoleKeyInfo key;
      bool end = false;
      player.SetPos(11);
      SetLowerBound(11);
      SetUpperBound(29);
      SetPrev(ConsoleKey.D);
      util.AddItem('b');

      StartingScreen();
      TutorialMap();

      do {
        TutorialMap();
        key = Console.ReadKey();
        SetKey(key.Key);

        if (GetKey() == ConsoleKey.D && player.GetPos() < upperBound) {
          player.IncPos();
        } else if (GetKey() == ConsoleKey.A && player.GetPos() > lowerBound) {
          player.DecPos();
        } else if (GetKey() == ConsoleKey.X) {
          end = true;
        }

        TutorialMap();

        SetAccessInventory(key.Key);

      } while (!end);
    }

    private void TutorialMap() {
      Console.Clear();

      // Variables
      string answer = "";

      TutorialIsland();

      if (player.GetPos() == 29 || player.GetPos() == 74 || player.GetPos() == 99) {
        System.Console.WriteLine("\nQuestion");
        answer = Console.ReadLine().ToLower();

        if (answer == "correct") {
          switch (player.GetPos()) {
          case 29:
            player.SetPos(41);
            SetUpperBound(74);
            break;
          case 74:
            player.SetPos(81);
            SetUpperBound(99);
            break;
          case 99:
            player.SetPos(111);
            SetUpperBound(159);
            break;
          }

          SetLowerBound(player.GetPos());
        } else {
          player.SetPos(lowerBound);
        }
      } else {
        Inventory();
      }
    }

    private void Inventory(){
      if(key == ConsoleKey.Enter && accessInventory == ConsoleKey.S){
          if(player.GetItems() != null){
            InventoryList();
          } else {
            Console.Clear();
            System.Console.WriteLine("\nYou have no items in your inventory, you can buy more at your home base\n\nPress any key to continue:");
            Console.ReadKey();
          }
        } else if(key == ConsoleKey.S){
          Console.ForegroundColor = ConsoleColor.Green;
          System.Console.WriteLine("\nInventory");
          Console.ForegroundColor = ConsoleColor.Gray;
        } else{
          System.Console.WriteLine("\nInventory");
        }
    }

    private void InventoryList(){
      Utility util = new Utility(player);
      ConsoleKeyInfo key;
      bool end = false;
      char[] items = player.GetItems();
      int itemNav = 0;
      int selected = -1;

      while(!end){
      Console.Clear();
      System.Console.WriteLine("\nPress S to move down and W to move up, press ENTER to select\n");

      for(int i=0;i<items.Length;i++){
        if(items[i] == 'b' && itemNav == i){
          Console.ForegroundColor = ConsoleColor.Green;
          System.Console.WriteLine("Bonus XP");
          Console.ForegroundColor = ConsoleColor.Gray;
          selected = i;
        } else if(items[i] == 'd' && itemNav == i){
          Console.ForegroundColor = ConsoleColor.Green;
          System.Console.WriteLine("Damage Boost");
          Console.ForegroundColor = ConsoleColor.Gray;
          selected = i;
        } else if(items[i] == 'h' && itemNav == i){
          Console.ForegroundColor = ConsoleColor.Green;
          System.Console.WriteLine("Restore Health");
          Console.ForegroundColor = ConsoleColor.Gray;
          selected = i;
        } else if(items[i] == 'i' && itemNav == i){
          Console.ForegroundColor = ConsoleColor.Green;
          System.Console.WriteLine("Increase Max Health");
          Console.ForegroundColor = ConsoleColor.Gray;
          selected = i;
        } else if(items[i] == 'b'){
          System.Console.WriteLine("Bonus XP");
        } else if(items[i] == 'd'){
          System.Console.WriteLine("Damage Boost");
        } else if(items[i] == 'h'){
          System.Console.WriteLine("Restore Health");
        } else if(items[i] == 'i'){
          System.Console.WriteLine("Increase Max Health");
        } 
      }

        key = Console.ReadKey();
        SetKey(key.Key);

        if (GetKey() == ConsoleKey.S && itemNav < player.GetItemsCount()) {
          itemNav++;
        } else if (GetKey() == ConsoleKey.W && itemNav > 0) {
          itemNav--;
        } else if (GetKey() == ConsoleKey.Enter){
          for(int i=0;i<items.Length;i++){
            if(items[selected] == items[i]){
              util.ActivateItem(items[selected]);
              end = true;
            }
          }
        }
      }
    }

    private void TutorialIsland() {
      Console.Clear();
      bool used = false;
      System.Console.WriteLine(Prompt());

      for (int i = 0; i < 57; i++) {
        for (int j = 0; j < 198; j++) {
          Island(10, 30, 15, i, j, ref used);
          Island(40, 75, 10, i, j, ref used);
          Island(80, 100, 17, i, j, ref used);
          Island(110, 160, 30, i, j, ref used);

          if (!used) {
            System.Console.Write(" ");
          }

          used = false;
        }
        System.Console.WriteLine();
      }
    }

    private void Island(int start, int stop, int height, int i, int j, ref bool used) {
      height = 60 - height;
      bool onIsland = false;

      if (player.GetPos() >= start && player.GetPos() <= stop) {
        onIsland = true;
      }

      if (i == height - 1 && j == player.GetPos() && player.GetPos() > start && player.GetPos() < stop) {
        System.Console.Write("-|-");
        used = true;
      } else if (i == height - 2 && j == player.GetPos() && player.GetPos() > start && player.GetPos() < stop && GetKey() == ConsoleKey.D) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.Write(" 0>");
        Console.ForegroundColor = ConsoleColor.Gray;
        used = true;
      } else if (i == height - 2 && j == player.GetPos() && player.GetPos() > start && player.GetPos() < stop && GetKey() == ConsoleKey.A) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.Write("<0 ");
        Console.ForegroundColor = ConsoleColor.Gray;
        used = true;
      } else if (i == height - 2 && j == player.GetPos() && player.GetPos() > start && player.GetPos() < stop) {
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
        return "3";
      } else if (player.GetPos() >= 110 && player.GetPos() <= 160) {
        return "4";
      }
      return "";
    }
  }
}