using System.Runtime.InteropServices;
using CGI_Project;

Menu();

static void Menu()
{
    // Variables
    Player player = new Player();
    Utility utility = new Utility();
    int userNav = 0; // Navigation index
    bool exitLoop = false;

    // Available options
    int[] answers = { 1, 2 }; // 1 - Login, 2 - Create New Account

    // Menu loop allowing multiple attempts to Login or Create Account
    while (!exitLoop)
    {
        Console.Clear();
        System.Console.WriteLine("Press W and S to navigate the menu, Enter to select");

        // Display options with navigation
        DisplayMenuOptions(userNav);

        // Get user input for navigation
        ConsoleKey newKey = Console.ReadKey().Key;

        // Navigate through options
        if (newKey == ConsoleKey.S && userNav < 1) // Down arrow key
        {
            userNav++;
        }
        else if (newKey == ConsoleKey.W && userNav > 0) // Up arrow key
        {
            userNav--;
        }
        else if (newKey == ConsoleKey.Enter) // Enter key to select
        {
            // Handle selection based on navigation
            switch (userNav)
            {
                case 0: // Login
                    if (utility.Login(ref player))
                    {
                        Home(player);  // Proceed to Home if login is successful
                        exitLoop = true; // Exit loop after successful login
                    }
                    break;
                case 1: // Create new account
                    utility.CreatePlayer();  // Proceed to create account
                    break;
            }
        }
    }
}

// Method to display the menu options with the selected option highlighted
static void DisplayMenuOptions(int userNav)
{
    string[] options = {
        "Existing user? Login",
        "Create new account"
    };

    for (int i = 0; i < options.Length; i++)
    {
        if (i == userNav)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(options[i]);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        else
        {
            System.Console.WriteLine(options[i]);
        }
    }
}


static void Home(Player player) {
  bool end = false;
  GameReport game = new GameReport(player);
  Utility util = new Utility(player);
  PlayerFileHandler file = new PlayerFileHandler(player);

  if(player.GetLevel() == 0){
    game.Tutorial();
  }

  game.Home();
}