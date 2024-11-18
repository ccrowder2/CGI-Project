using CGI_Project;

namespace CGI_Project
{
    public class PlayerFileHandler
    {
        private Player player;
        private const int MAX_ITEMS = 5;
        private const int MAX_PLAYERS = 500;

        public PlayerFileHandler(){

        }

        public PlayerFileHandler(Player player){
            this.player = player;
        }

        public int NumberOfPlayers(){
            StreamReader inFile = new StreamReader("Players.txt");
            int count = 0;
            string line = inFile.ReadLine();
            while(line != null){
                count++;
                line = inFile.ReadLine();
            }

            inFile.Close();

            return count;
        }

        public bool EmailInUse(){
            bool rValue = false;
            StreamReader inFile = new StreamReader("Players.txt");

            string line = inFile.ReadLine();
            while(line != null){
                string[] temp = line.Split('#');
                string email= temp[1];
                
                if(player.GetEmail() == email){
                    rValue = true;
                }
                line=inFile.ReadLine();
            }

            inFile.Close();

            return rValue;
        }

        public bool UsernameInUse(){
            bool rValue = false;
            StreamReader inFile = new StreamReader("Players.txt");

            string line = inFile.ReadLine();
            while(line != null){
                string[] temp = line.Split('#');
                string userName= temp[3];
                
                if(player.GetUserName() == userName){
                    rValue = true;
                }
                line=inFile.ReadLine();
            }

            inFile.Close();

            return rValue;
        }
        
        public void SavePlayer(){
            using (StreamWriter outFile = new StreamWriter("Players.txt", append: true))
            {
                outFile.WriteLine(player.ToFile());
            }
        }

        public void SaveExistingPlayer(){
            // Read In All Players
            StreamReader inFile = new StreamReader("Players.txt");
            Player[] players = new Player[MAX_PLAYERS];
            int count = 0;

            string line = inFile.ReadLine();
            while(line != null){
                Utility util = new Utility(players[count]);
                string[] temp = line.Split('#');
                players[count] = new Player(int.Parse(temp[0]), temp[1], temp[2], temp[3], int.Parse(temp[4]), int.Parse(temp[5]));

                string itemList = temp[6];

                for(int i=0;i<itemList.Length;i++){
                    util.AddItem(itemList[i]);  
                }
                
                count++;
                line=inFile.ReadLine();
            }
            inFile.Close();

            // Write Out All Players
            StreamWriter outFile = new StreamWriter("Players.txt");
            if(player.GetItems() != null){
            char[] items = player.GetItems();
            for(int i=0; i<player.GetItemsCount();i++){
                System.Console.WriteLine(items[i]);
            }
            }

            
            for(int i=0;i<count;i++){
                outFile.WriteLine(players[i].ToFile());
            }
            outFile.Close();
        }

        public Player FindPlayerByEmail(string email){
            Player player = new Player();
            StreamReader inFile = new StreamReader("Players.txt");

            string line = inFile.ReadLine();
            while(line != null){
                string[] temp = line.Split('#');

                if(email == temp[1]){
                    player.SetID(int.Parse(temp[0]));
                    player.SetEmail(temp[1]);
                    player.SetPassword(temp[2]);
                    player.SetUserName(temp[3]);
                    player.SetXP(int.Parse(temp[4]));
                    player.SetLevel(int.Parse(temp[5]));

                    if(AddInventory(email) != null){
                        player.SetItems(AddInventory(email));
                    }
                }
                
                line=inFile.ReadLine();
            }

            if(player.GetID() == 0){
                player = null;
            }

            inFile.Close();

            return player;
        }

        public bool CheckPassword(string password){
            bool rValue = false;
            StreamReader inFile = new StreamReader("Players.txt");

            string line = inFile.ReadLine();
            while(line != null){
                string[] temp = line.Split('#');
                string userName = temp[3];
                
                if(player.GetPassword() == password){
                    rValue = true;
                }
                line=inFile.ReadLine();
            }

            inFile.Close();

            return rValue;
        }

        public char[] AddInventory(string email){
            StreamReader inFile = new StreamReader("Players.txt");
            char[] inventory = new char[MAX_ITEMS];

            string line = inFile.ReadLine();
            while(line != null){
                string[] temp = line.Split('#');
                string items = temp[6];

                if(email == temp[1]){
                    for(int i=0;i<items.Length;i++){
                        inventory[i] = items[i];
                    }
                }
                
                line=inFile.ReadLine();
        }

        return inventory;
        }
    }
}