# dotNetBookStore Project
## This is a .NET WebApi project that contains ORM, EFCore, Middlewares, DI, Services usages.
### In this peroject, there are 3 different operations available:
#### 1. AuthorOperations
#### 2. BookOperations
#### 3. GenreOperations
###  Operations are connected with other. Each of them includes CRUD functions with given restrictions:
#### A) New author can be added if it is not available in the database
#### B) Author cannot be deleted if the author has at least 1 book in the database
#### C) Author information can be updated.
#### D) A new book can be added if not in the database. If the author of new book is not available in the database, program automatically adds that author to the database.
#### E) Book details include Genre Type which takes from GenreOperations.
