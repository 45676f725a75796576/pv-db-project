# Database edit API

This API is made to simplify editing databases.

### Tutorials:

##### Select:
>   DoOperation(DBOperations.SELECT_TABLE, "example-table");  
>   DoOperation(DBOperations.SELECT_COLUMN_IN_TABLE, "example-column");  
>   DoOperation(DBOperations.GET_ROWS_FROM_COLUMN, "example-value");  
>   DoOperation(DBOperations.EXECUTE, null);

*example-value* can be null, then you will select every *example-column* from *example-table*  

##### Update:
>   DoOperation(DBOperations.SELECT_TABLE, "example-table");  
>   DoOperation(DBOperations.SELECT_COLUMN_IN_TABLE, "example-column");  
>   DoOperation(DBOperations.SET_ROW, "new-value");  
>   DoOperation(DBOperations.SELECT_ROW, "old-value"); <- this can be null but then you will update every record  
>   DoOperation(DBOperations.EXECUTE, null);  
