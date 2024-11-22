using CGI_Project;

namespace CGI_Project {
  public class Utility {

    private Player player;
    private const int MAX_ITEMS = 5;
    public Utility(){

    }

    public Utility(Player player){
      this.player = player;
    }

    public void Pause() {
      System.Console.WriteLine("\nPress any key to continue:");
      Console.ReadKey();
    }

    public void StringInvalidInput(ref string userInput, string[] answers) {

      bool endLoop = false;

      for (int i = 0; i < answers.Length; i++) {
        if (userInput.ToLower() == answers[i].ToLower()) {
          endLoop = true;
        }
      }

      while (!endLoop) {
        System.Console.WriteLine("Invalid Input, please choose a correct option: ");
        userInput = Console.ReadLine().ToLower();

        for (int i = 0; i < answers.Length; i++) {
          if (userInput.ToLower() == answers[i].ToLower()) {
            endLoop = true;
          }
        }
      }
    }

    public void IntInvalidInput(ref int userInput, int[] answers) {

      bool endLoop = false;

      for (int i = 0; i < answers.Length; i++) {
        if (userInput == answers[i]) {
          endLoop = true;
        }
      }

      while (!endLoop) {
        System.Console.WriteLine("Invalid Input, please choose a correct option: ");

        try {
          userInput = int.Parse(Console.ReadLine());
        } catch (Exception e) {

        }

        for (int i = 0; i < answers.Length; i++) {
          if (userInput == answers[i]) {
            endLoop = true;
          }
        }
      }
    }

    // Creates a new player
    public void CreatePlayer() {
      Player player = new Player();
      PlayerFileHandler file = new PlayerFileHandler(player);
      // Variables
      string confirm = "";
      int iD = 0;
      string userName = "";
      string password = "";
      string reTypePassword = "";
      string email = "";
      int xP = 0;
      int level = 0;

      while (confirm != "yes") {
        Console.Clear();
        System.Console.WriteLine("Please enter your email: ");
        email = Console.ReadLine();
        Console.Clear();
        System.Console.WriteLine($"Your email is {email}. Type yes to confirm:");
        confirm = Console.ReadLine();
      }
      player.SetEmail(email);
      if (file.EmailInUse() == true) {
        Console.Clear();
        System.Console.WriteLine("Email is already in use, please select the login option.");
        Pause();
      } else {
        confirm = "";
        player.SetID(file.NumberOfPlayers() + 1);

        while (confirm != "yes") {
          Console.Clear();
          System.Console.WriteLine("Please enter your password: ");
          password = Console.ReadLine();
          Console.Clear();
          System.Console.WriteLine($"Please rewrite your password to confirm");
          reTypePassword = Console.ReadLine();

          if (reTypePassword == password) {
            confirm = "yes";
          } else {
            System.Console.WriteLine("Passwords don't match, please try again");
            Pause();
          }
        }
        confirm = "";
        player.SetPassword(password);

        while (confirm != "yes") {
          Console.Clear();
          System.Console.WriteLine("Please enter your new username: ");
          userName = Console.ReadLine();
          player.SetUserName(userName);

          if(file.UsernameInUse() == true){
            Console.Clear();
              System.Console.WriteLine($"The username {userName} is already in use");
              Pause();
          } else {
            Console.Clear();
            System.Console.WriteLine($"Your new username will be {userName}. Type yes to confirm:");
            confirm = Console.ReadLine();
          }
        }
        confirm = "";

        file.SavePlayer();
      }
    }

    public bool Login(ref Player player){
      Console.Clear();
      PlayerFileHandler file = new PlayerFileHandler();
      string email = "";
      
      System.Console.WriteLine("Please enter your email:");
      email = Console.ReadLine();

      player = file.FindPlayerByEmail(email);

      if(player == null){
        Console.Clear();
        System.Console.WriteLine("Email not in use, please create an account");
        Pause();
      } else {
        if(EnterPassword(player) == true){
          return true;
        }
      }

      return false;
    }

