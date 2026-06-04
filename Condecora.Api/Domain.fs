module Domain

open System

type TypeMovement =
    | In
    | Out

type Status =
    | Pending
    | Completed
    | Canceled

module Models =

    type CategoryBadge = {
        Id: int
        Name: string
        CreatedAt: DateTime
        UpdatedAt: DateTime
    }

    type Badge = {
        Id: int
        CategoryId: int
        Name: string
        CreatedAt: DateTime
        UpdatedAt: DateTime
    }

    type BadgeStock = {
        BadgeId: int
        Quantity: int
        UpdatedAt: DateTime
    }

    type StockMovement = {
        Id: int
        BadgeId: int
        Quantity: int
        Description: string
        ScheduledFor: DateTime
        TypeMovement: TypeMovement
        Status: Status
        CreatedAt: DateTime
        UpdatedAt: DateTime
    }
