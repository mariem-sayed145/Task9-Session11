# C# Delegates and Events — Assignment

## Q1. What is the difference between `PriceCalculator` and `Func<Order, decimal>`?

`PriceCalculator` is a user-defined delegate, while `Func<Order, decimal>` is a built-in generic delegate in C#.

Both can represent methods that accept an `Order` object and return a `decimal` value.

The main difference is that `PriceCalculator` is a custom type with a meaningful name, whereas `Func<Order, decimal>` is a reusable built-in type that avoids defining a new delegate for the same signature.

Example:

```csharp
public delegate decimal PriceCalculator(Order order);

Func<Order, decimal> calculator =
    order => order.Price * order.Quantity;
```

Both can be used to calculate an order's total price.

## Q2. What is the difference between `Action<Order>` and `Func<Order, decimal>`?

Both are built-in generic delegates, but they serve different purposes.

* `Action<Order>` accepts an `Order` object and returns no value (`void`). It is useful for operations such as printing order details or sending notifications.
* `Func<Order, decimal>` accepts an `Order` object and returns a `decimal` value. It is useful for calculations that produce a result, such as calculating the total price.

Example:

```csharp
Action<Order> printOrder =
    order => Console.WriteLine(order.Id);

Func<Order, decimal> calculatePrice =
    order => order.Price * order.Quantity;
```

Therefore, `Action` is used for operations that do not return a value, while `Func` is used when a return value is required.

## Q3. Why does `Predicate<T>` return `bool`? What kind of problem is it designed to represent?

`Predicate<T>` returns `bool` because it is designed to represent a condition or logical test.

It accepts an object of type `T` and determines whether that object satisfies a specific condition. The result is either `true` or `false`.

For example:

```csharp
Predicate<Order> isValidOrder =
    order => order.Quantity > 0 && order.Price > 0;
```

This predicate checks whether the order has a positive quantity and a positive price.

`Predicate<T>` is commonly used for validation, filtering, and checking conditions.

## Q4. What is the difference between a delegate and an event?

A delegate is a type that represents references to methods with compatible signatures. It can be used to call a method or multiple methods through a delegate instance.

An event is a mechanism built on a delegate type that supports the publisher-subscriber pattern.

The publisher raises the event when something happens, and subscribed handlers respond to it.

For example, `OrderService` publishes the `OrderProcessed` event, while methods such as `PrintOrderMessage` and `SendNotification` subscribe to it.

In short, a delegate represents callable methods, while an event provides a controlled way to notify subscribers.

## Q5. Why can't external code normally invoke an event declared in another class?

In C#, an event can normally be raised only from within the type that declares it, or from within types permitted by the language's access rules.

External code can subscribe to an event using `+=` and unsubscribe using `-=`, but it cannot directly invoke the event.

This restriction allows the declaring class to control when the event is raised.

For example, `OrderService` should raise `OrderProcessed` after processing an order rather than allowing unrelated external code to raise it at any time.

## Q6. What happens when multiple handlers subscribe to the same event?

When multiple handlers subscribe to the same event, the event can invoke all subscribed handlers when it is raised.

For example:

```csharp
orderService.OrderProcessed += Handler1;
orderService.OrderProcessed += Handler2;
orderService.OrderProcessed += Handler3;
```

When `OrderProcessed` is raised, the handlers are normally invoked in their subscription order:

1. `Handler1`
2. `Handler2`
3. `Handler3`

This is called multicast behavior.

Each handler can perform a different operation, such as printing a message, sending a notification, or recording an audit entry.

A handler can be removed using `-=`, and an unhandled exception in a handler can prevent later handlers in the invocation list from executing.

## Q7. Explain the following code

```csharp
orderService.OrderProcessed += HandleOrderProcessed;
```

This statement subscribes the `HandleOrderProcessed` method to the `OrderProcessed` event of the `orderService` object.

* **`orderService`**: The object that exposes the event.
* **`OrderProcessed`**: The event to which the handler subscribes.
* **`+` and `=` (`+=`)**: Together, they form the subscription operator, which adds the handler to the event's invocation list.
* **`HandleOrderProcessed`**: The method that will be called when the event is raised. Its signature must be compatible with the event's delegate type.

For an event declared as `event Action<Order>`, the handler must accept an `Order` parameter and return `void`.

## Q8. What is the difference between `Action<Order>` and `event Action<Order>`?

`Action<Order>` is a delegate type that represents a method accepting an `Order` and returning no value.

When exposed as a public delegate field, external code may be able to replace its value or invoke it directly, depending on its accessibility.

`event Action<Order>` declares an event that uses `Action<Order>` as its handler type.

External code can subscribe to the event using `+=` and unsubscribe using `-=`, but it cannot directly invoke the event or replace its invocation list using `=` from outside the declaring type.

An event is preferred when implementing the publisher-subscriber pattern because it allows the declaring class to control when notifications are raised while allowing other parts of the application to subscribe to them.

In this assignment, `OrderService` controls the `OrderProcessed` event, while multiple handlers respond to the event when an order is processed.
