namespace CGI_Project
{
    public class GameReport
    {
        private Player player;

        public GameReport(){

        }

        public GameReport(Player player){
            this.player = player;
        }

        public void Tutorial(){
            Console.Clear();

            // Variables
            ConsoleKeyInfo key;
            bool end = false;
            player.SetPos(10);

            StartingScreen();

            do{
                TutorialMap();

                key = Console.ReadKey();

                if(key.Key == ConsoleKey.D){
                    player.IncPos();
                } else if (key.Key == ConsoleKey.A){
                    player.DecPos();
                } else if (key.Key == ConsoleKey.X){
                    end = true;
                }

            }while(!end);
        }

        private void TutorialMap(){
            Console.Clear();

            // Variables
            bool used = false;
            string answer = "";

            for(int i=0;i<60;i++){
                for(int j=0;j<80;j++){
                    Island(10,30,15,i,j,ref used);
                    Island(40,55,10,i,j,ref used);

                    if(!used){
                        System.Console.Write(" ");
                    }  

                    used = false;
                }
                System.Console.WriteLine();
            }
            if(player.GetPos() == 28 || player.GetPos() == 53){
                System.Console.WriteLine("\n\nQuestion");
                answer = Console.ReadLine().ToLower();
            }
        }

        private void Island(int start, int stop, int height, int i, int j, ref bool used){
            height = 60-height;

            if(i == height-1 && j == player.GetPos() && player.GetPos() >= start && player.GetPos() <= stop-2){
                System.Console.Write("-|-");
                used = true;
            } else if(i == height-2 && j == player.GetPos() && player.GetPos() >= start && player.GetPos() <= stop-2){
                System.Console.Write(" > ");
                used = true;
            } else if(i == height && j >= start && j <= stop){
                System.Console.Write("_");
                used = true;
            } else if (i>height && j >= start && j <= stop){
                System.Console.Write("|");
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