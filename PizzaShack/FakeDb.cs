using System.Collections.Generic;
using PizzaShack.Models;

namespace PizzaShack.Mock
{
  public static class FakeDb
  {

    public static List<string> MyValues = new List<string>() { "value1", "value2", "value3" };

    public static Dictionary<string, Topping> AvailToppings = new Dictionary<string, Topping>()
    {
      { "pepperoni", new Topping("Pepperoni", .50m) },
      { "ham", new Topping("Ham", .50m) },
      { "bacon", new Topping("Bacon", .50m) },
      { "peppers", new Topping("Peppers", .50m) },
      { "olive", new Topping("Olives", .25m) },
      { "pineapple", new Topping("Pineapple", .25m) },
      { "cheeseBlend", new Topping("3 Cheese Blend", .50m) }
    };

    public static List<Pizza> Pizzas = new List<Pizza>()
    {
      new Pizza(){
        Name = "Hawaiian",
        Toppings = new List<Topping>(){
          AvailToppings.GetValueOrDefault("ham"),
          AvailToppings.GetValueOrDefault("pineapple")
        }
      },
      new Pizza(){
        Name = "Pepperoni",
        Toppings = new List<Topping>(){
          AvailToppings.GetValueOrDefault("pepperoni")
        }
      },
      new Pizza(){
        Name = "Meat Lovers",
        Toppings = new List<Topping>(){
          AvailToppings.GetValueOrDefault("ham"),
          AvailToppings.GetValueOrDefault("bacon"),
          AvailToppings.GetValueOrDefault("pepperoni"),
        }
      }
    };

    public static List<Order> Orders = new List<Order>();
  }
}