//var assert = require('assert');
var myService = require('./../services/myService');
var Till = require('./../services/till.js');

exports.testHelloWorld = function(test){
  test.expect(1);

  test.equal('Hello World!', myService.helloWorld());
  
  test.done();
};

exports.till = function(test){
  assertProductsTotal(test, [],0);
};

exports.givenAnApple_TheTotalShouldBe30Pence = function(test) {
	assertProductsTotal(test, ["Apple"],30);
};

exports.givenTwoApples_TheTotalShouldBe60Pence = function(test) {
	products = createMany("Apple", 2);
	assertProductsTotal(test, products,60);
};

exports.givenABean_TheTotalShouldBe50Pence = function(test) {
	assertProductsTotal(test, ["Beans"],50);
};

exports.givenACoke_TheTotalShouldBe180Pence = function(test) {
	assertProductsTotal(test, ["Coke"],180);
};

exports.givenACoke_TheTotalShouldBe180Pence = function(test) {
	assertProductsTotal(test, ["Deodorant"],250);
};

exports.givenThreeApples_TheTotalShouldBe90Pence = function(test) {
	products = createMany("Apple", 3);
	assertProductsTotal(test, products, 90);
};

exports.givenFourApples_TheTotalShouldBe100Pence = function(test) {
	products = createMany("Apple", 4);
	assertProductsTotal(test, products,100);
};

exports.givenFiveApples_TheTotalShouldBe100Pence = function(test) {
	products = createMany("Apple", 5);

	assertProductsTotal(test, products,130);
};

exports.givenEightApples_TheTotalShouldBe200Pence = function(test) {
	var products = createMany("Apple", 8);

	assertProductsTotal(test, products,200);
};

exports.integerDivision = function(test) {
	test.expect(1);
	var till = new Till();
	
	test.equal(2,till.integerDivision(10,5));

	test.done();
};

exports.givenAnInvalidProduct = function(test) {
	test.expect(1);

	var till = new Till();
	try{
		till.scan("NotAProductCode");
		test.fail("Exception not thrown");
	} catch (ex){
		test.equal("Invalid product: 'NotAProductCode'", ex.message);
	}	

	test.done();
};

function assertProductsTotal(test, products, expectedTotal){
	test.expect(1);

	var till = new Till();

	products.forEach(function(product){
		till.scan(product);
	});

	test.equal(expectedTotal, till.total());

	test.done();
};

function createMany(productCode, numberToCreate){
	var products = [];

	for(var i = 0 ; i < numberToCreate ;i++){
		products.push(productCode);
	}

	return products;
};