open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Apis
open Giraffe

let webApp =
    choose [
        route "/" >=> text "Bem-vindo à API Condecora!"
        route "/ping" >=> text "Pong!"
        route "/categories" >=> Categories.getAllCategories 
    ]

let configureApp (app: IApplicationBuilder) =
    app.UseGiraffe webApp

let configureServices (services: IServiceCollection) =
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =

    Infrastructure.registerDapperHandlers()

    let builder = WebApplication.CreateBuilder()
    configureServices builder.Services

    let app = builder.Build()
    configureApp app

    app.Run()

    0