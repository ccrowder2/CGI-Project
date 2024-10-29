using CGI_Project;

namespace CGI_Project {
  public class Utility {

    public void Pause() {
      System.Console.WriteLine("\nPress any key to continue:");
      Console.ReadKey();
    }

    // Makes user re enter string if it's an invalid input
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

    // Makes user re enter int if it's an invalid input
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
  }
}