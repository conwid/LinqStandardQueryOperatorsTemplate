This repository contains the source code for my Visual Studio extensions that adds a new project template to Visual Studio. The template is a console app-based project template to help you demonstrate how the standard query operators of the Language Integrated Query, LInQ work during your classes, courses or demos.

You can download the extension from the marketplace: https://marketplace.visualstudio.com/items?itemName=CONWID.LinqStandardQueryOperatorsTemplateExtension

For other teaching aids and materials, check out [the teaching related posts on my blog](https://dotnetfalcon.com/tag/teaching).

## The template
When creating the project template I had three goals in mind:
- Create a solution so that students have to write minimal boilerplate and help the instructor dig into LInQ as fast as possible
- Provide real-world (or at least real-world like) data to work with
- Provide a way to easily visualize the query results, again without having to write code

In order to realize these goals, the basic console-app template is extended with a simple data model, a data repository and some extension methods.
After you install the extension, you'll find a new project template in Visual Studio:

![](https://dotnetfalconcontent.blob.core.windows.net/linqtemplate-marketplace-content/project.png)

### The data model
The data model is based on the late-and-great Northwind database, or at least parts of it. You can find the elements of the data model inside the Model folder of the project.

![](https://dotnetfalconcontent.blob.core.windows.net/linqtemplate-marketplace-content/datamodel.png)

### The Product class
The Product class represents a product that you can buy at a store. 
- Name (string): The name of the product
- CategoryID (int): The unique identifier of the category the product belongs to
- Category (Category): A reference to the Category object the product belongs to
- QuantityPerUnit(string): Shows the "unit" the product is sold in (e.g. "one 5 pound-box", "two pairs")
- UnitPrice (decimal): The price of the product
- UnitsInStock (short): Shows how many units are in stock of the product
- UnitsOnOrder (short): Show how many units of the product are on order
- Discontinued (bool): True if the product is no longer available, false otherwise

### The Category class
The Category class represents a product category; basically a product group (e.g. "Books", "Drinks").
- Name (string): The name of the category
- CategoryID (int): The unique identifier of the category
- Description (string): The description of the category
- Products (List<Product>): Reference to the products that belong to the category

**Note that everything is wired together by reference and there is exactly one of each object in the data source, no matter whether you access it directly from the data source, from the Category property of a Product or from the Products property of a Category. This is an important piece of information when discussing operators that do equality comparison, like the set operators, joins or GroupBy().**

**Note that both classes have their ToString() method overridden to return the value of their Name property.**

**Note that the namespace of the model classes, Model is already referenced in Program.cs. If you add other files, you have to add the reference to the namespace yourself.**

## The data repository
Instances of the model classes are available through the static DataRepository class in the DataSource folder. The class has two static properties:
- public static IEnumerable<Category> Categories { get; private set; } : Holds a reference to 8 Category objects
- public static IEnumerable<Product> Products { get; private set; }: Holds a reference to 77 Product objects

The properties are instantiated and populated in the static constructor of the class. The data for the properties come from the two XML files in the DataSource folder, which were exported from the Northwind database using the DataContractSerializer.

**Please make sure to make your students aware that the static class and static properties are only used here for the sake of simplicity and this is by no means a best practice to implement data sources.**

**Note that the properties are actually of type List<T> at runtime.**

**Note that the namespace of the DataRepository class, DataSource is already referenced in Program.cs. If you add other files, you have to add the reference to the namespace yourself.**

## The Dump() extension method
In order to visualize the results of queries, an extension method called Dump() was implemented to the IEnumerable<T> type. The Dump() method has the following capabilities:
- Can dump IEnumerable<T> collections if T is a C# primitive type, enum, DateTime or TimeSpan. In this case, the values are simply output to the console, one value per line.
- Can dump IEnumerable<T> collections of complex objects. In this case, the readable properties of the objects type that are of a C# primitive type, enum, DateTime or Timespan are output in a table-based format. If a property is of another type but the type overrides ToString(), the property is also included in the table and ToString() is used to print its value. Otherwise the property is omitted from the output.
- A header with the property names is also printed.
- When the output is displayed you have to press enter to allow execution to continue.
- The console window is maximized automatically.

![](https://dotnetfalconcontent.blob.core.windows.net/linqtemplate-marketplace-content/demo.png)

**Note that the namespace of the class of the extension method, Extensions is already referenced in Program.cs. If you add other files, you have to add the reference to the namespace yourself.**

**Please make sure to discuss with your students how LInQ query evaluation works and the inner implementation of Dump() triggers query evaluation.**

So if you are interested, go ahead and download it from the [marketplace](https://marketplace.visualstudio.com/items?itemName=CONWID.LinqStandardQueryOperatorsTemplateExtension), maybe it can help you break down boilerplate in your own presentations.

##Licensing and terms, contribution and other materials

You are welcome to use this extension during your presentations, classes or courses with the limitations of the MIT license (which is not that much :) ). But use it at your own risk :) and always test your demos beforehand (especially in the case of Dump() which was not tested extensively).
If you have ideas or requests, create an issue. PRs are always welcome :)