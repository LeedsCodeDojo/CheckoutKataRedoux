defmodule Checkout do
	def price(items) do
		Enum.sort(items) |> total(0)
	end

	def item([:apple,:apple,:apple,:apple|shopping]) do
		{1.00, shopping}
	end
	def item([:deodorant, :deodorant|shopping]) do
		{4.50, shopping}
	end
	def item([:apple|shopping]) do
		{0.30, shopping}
	end
	def item([:beans|shopping]) do
		{0.50, shopping}
	end
	def item([:coke|shopping]) do
		{1.80, shopping}
	end
	def item([:deodorant|shopping]) do
		{2.50, shopping}
	end


	def total([], acc) do
		acc
	end
	def total(shopping_list, acc) do
		{price, shopping} = item(shopping_list)
		total(shopping, acc + price)
	end
end
