using System.Security.Cryptography;

namespace CGI_Project
{
    public class GameReport
    {
        private Player player;
        private int upperBound;
        private int lowerBound;

        public GameReport(){

        }

        public GameReport(Player player){
            this.player = player;
        }

        public int GetUpperBound(){
            return upperBound;
        }

        public int GetLowerBound(){
            return lowerBound;
        }

        public void SetUpperBound(int bound){
            this.upperBound = bound;
        }

        public void SetLowerBound(int bound){
            this.lowerBound = bound;
        }

        public void Tutorial(){
            Console.Clear();

            // Variables
            ConsoleKeyInfo key;
            bool end = false;
            player.SetPos(11);
            SetLowerBound(11);
            SetUpperBound(29);

            StartingScreen();
            TutorialMap();

            do{
                key = Console.ReadKey();

                if(key.Key == ConsoleKey.D && player.GetPos() < GetUpperBound()){
                    player.IncPos();
                } else if (key.Key == ConsoleKey.A && player.GetPos() > GetLowerBound()){
                    player.DecPos();
                } else if (key.Key == ConsoleKey.X){
                    end = true;
                }

                TutorialMap();

            }while(!end);
        }

        private void TutorialMap(){
            Console.Clear();

            // Variables
            bool used = false;
            string answer = "";

            for(int i=0;i<60;i++){
                for(int j=0;j<198;j++){
                    Island(10,30,15,i,j,ref used);
                    Island(40,75,10,i,j,ref used);
                    Island(80,100,17,i,j,ref used);
                    Island(110,170,30,i,j,ref used);

                    if(!used){
                        System.Console.Write(" ");
                    }  

                    used = false;
                }
                System.Console.WriteLine();
            }
            if(player.GetPos() == 29 || player.GetPos() == 73 || player.GetPos() == 98 || player.GetPos() == 118){
                System.Console.WriteLine("\n\nQuestion");
                answer = Console.ReadLine().ToLower();

                if(answer == "correct"){
                    switch(player.GetPos()){
                        case 28:
                            player.SetPos(41);
                            break;
                        case 73:
                            player.SetPos(81);
                            break;
                        case 98:
                            player.SetPos(111);
                            break;
                        case 118:
                            //player.SetPos();
                            break;
                    }

                    SetLowerBound(player.GetPos());
                } else {
                    player.SetPos(GetLowerBound());
                }

                Console.Clear();
                
                for(int i=0;i<60;i++){
                for(int j=0;j<198;j++){
                    Island(10,30,15,i,j,ref used);
                    Island(40,75,10,i,j,ref used);
                    Island(80,100,17,i,j,ref used);
                    Island(110,170,30,i,j,ref used);

                    if(!used){
                        System.Console.Write(" ");
                    }  

                    used = false;
                }
                System.Console.WriteLine();
            }
            }
        }

        private void Island(int start, int stop, int height, int i, int j, ref bool used){
            height = 60-height;
            bool onIsland = false;

            if(player.GetPos() >= start && player.GetPos() <= stop){
                onIsland = true;
            }

            if(i == height-1 && j == player.GetPos() && player.GetPos() > start && player.GetPos() < stop){
                System.Console.Write("-|-");
                used = true;
            } else if(i == height-2 && j == player.GetPos() && player.GetPos() > start && player.GetPos() < stop){
                System.Console.Write(" > ");
                used = true;
            } else if(i == height && j >= start && j <= stop){
                System.Console.Write("_");
                used = true;
            } else if (i > height && j >= start && j <= stop){
                System.Console.Write("|");
                used = true;
            } else if(i == height-1 && j == player.GetPos()+1 && onIsland == true || i == height-2 && j == player.GetPos()+1 && onIsland == true){
                used = true;
            } else if(i == height-1 && j == player.GetPos()-1 && onIsland == true || i == height-2 && j == player.GetPos()-1 && onIsland == true){
                used = true;
            }
        }

        private void StartingScreen(){
            Console.Clear();
            System.Console.WriteLine("It looks like you're new to the game, here's a tutorial to help you get started\n\nPress any key to continue:");
            Console.ReadKey();
            Console.Clear();
            System.Console.WriteLine("Rules:\n1. In order for the game to print as intended, use an 80x24 terminal\n2. Press the D key to move forward, and the A to move backwards\n3. To interact with an object, stand on it and press the enter key\n\nPress any key to continue:");
            Console.ReadKey();
        }
    }
}