    public bool EnterPassword(Player player){
      PlayerFileHandler file = new PlayerFileHandler(player);
      Console.Clear();
      string password = "";
      int count = 2;
      bool condMet = false;
        System.Console.WriteLine("Please enter your password:");
        password = Console.ReadLine();

        if(file.CheckPassword(password) == true){
          return true;
        } else {
          while(count > 0 && condMet == false){
            Console.Clear();
            System.Console.WriteLine($"Incorrect password, you have {count} attempt(s) left");
            Pause();
            Console.Clear();
            System.Console.WriteLine("Please enter your password:");
            password = Console.ReadLine();

            if(file.CheckPassword(password) == true){
              condMet = true;
              return true;
            }
            count--;
          }
          Console.Clear();
          System.Console.WriteLine("You've reached the maximum amount of password attempts, you will now be sent back to the menu.");
          Pause();
        }
      return false;
    } 

    public void AddItem(char item) {
    if (player == null) {
        Console.WriteLine("");
        return;
    } else if(player.GetItemsCount() == 4){
      return;
    }

    if (player.GetItems() == null) {
        char[] newArray = new char[MAX_ITEMS];
        newArray[0] = item;
        player.SetItems(newArray);
    } else {
        char[] items = player.GetItems();
        int itemCount = player.GetItemsCount();

        // Check if the inventory is full
        if (itemCount >= MAX_ITEMS-1) {
            Console.WriteLine("Inventory is full. Cannot add more items.");
            return;
        }

        items[itemCount+1] = item;
        player.SetItems(items);
    }

    SortInventory();
}


    public void ActivateItem(char item){
      char[] items = player.GetItems();
      string itemsInUse = player.GetItemsInUse();
      bool exit = false;
      int count = 0;
      while(!exit && items != null){
        if(items[count] == item){
          itemsInUse+=item;
          player.SetItemsInUse(itemsInUse);
          exit = true;
        }

        count++;
      }
      
      if(items != null){
        RemoveItems(item);
      }
    }  

    private void RemoveItems(char item){
      char[] items = player.GetItems();
      int count = 0;
      bool exit = false;

      while(!exit){
        if(items[count] == item){
          items[count] -= item;
          exit = true;
        }
        count++;
      }
    }

    public void SortInventory(){
      char[] items = player.GetItems();
      for(int i=0;i<items.Length-1;i++){
        for(int j=i+1;j<items.Length;j++){
          if(items[i].CompareTo(items[j]) < 0){
            Swap(i, j);
          }
        }
      }
    }
    

    private void Swap(int i, int j){
      char[] items = player.GetItems();

      if(player.GetItems() != null){
        char temp = items[i];
        items[i] = items[j];
        items[j] = temp;

        player.SetItems(items);
      }
    }

    public void CheckActivatedItems(){
      if(player.GetItemsInUse() != null){
      string items = player.GetItemsInUse();
      
      for(int i=0;i<items.Length;i++){
        if(items[i] == 'd'){
          player.SetDamage(50);
        } else if(items[i] == 'h'){
           player.SetHealth(100);
          } else if(items[i] == 'i'){
            player.SetHealth(150);
         } else if(items[i] == 'b'){
          player.SetXpToEarn(150);
         }
        }
      }
    }

    public void PrintActivatedItems(){
      if(!string.IsNullOrEmpty(player.GetItemsInUse())){
        string items = player.GetItemsInUse();

        for(int i=0;i<items.Length;i++){
          if(items[i] == 'd'){
          System.Console.Write("Damage Boost");
          } else if(items[i] == 'i'){
          System.Console.Write("Increased Max Health");
         } else if(items[i] == 'b'){
          System.Console.Write("Bonus XP");
         }

         if(i != items.Length-1 && items[i] != 'h'){
          System.Console.Write(", ");
         }
        }
      } else {
        System.Console.Write("No active items");
      }
    }

    public void ResetItems(){
      player.SetDamage(25);
      player.SetHealth(100);
      player.SetXpToEarn(100);
      player.SetItemsInUse("");
    }
  }
}