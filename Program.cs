using CGI_Project;

Menu();

static void Menu(){
    // Variables
    Utility utility = new Utility();
    int userInput = -1;

    // Prompts the user at the start of the game
    System.Console.WriteLine("1. Existing user? Login\n2. Create new account");

    try{
        userInput = int.Parse(Console.ReadLine());
    } catch(Exception e){

    }

    int[]answers = {1,2};

    switch(userInput){
        case 1:
            System.Console.WriteLine("C1 successful");
            break;
        case 2:
            System.Console.WriteLine("c2 successful");
            break;
        default:
            utility.IntInvalidInput(ref userInput,answers);
            break;
    }
}