ExUnit.start

# 2) Create a new test module (test case) and use [`ExUnit.Case`](ExUnit.Case.html).
defmodule CheckoutTest do
  # 3) Notice we pass `async: true`, this runs the test case
  #    concurrently with other test cases
  use ExUnit.Case, async: true

  # 4) Use the `test` macro instead of `def` for clarity.
  test "single apple" do
    assert 0.30 == Checkout.price([:apple])
  end

  test "two apples" do
    assert 0.60 == Checkout.price([:apple, :apple])
  end

  test "four apples" do
    assert 1.00 == Checkout.price([:apple, :apple, :apple, :apple])
  end

  test "two apples and a coke" do
    assert 2.40 == Checkout.price([:apple, :apple, :coke])
  end

  test "one deodorant" do
    assert 2.50 == Checkout.price([:deodorant])
  end

  test "two deodorants" do
    assert 4.50 == Checkout.price([:deodorant, :deodorant])
  end

  test "two deodorants and one can of beans" do
    assert 5.00 == Checkout.price([:deodorant, :deodorant, :beans])
  end  

end