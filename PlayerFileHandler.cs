using CGI_Project;

namespace CGI_Project
{
    public class PlayerFileHandler
    {
        private Player player;

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
                }
                
                line=inFile.ReadLine();
            }

            if(player.GetID() == 0){
                player = null;
            }

            inFile.Close();

            return player;
        }
    }
}