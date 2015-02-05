package CheckoutKata

//import "fmt"

type Checkout struct {
	Products []Product
}

func (checkout *Checkout) Scan(product Product) {
	checkout.Products = append(checkout.Products, product)
}

func (checkout *Checkout) GetTotal() float64 {
	discounts := map[string]Discount{
		"Apple":     Discount{"Apple", 4, 0.2},
		"Deodorant": Discount{"Deodorant", 2, 0.5},
	}

	var productsCount = map[string]int16{}

	var total float64 = 0

	for _, product := range checkout.Products {
		productsCount[product.Name] += product.Quantity
		total += product.Price * float64(product.Quantity)
		//fmt.Println("Adding product: " + product.Name)
		//fmt.Println(fmt.Sprintf("Price: %f", product.Price))
		//fmt.Println(fmt.Sprintf("Quantity: %d", product.Quantity))
	}

	for productName, discount := range discounts {
		productCount := productsCount[productName]

		var numberOfDiscountsToApply int16 = productCount / discount.Quantity
		var saving float64 = float64(numberOfDiscountsToApply) * discount.Total
		total -= saving
	}

	return total
}
