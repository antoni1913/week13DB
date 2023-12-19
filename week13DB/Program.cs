using System.Data.SQLite;

//AddCustumer(CreateConnection());
ReadData(CreateConnection());
RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{

    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version=3; New=True; Compress=True;");

    try
    {
        connection.Open();
        Console.WriteLine("Conection established");
    }
    catch
    {
        Console.WriteLine("DB connection failed");
    }return connection;

}

static void ReadData(SQLiteConnection myConection)
{

    SQLiteDataReader read;
    SQLiteCommand command;

    command = myConection.CreateCommand();
    command.CommandText = "SELECT * FROM customer";
    read = command.ExecuteReader();

    while (read.Read())
    {
        string fName = read.GetString(0);
        string lName = read.GetString(1);
        string dob = read.GetString(2);

        Console.WriteLine($"full name: {fName} {lName}; dob is {dob} ");
     }
    myConection.Close();
}

static void AddCustumer (SQLiteConnection myConection)
{
    SQLiteCommand command;

    string fName = "Harry";
    string lName = "Potter";
    string dob = "12-13-1999";

    command = myConection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) VALUES('{fName}','{lName}','{dob}')";
    int rowInserted = command.ExecuteNonQuery();

    Console.Clear();
    Console.WriteLine($"Rows inserted: {rowInserted}");

    ReadData(myConection);

    myConection.Close();
}

static void RemoveCustomer(SQLiteConnection myConection)
{
    SQLiteCommand command;

    string idToDelete = "8";

    command = myConection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";

    int rowDeleted = command.ExecuteNonQuery();
    
    Console.Clear();
    Console.WriteLine($"Row deleted: {rowDeleted}");

    ReadData(myConection);

}
