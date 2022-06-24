using System;

    class Program
    {
        static void Main(string[] args)
        {
            var sqlConnection = new SqlConnection("connectionString");
            var oracleConnection = new OracleConnection("connectionString");
            
            var sqlDbCommand = new DbCommand(sqlConnection,"inst");
            sqlDbCommand.Execute();
            
            var oracleDbCommand = new DbCommand(oracleConnection,"inst");
            oracleDbCommand.Execute();
        }
    }
    public abstract class DbConnection
    {
        public abstract string _connectionString { get; set; }
        public abstract TimeSpan _timeout { get; set; }

        public DbConnection(){}
        public DbConnection(string ConnectionString)
        {   
            if(String.IsNullOrEmpty(ConnectionString))
                throw new Exception("Connection String is null or empty");
            _connectionString = ConnectionString;
            _timeout = new TimeSpan();

        }
        //If the connection have a TimeOut value
        public DbConnection(string ConnectionString, TimeSpan Timeout)
        {
            if(String.IsNullOrEmpty(ConnectionString))
                throw new Exception("_connectionString is null or empty");
            _connectionString = ConnectionString;
            _timeout = Timeout;

        }
        public abstract void Open();
        public abstract void Close();
    }
    public class SqlConnection : DbConnection
    {
        public override string _connectionString { get; set; }
        public override TimeSpan _timeout { get; set; }
        public SqlConnection(){}
        public SqlConnection(string ConnectionString)
        :base(ConnectionString)
        {
        }
        //If the connection have a TimeOut value
        public SqlConnection(string ConnectionString, TimeSpan Timeout)
        :base(ConnectionString, Timeout)
        {
        }

        public override void Open()
        {
            if (_timeout != TimeSpan.Zero)//ıf a timeSpan given
            { //need to check the given value 
                System.Console.WriteLine("in timespan");
                System.Console.WriteLine(_timeout);
                TimeSpan startTime = DateTime.Now.TimeOfDay;
                bool connected = false;
                while((DateTime.Now.TimeOfDay - startTime < _timeout) & !connected) {
                    //do operations to connect the database
                    //if connection is successful bool value connected will be true
                    connected = true;
                    //program will be continued until the end of the timespan
                    //If the connection is unsuccessful at the end of the timespan bool connected will be false
                }
                //we are checking the connected value 
                //If the connected is false, connection could not be opened within the 
                //timeout, an exception will be thrown
                if(!connected)
                    throw new Exception("Could not connected to the database within the timeout");
                System.Console.WriteLine("Connected to the Sql database successfully");    
            }
            else{
                // If there is not a timespan given
                // do operations to connect the database
                System.Console.WriteLine("Connected to the Sql database successfully");  
            }
        }
        public override void Close()
        {   
            //Do close operations
            System.Console.WriteLine("Sql Database closed.");
        }
    }
    public class OracleConnection : DbConnection
    {
        public override string _connectionString { get; set; }
        public override TimeSpan _timeout { get; set; }
       
        public OracleConnection(){}
        public OracleConnection(string ConnectionString)
        :base(ConnectionString)
        {}
      
        //If the connection have a TimeOut value
        public OracleConnection(string ConnectionString, TimeSpan Timeout)
        :base(ConnectionString, Timeout)
        {}

        public override void Open()
        {
            if (_timeout != TimeSpan.Zero)//ıf a timeSpan given
            { //need to check the given value 
                TimeSpan startTime = DateTime.Now.TimeOfDay;
                bool connected = false;
                while((DateTime.Now.TimeOfDay - startTime < _timeout) & !connected) {
                    //do operations to connect the database
                    //if connection is successful bool value connected will be true
                    connected = true;
                    //program will be continued until the end of the timespan
                    //If the connection is unsuccessful at the end of the timespan bool connected will be false
                }
                //we are checking the connected value 
                //If the connected is false, connection could not be opened within the 
                //timeout, an exception will be thrown
                if(!connected)
                    throw new Exception("Could not connected to the Oracle database within the timeout");
                System.Console.WriteLine("Connected to the Oracle database successfully");    
            }
            else{
                // If there is not a timespan given
                // do operations to connect the database
                System.Console.WriteLine("Connected to the Oracle database successfully");  
            }
        }
        public override void Close()
        {
            //Do close operations
            System.Console.WriteLine("Oracle Database closed.");
        }
    }
    public class DbCommand
    {
        private String _instruction;
        private DbConnection _dbConnection;
        public DbCommand(){}

        public DbCommand(DbConnection dbConnection, string instruction){
            if((dbConnection is null) | (String.IsNullOrEmpty(instruction)))
            {
                throw new NullReferenceException("DBConnection or instruction is null");
            }
            _instruction = instruction;
            _dbConnection = dbConnection;
        }
        public void Execute()
        {
            _dbConnection.Open();
            //Run the istruction
            System.Console.WriteLine("Instruction is running on database");
            _dbConnection.Close();
        }

    }

    


