module Checkout

type Product = Apple | Beans | Coke | Deodorant

let checkout (items:Product list) (prices:Map<Product,float>) (discounts:Map<Product list,float>) = 
    
    let (undiscountedItems, discountsTotal) =
        discounts 
        |> Map.fold (fun agg key value -> 
                let remainingItems = (fst agg)
                let discountProducts = key |> List.toArray
                let discountedProductCount = key |> List.length

                let discountFound = 
                    (remainingItems |> Array.length >= discountedProductCount)
                    && (remainingItems.[0..discountedProductCount-1] = discountProducts)

                match discountFound with
                | true -> (remainingItems.[discountedProductCount..]),(snd agg + value)
                | _ -> agg)
            (items |> List.toArray,0.0)

    discountsTotal + (undiscountedItems |> Array.sumBy (fun item -> prices.[item]))
