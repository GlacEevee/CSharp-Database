var db = new Database();

db.CreateTable("users", "id", "name", "age");
db.Insert("users", "1", "Alice", "30");
db.Insert("users", "2", "Bob", "25");

db.SelectAll("users");
