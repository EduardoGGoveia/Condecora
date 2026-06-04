module Apis

open System
open System.Threading.Tasks
open Microsoft.Extensions.Configuration
open Microsoft.AspNetCore.Http
open Domain
open Infrastructure
open Giraffe

module Categories =
    let getAllCategories =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let connString = ctx.GetService<IConfiguration>()
                let! result = Repository.allCategoriesAsync(connString.GetConnectionString("DefaultConnection"))

                return! json result next ctx
            }

    let getCategory =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let connString = ctx.GetService<IConfiguration>()
                
            }             
