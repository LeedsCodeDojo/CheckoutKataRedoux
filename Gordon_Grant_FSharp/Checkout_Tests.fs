module Checkout_Tests

open Xunit
open FsUnit.Xunit
open Checkout

// Apple 0.30  (discount 4 for 1.00)
// Beans 0.50
// Coke  1.80
// Deodorant 2.50 (discount 2 for 4.50)

let prices = 
    Map.empty
        .Add(Apple, 0.3)
        .Add(Beans, 0.5)
        .Add(Coke, 1.8)
        .Add(Deodorant, 2.5)

let appleDiscountedPrice = 1.0
let deodorantDiscountedPrice = 4.5

let discounts = 
    Map.empty
        .Add([Apple; Apple; Apple; Apple], appleDiscountedPrice)
        .Add([Deodorant; Deodorant], deodorantDiscountedPrice)

[<Fact>]
let ``Single item gives correct price`` () = 
    checkout [Apple] prices discounts |> should equal prices.[Apple]
    checkout [Beans] prices discounts |> should equal prices.[Beans]
    checkout [Coke] prices discounts |> should equal prices.[Coke]
    checkout [Deodorant] prices discounts |> should equal prices.[Deodorant]

[<Fact>]
let ``Multiple single items give correct price`` () = 
    checkout [Apple; Beans] prices discounts |> should equal (prices.[Apple] + prices.[Beans])

[<Fact>]
let ``Apple discount works`` () = 
    checkout [Apple; Apple; Apple; Apple] prices discounts |> should equal appleDiscountedPrice

[<Fact>]
let ``Deodorant discount works`` () = 
    checkout [Deodorant; Deodorant] prices discounts |> should equal deodorantDiscountedPrice

[<Fact>]
let ``Mix of discounted and non-discounted single product gives correct price`` () = 
    checkout [Apple; Apple; Apple; Apple; Apple] prices discounts |> should equal (prices.[Apple] + appleDiscountedPrice)