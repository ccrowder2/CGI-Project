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
        
        public void SavePlayer(){
            using (StreamWriter outFile = new StreamWriter("Players.txt", append: true))
            {
                outFile.WriteLine(player.ToFile());
            }
        }
    }
}