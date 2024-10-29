namespace CGI_Project {
  public class Utility {

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

    public void CreatePlayer() {
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
      confirm = "";

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

      while (confirm != "yes") {
        Console.Clear();
        System.Console.WriteLine("Please enter your new username: ");
        userName = Console.ReadLine();
        Console.Clear();
        System.Console.WriteLine($"Your new username will be {userName}. Type yes to confirm:");
        confirm = Console.ReadLine();
      }
      confirm = "";

      Player player = new Player(iD, userName, email, password, xP, level);

      System.Console.WriteLine(player.ToFile());
    }
  }
}