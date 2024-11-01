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
        static private int count = 1;

        public Player(){

        }
        public Player(int iD, string userName, string email, string password, int xP, int level){
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

        public void SetCount(int count){
            Player.count = count;
        }
        public void IncCount(){
            count++;
        }

        public int GetCount(){
            return count;
        }

        public string ToFile(){
            return $"{iD}#{email}#{password}#{userName}#{xP}#{level}";
        }
    }
}