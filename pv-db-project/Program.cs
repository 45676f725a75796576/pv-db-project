using CsharpDatabaseAPI.DatabaseObjects;
using pv_db_project;


using (DBManager manager = new DBManager("localhost", "autopujcovna", "pracovna", "pracovna"))
{
    foreach (var value in Model.GetColumn("typ", manager.Connection)) Console.WriteLine(value);
    foreach (var value in Model.FindByColumn("znacka", "BMW", manager.Connection)) Console.WriteLine(value);

    Pojistovna.Add(null, "CSOB", manager.Connection);
    foreach (var value in Pojistovna.GetColumn("nazev", manager.Connection)) Console.WriteLine(value);
    Vuz.Update("spz", "5IJ7890", "pojistovna_id", "2", manager.Connection);
    foreach (var value in Vuz.FindByColumn("spz", "5IJ7890", manager.Connection)) Console.WriteLine(value);
    Vuz.Update("spz", "5IJ7890", "pojistovna_id", "5", manager.Connection);
    Pojistovna.DeleteRecord("nazev", "CSOB", manager.Connection);
    foreach (var value in Vuz.FindByColumn("spz", "5IJ7890", manager.Connection)) Console.WriteLine(value);

    manager.ExportDatabaseToJson("autopujcovna.json");
    manager.ExportDatabaseToCsv("autopujcovna.csv");
}