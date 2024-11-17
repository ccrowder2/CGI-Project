using System.Data.Common;

namespace CGI_Project
{
    public class Player
    {
        private int iD;
        private string userName;
        private string email;
        private string password;
        private int xP;
        private int level;
        private int position;
        private int health;
        private char[] items;
        private string itemsInUse;
        private int countOfItems;

        public Player(){

        }
        public Player(int iD, string email, string password, string userName, int xP, int level){
            this.iD = iD;
            this.userName = userName;
            this.email = email;
            this.password = password;
            this.xP = xP;
            this.level = level;
        }

        public int GetID(){
            return iD;
        }

        public void SetID(int iD){
            this.iD = iD;
        }

        public string GetUserName(){
            return userName;
        }

        public void SetUserName(string userName){
            this.userName = userName;
        }

        public string GetEmail(){
            return email;
        }

        public void SetEmail(string email){
            this.email = email;
        }

        public string GetPassword(){
            return password;
        }

        public void SetPassword(string password){
            this.password = password;
        }

        public int GetXP(){
            return xP;
        }

        public void SetXP(int xP){
            this.xP = xP;
        }

        public int GetLevel(){
            return xP;
        }

        public void SetLevel(int level){
            this.level = level;
        }

        public int GetPos(){
            return position;
        }

        public void SetPos(int position){
            this.position = position;
        }

        public void IncPos(){
            position += 3;
        }

        public void DecPos(){
            position -= 3;
        }

        public int GetHealth(){
            return health;
        }

        public void SetHeath(int health){
            this.health = health;
        }

        public void SetItems(char[] items){
            this.items = items;
        }

        public char[] GetItems(bool ovrRide = false){
            // b - bonus XP // d - damange boost // h - restore health // i - increased max health
            bool empty = true;
            if(items != null){
                for(int i=0;i<items.Length;i++){
                    if(items[i] == 'b' || items[i] == 'd' || items[i] == 'h' || items[i] == 'i'){
                     empty = false;
                    }
                }
            }

            if(empty == false || ovrRide == true){
                return items;
            } else {
                return null;
            }
        }

        public int GetItemsCount(){
            countOfItems=-1;
            for(int i=0;i<items.Length;i++){
                if(items[i] == 'b' || items[i] == 'd' || items[i] == 'h' || items[i] == 'i'){
                    countOfItems++;
                }
            }
            return countOfItems;
        }

        public void Damage(int damage){
            health -= damage;
        }

        public string GetItemsInUse(){
            return itemsInUse;
        }

        public void SetItemsInUse(string itemsInUse){
            this.itemsInUse = itemsInUse;
        }

        public string ToFile(){
            string writeItems = "";

            if(items != null){
            for(int i =0; i<items.Length;i++){
                if(items[i] == 'b'){
                    writeItems += "b";
                } else if(items[i] == 'd'){
                    writeItems += "d";
                } else if(items[i] == 'h'){
                    writeItems += "h";
                } else if(items[i] == 'i'){
                    writeItems += "i";
                }
            }
            }
            return $"{iD}#{email}#{password}#{userName}#{xP}#{level}#{writeItems}";
        }
    }
}