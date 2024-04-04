# Xamarin CRUD Blog Page with PostgreSQL

This simple Xamarin application allows users to perform CRUD (Create, Read, Update, Delete) operations on a blog page. It's built using Xamarin.Forms and integrates with a **PostgreSQL** database for data storage.

## Made with:

- Xamarin.Forms ( ofc ;3 )
- PostgreSQL 16
  - npgsql v7
  - pgAdmin 4

## Step by step guide

1. Install postgreSQL
   https://www.postgresql.org/download/

2. Clone this repo

   ```sh
   git clone git@github.com:Habownia/postgresXamarin.git
   cd postgresXamarin
   ```

3. Import .sql file

   ```sh
   psql -h 127.0.0.1 -p 5432 -U postgres
   ```

   ```sql
   CREATE DATABASE blog;
   \list # you should have blog in our db list
                                                          List of databases
   Name    |  Owner   | Encoding | Locale Provider |          Collate           |           Ctype            | ICU Locale | ICU Rules |   Access privileges
   --------+----------+----------+-----------------+----------------------------+----------------------------+------------+-----------+-----------------------
   blog    | postgres | UTF8     | libc            | Polish_Poland.1250         | Polish_Poland.1250         |            |


   \change blog # change db

   \include 'C:\\{path_to_dir}\\postgres-blog.sql' # Use double slash and single quotes!!!
   ```

4. Run app in VS

Disclaimer: tested only on Windows 11 (😿😿😿) !!!
