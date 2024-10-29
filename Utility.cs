namespace CGI_Project {
  public class Utility {

    public Utility(){

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
  }
}