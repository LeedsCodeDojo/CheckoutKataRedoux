var Till = function(){
	var COST_OF_4_APPLES = 100;
	var currentTotal = 0;
	var numberOfApples = 0;

	var productCosts = {
		"Apple" : 30,
		"Beans" : 50,
		"Coke" : 180,
		"Deodorant" : 250,
	};

	var applesCostTotal = function(){
		var numberOfApplesInBatch = 4;
		
		var batchesOf4Apples = Math.floor(numberOfApples / numberOfApplesInBatch);
		var numberOfSingleApples = numberOfApples - (batchesOf4Apples * numberOfApplesInBatch);
		
		var totalAppleCost = batchesOf4Apples * COST_OF_4_APPLES;
		return totalAppleCost + (numberOfSingleApples * productCosts["Apple"]);
	};

	this.total = function(){
		return currentTotal + applesCostTotal();
	};

	this.scan = function(product){
		var productCost = productCosts[product];
		if (productCost === undefined) {
			throw new Error("Invalid product: '" + product + "'");
		}
		if ("Apple" === product) {
			numberOfApples++;
		} else {
			currentTotal += productCost;
		}
	};

	this.integerDivision = function(a, b){
		return Math.floor(a / b);
	}
};

module.exports = Till;
