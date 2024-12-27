CREATE TABLE PaginatedData (
   Id INT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing primary key
   Data NVARCHAR(MAX) NULL,          -- Allows for large text data and nullable
   Created DATETIME NOT NULL,        -- Non-nullable datetime field
   Updated DATETIME NULL         -- Non-nullable datetime field
);