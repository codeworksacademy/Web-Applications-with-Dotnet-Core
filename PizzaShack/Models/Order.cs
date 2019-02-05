using System;
using System.Collections.Generic;
using System.Linq;
using PizzaShack.Mock;

namespace PizzaShack.Models
{

  public class OrderRequest
  {
    public string Name { get; set; }

    public List<string> ToppingNames { get; set; }

    public PizzaSizes Size { get; set; }

    public Pizza FullfillRequest()
    {
      Pizza pizza = new Pizza();
      pizza.Name = Name;
      ToppingNames.ForEach(t =>
      {
        if (FakeDb.AvailToppings.TryGetValue(t, out Topping topping))
        {
          pizza.Toppings.Add(topping);
        }
      });

      if (Size < PizzaSizes.Personal || Size > PizzaSizes.Family)
      {
        throw new Exception("Please select a valid Size");
      }

      pizza.Size = Size;

      return pizza;
    }


  }

  public class Order
  {
    public List<Pizza> Pizzas { get; set; } = new List<Pizza>();

    public decimal TotalPrice
    {
      get
      {
        return Pizzas.Sum(pizza => pizza.Price);
      }
    }

  }
}