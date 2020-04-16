namespace HashWithSalt
{
    class User
    {
        //Lazy model -- no abstraction
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + "\nUsername: " + Username + "\nPassword: " + Password + "\n\n";
        }
    }
}
