# Database edit API

This API is made to simplify editing database autopujcovna, which script to create and insert test data is in repository.

### Tutorials:

#### Setting up:
Open Microsoft SQL Server Management Studio.  
Create new query.  
Copy script.

#### What is in Program.cs
Test commands to check if code is working.

#### How it works:
Every object of database has its own class and everyone has same methods.
###### IDBObject:
Interface for every table.
Create new table in MSSMS and to modify it create new class, that will inherit this interface.  
There is also small information about what each method should do.
###### Add()
Doesn't work properly with objects where is non key attribute.
Adds new record and sets input to selected column.
###### DeleteRecord()
Deletes every record, where selected column is equal to filter.
###### GetColumn()
Returns column data as array of strings.
###### FindByColumn()
Returns every record, where selected column is equal to filter.
###### Update()
Sets new_value to selected column in every record where f_column_name is equal to filter.
