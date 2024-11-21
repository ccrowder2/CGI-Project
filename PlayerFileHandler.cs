using CGI_Project;

namespace CGI_Project {
  public class PlayerFileHandler {
    private Player player;
    private
    const int MAX_ITEMS = 5;
    private
    const int MAX_PLAYERS = 500;

    public PlayerFileHandler() {

    }

    public PlayerFileHandler(Player player) {
      this.player = player;
    }

    public int NumberOfPlayers() {
      StreamReader inFile = new StreamReader("Players.txt");
      int count = 0;
      string line = inFile.ReadLine();
      while (line != null) {
        count++;
        line = inFile.ReadLine();
      }

      inFile.Close();

      return count;
    }

    public bool EmailInUse() {
      bool rValue = false;
      StreamReader inFile = new StreamReader("Players.txt");

      string line = inFile.ReadLine();
      while (line != null) {
        string[] temp = line.Split('#');
        string email = temp[1];

        if (player.GetEmail() == email) {
          rValue = true;
        }
        line = inFile.ReadLine();
      }

      inFile.Close();

      return rValue;
    }

    public bool UsernameInUse() {
      bool rValue = false;
      StreamReader inFile = new StreamReader("Players.txt");

      string line = inFile.ReadLine();
      while (line != null) {
        string[] temp = line.Split('#');
        string userName = temp[3];

        if (player.GetUserName() == userName) {
          rValue = true;
        }
        line = inFile.ReadLine();
      }

      inFile.Close();

      return rValue;
    }

    public void SavePlayer() {
      using(StreamWriter outFile = new StreamWriter("Players.txt", append: true)) {
        outFile.WriteLine(player.ToFile());
      }
    }

    public void SaveExistingPlayer() {
      // Read all players from the file
      StreamReader inFile = new StreamReader("Players.txt");
      Player[] players = new Player[MAX_PLAYERS];
      int count = 0;

      string line = inFile.ReadLine();
      while (line != null) {
        string[] temp = line.Split('#');

        // Create a new Player object from the line data
        players[count] = new Player(
          int.Parse(temp[0]), // ID
          temp[1], // Email
          temp[2], // Password
          temp[3], // Username
          int.Parse(temp[4]), // XP
          int.Parse(temp[5]) // Level
        );

        // Parse inventory items
        if (temp.Length > 6 && !string.IsNullOrEmpty(temp[6])) {
          char[] items = temp[6].ToCharArray();
          players[count].SetItems(items);
        }

        count++;
        line = inFile.ReadLine();
      }
      inFile.Close();

      // Update the player's data in the array
      for (int i = 0; i < count; i++) {
        if (players[i].GetID() == player.GetID()) {
          players[i] = player; // Replace with the updated player object
        }
      }

      // Write all players back to the file
      StreamWriter outFile = new StreamWriter("Players.txt");
      for (int i = 0; i < count; i++) {
        outFile.WriteLine(players[i].ToFile());
      }
      outFile.Close();
    }

    public Player FindPlayerByEmail(string email) {
      Player player = new Player();
      StreamReader inFile = new StreamReader("Players.txt");

      string line = inFile.ReadLine();
      while (line != null) {
        string[] temp = line.Split('#');

        if (email == temp[1]) {
          player.SetID(int.Parse(temp[0]));
          player.SetEmail(temp[1]);
          player.SetPassword(temp[2]);
          player.SetUserName(temp[3]);
          player.SetXP(int.Parse(temp[4]));
          player.SetLevel(int.Parse(temp[5]));

          if (AddInventory(email) != null) {
            player.SetItems(AddInventory(email));
          }
        }

        line = inFile.ReadLine();
      }

      if (player.GetID() == 0) {
        player = null;
      }

      inFile.Close();

      return player;
    }

    public bool CheckPassword(string password) {
      bool rValue = false;
      StreamReader inFile = new StreamReader("Players.txt");

      string line = inFile.ReadLine();
      while (line != null) {
        string[] temp = line.Split('#');
        string userName = temp[3];

        if (player.GetPassword() == password) {
          rValue = true;
        }
        line = inFile.ReadLine();
      }

      inFile.Close();

      return rValue;
    }

    public char[] AddInventory(string email) {
      StreamReader inFile = new StreamReader("Players.txt");
      char[] inventory = new char[MAX_ITEMS];

      string line = inFile.ReadLine();
      while (line != null) {
        string[] temp = line.Split('#');
        string items = temp[6];

        if (email == temp[1]) {
          for (int i = 0; i < items.Length; i++) {
            inventory[i] = items[i];
          }
        }

        line = inFile.ReadLine();
      }
      inFile.Close();

      return inventory;
    }

    public string[] Question(string difficulty){
      StreamReader inFile = new StreamReader("Questions.txt");
      string[] question = new string[6];

      Random rnd = new Random();
      int num = 0;
      int count = 0;

      if(difficulty == "easy"){
        num = rnd.Next(0,46);
      } else if(difficulty == "medium"){
        num = rnd.Next(46,81);
      } else if(difficulty == "hard"){
        num = rnd.Next(81,108);
      }

      string line = inFile.ReadLine();

      while(line != null){
        string[] temp = line.Split('#');
        if(count == num){
          // Question
          question[0] = temp[1];
          // Answer
          question[1] = temp[2];
          // Option1
          question[2] = temp[3];
          // Option2
          question[3] = temp[4];
          // Option3
          question[4] = temp[5];
          // Option4
          question[5] = temp[6];
        }

        count++;
        line = inFile.ReadLine();
      }

      return question;
    }
  }
}