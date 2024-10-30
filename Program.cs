using System.Runtime.InteropServices;
using CGI_Project;

Menu();
 
static void Menu(){
    // Variables
    Player player = new Player();
    Utility utility = new Utility();
    int userInput = -1;

    // Prompts the user at the start of the game
    while(userInput != 1){
    Console.Clear();
    System.Console.WriteLine("1. Existing user? Login\n2. Create new account");

    try{
        userInput = int.Parse(Console.ReadLine());
    } catch(Exception e){

    }

    int[]answers = {1,2};

    switch(userInput){
        case 1:
            player = utility.Login(); 

            if(player == null) {
                System.Console.WriteLine("Email not in use, please create an account");
            } else {
                System.Console.WriteLine(player.ToFile());
            }
            break;
        case 2:
            utility.CreatePlayer();
            break;
        default:
            utility.IntInvalidInput(ref userInput,answers);
            break;
    }
    }
}