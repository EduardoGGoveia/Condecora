module Infrastructure

open System
open System.Data
open Npgsql
open Dapper
open Domain

type StatusHandler() =
    inherit SqlMapper.TypeHandler<Status>()
    override _.SetValue (parameter, value) = 
        let statusValue =
            match value with
            | Status.Pending -> "PENDING"
            | Status.Completed -> "COMPLETED"
            | Status.Canceled -> "CANCELED"
        parameter.Value <- statusValue
        
    override _.Parse (value: obj): Status = 
        match value.ToString() with
        | "PENDING" -> Pending
        | "COMPLETED" -> Completed
        | "CANCELED" -> Canceled
        | str -> failwith $"Status desconhecido: {str}"

type TypeMovementHandler() =
    inherit SqlMapper.TypeHandler<TypeMovement>()

    override _.SetValue (parameter: Data.IDbDataParameter, value: TypeMovement): unit = 
            let typeMovement =
                match value with
                | In -> "IN"
                | Out -> "OUT"
            parameter.Value <- typeMovement

    override _.Parse (value: obj): TypeMovement = 
            match value.ToString() with
            | "IN" -> In
            | "OUT" -> Out
            | str -> failwith $"Tipo de movimento desconhecido: {str}"


let registerDapperHandlers () =
    SqlMapper.AddTypeHandler(StatusHandler())
    SqlMapper.AddTypeHandler(TypeMovementHandler())

    DefaultTypeMap.MatchNamesWithUnderscores <- true

let getDbConnection (connectionString: string) : IDbConnection =
    new NpgsqlConnection(connectionString)

module Repository =
    let allCategoriesAsync (connectionString: string) =
        task {
            use conn = getDbConnection connectionString

            let query = "SELECT * FROM category_badge;"

            let! result = 
                conn.QueryAsync<Models.CategoryBadge>(query)

            return result
        }

    let categoryAsync (connString: string) =
        task {
            use conn = getDbConnection connString

            let query = "SELECT * FROM category_badge AS cb WHERE cb.id = @;"
        }