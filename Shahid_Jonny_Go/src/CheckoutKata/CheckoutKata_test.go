package CheckoutKata

import (
	"fmt"
	"testing"
)

func Test_Checkout_ScanApple_Return30p(t *testing.T) {
	var checkout = new(Checkout)

	product := Product{"Apple", 0.30, 1}
	checkout.Scan(product)

	var total float64 = 0
	total = checkout.GetTotal()
	if total != 0.30 {
		t.Error(fmt.Sprintf("%f is not equal to 0.30", total))
	}
}

func Test_Checkout_ScanBeans_Return50p(t *testing.T) {
	var checkout = new(Checkout)

	product := Product{"Beans", 0.50, 1}
	checkout.Scan(product)

	var total float64 = 0
	total = checkout.GetTotal()
	if total != 0.50 {
		t.Error(fmt.Sprintf("%f is not equal to 0.50", total))
	}
}

func Test_Checkout_ScanCoke_Return30p(t *testing.T) {
	var checkout = new(Checkout)

	product := Product{"Coke", 1.80, 1}
	checkout.Scan(product)

	var total float64 = 0
	total = checkout.GetTotal()
	if total != 1.80 {
		t.Error(fmt.Sprintf("%f is not equal to 1.80", total))
	}
}

func Test_Checkout_ScanDeodorant_Return50p(t *testing.T) {
	var checkout = new(Checkout)

	product := Product{"Deodorant", 2.50, 1}
	checkout.Scan(product)

	var total float64 = 0
	total = checkout.GetTotal()
	if total != 2.50 {
		t.Error(fmt.Sprintf("%f is not equal to 2.50", total))
	}
}

func Test_Checkout_Scan2Apples_Return60p(t *testing.T) {
	var checkout = new(Checkout)

	product := Product{"Apple", 0.30, 2}
	checkout.Scan(product)

	var total float64 = 0
	total = checkout.GetTotal()
	if total != 0.60 {
		t.Error(fmt.Sprintf("%f is not equal to 0.60", total))
	}
}

func Test_Checkout_Scan2Apples2Beans_Return160p(t *testing.T) {
	var checkout = new(Checkout)

	productApple := Product{"Apple", 0.30, 2}
	checkout.Scan(productApple)
	productBeans := Product{"Beans", 0.50, 2}
	checkout.Scan(productBeans)

	var total float64 = checkout.GetTotal()
	if total != 1.60 {
		t.Error(fmt.Sprintf("%f is not equal to 1.60", total))
	}
}

func Test_Checkout_Scan4Apples_Return100p(t *testing.T) {
	var checkout = new(Checkout)

	productApple := Product{"Apple", 0.30, 4}
	checkout.Scan(productApple)

	var total float64 = checkout.GetTotal()
	if total != 1.00 {
		t.Error(fmt.Sprintf("%f is not equal to 1.00", total))
	}
}

func Test_Checkout_Scan2Apples2Apples_Return100p(t *testing.T) {
	var checkout = new(Checkout)

	productApple1 := Product{"Apple", 0.30, 2}
	productApple2 := Product{"Apple", 0.30, 2}
	checkout.Scan(productApple1)
	checkout.Scan(productApple2)

	var total float64 = checkout.GetTotal()
	if total != 1.00 {
		t.Error(fmt.Sprintf("%f is not equal to 1.00", total))
	}
}

func Test_Checkout_Scan3Deodorant_Return700p(t *testing.T) {
	var checkout = new(Checkout)

	productDeodorant := Product{"Deodorant", 2.5, 3}
	checkout.Scan(productDeodorant)

	var total float64 = checkout.GetTotal()
	if total != 7.00 {
		t.Error(fmt.Sprintf("%f is not equal to 7.00", total))
	}
}
