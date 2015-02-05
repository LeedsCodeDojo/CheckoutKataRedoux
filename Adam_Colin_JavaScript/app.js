var Till = require('./services/till');

var till = new Till();
till.scan("Apple");
till.scan("Apple");
till.scan("Beans");
var tillTotal = till.total();

console.log("Till Total: ", tillTotal);